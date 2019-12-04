using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Services.MyLogger;
using Services.MyMemoryCache;
using Services.WebServices.WeChat;
using Services.WebServices.WeChat.Models;
using Services.WeChatBackend;
using WeChatPlugin.Settings;
using Services.Misc;

namespace WeChatPlugin.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private IMyLogger _logger;
        private ICacheControl _cacheControl;
        private IOptions<WeChatSettings> _weChatSettings;
        private IWeChatAPI _weChatAPI;





        private string _accessToken;

        public CallbackController(IMyLogger logger, ICacheControl cacheControl, IOptions<WeChatSettings> weChatSettings,IWeChatAPI weChatAPI)
        {
            _logger = logger;
            _cacheControl = cacheControl;
            _weChatSettings = weChatSettings;
            _weChatAPI = weChatAPI;
        }

        [HttpGet]
        [Route("Test")]
        public IActionResult Test([FromQuery]string signature, [FromQuery]string timestamp, [FromQuery]string nonce, [FromQuery]string echostr)
        {
            
            
            _logger.Info(echostr, signature, timestamp, nonce);
            return Ok("good");
        }


        /// <summary>
        /// This is ONLY used when establishing connection between WeChat and callback. After connection successfully established, WeChat will send info. with HttpPost Method.
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PublicAccountCallback")]
        public IActionResult PublicAccountCallback([FromQuery]string signature, [FromQuery]string timestamp, [FromQuery]string nonce, [FromQuery]string echostr)
        {
            try
            {
                _logger.Info(echostr,signature,timestamp,nonce);

                CheckSignature checkSignature = new CheckSignature(_weChatSettings.Value.token);

                if (checkSignature.IsValidSignature(timestamp, nonce, signature))
                    return Ok(echostr);
                else
                    return Unauthorized();
            }
            catch(Exception e)
            {
                _logger.Error(e.ToString());
                return StatusCode(500);
            }
            
            
        }


        /// <summary>
        /// Wechat post XML to this method.
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PublicAccountCallback")]
        public async Task<IActionResult> PublicAccountCallbackPost([FromQuery]string signature, [FromQuery]string timestamp, [FromQuery]string nonce, [FromQuery]string echostr)
        {
            try
            {

                CheckSignature checkSignature = new CheckSignature(_weChatSettings.Value.token);

                // Check if signature matches
                if (checkSignature.IsValidSignature(timestamp, nonce, signature))
                {
                    // Authorized call
                    using (var reader = new StreamReader(Request.Body))
                    {
                        // Read message
                        var body = await reader.ReadToEndAsync();
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(MessageXml));
                        StringReader stringReader = new StringReader(body);
                        MessageXml messageXml = (MessageXml)xmlSerializer.Deserialize(stringReader);

                        

                        if (messageXml.MsgType == "text")
                        {
                            _logger.Info($"User ({messageXml.FromUserName}) post text message {messageXml.Content}");
                            // Check if access token is cached already, if not, using wechat API to get a new one, then save it in cache
                            if (!_cacheControl.IsCacheExist("access_token"))
                            {
                                Task<string> taskGetAccessToken = _weChatAPI.GetAccessTokenAsync();
                                taskGetAccessToken.Wait();
                                _accessToken = JSONFormatting<AccessToken>.JsonToClass(taskGetAccessToken.Result).access_token;
                                _cacheControl.SetCache("access_token", _accessToken, 60 * 60);
                            }
                            // access token is cached, retrieve it
                            else
                            {
                                _accessToken = _cacheControl.GetValueBykey("access_token").ToString();
                            }


                            Task<string> taskGetUserInfo = _weChatAPI.GetUserInfo(_accessToken, messageXml.FromUserName);
                            taskGetUserInfo.Wait();
                            WeChatUserInfo weChatUserInfo = JSONFormatting<WeChatUserInfo>.JsonToClass(taskGetUserInfo.Result);

                            _logger.Debug("hehehhee", taskGetUserInfo.Result,_accessToken);


                            // fetch user information
                            
                        }
                        else
                        {

                        }

                        
                    }
                    return Ok(echostr);
                }
                else
                {
                    // Unauthorized call
                    _logger.Warn("Unauthroized call!", Request.HttpContext.Connection.RemoteIpAddress?.ToString());
                    return Unauthorized();
                }
                    
            }
            catch(Exception e)
            {
                _logger.Error("---");
                _logger.Error(e.ToString());
                _logger.Error("---");
                return Ok(echostr);
            }
        
        }



        
    }
}