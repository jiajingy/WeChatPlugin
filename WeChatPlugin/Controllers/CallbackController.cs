﻿using System;
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
using Services.WeChatBackend.Message;

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
        /// Wechat post XML to this method after receiving message from user.
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
                if (!checkSignature.IsValidSignature(timestamp, nonce, signature))
                {
                    // Unauthorized call
                    _logger.Warn("Unauthroized call!", 
                        Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                        $"signature:{signature}, nonce:{nonce}, echostr:{echostr}");
                    return Unauthorized();
                }
                

                // Authorized call
                using (var reader = new StreamReader(Request.Body))
                {
                    // Read message
                    var body = await reader.ReadToEndAsync();

                    // log original response
                    _logger.Info(body);

                    var messageXml = Formatting<MessageXml>.XmlToClass(body);


                    // Check access_token
                    if (!_cacheControl.IsCacheExist("access_token"))
                    {
                        // access token is not cached or expired, using wechat API to get a new one, then save it in cache
                        Task<string> taskGetAccessToken = _weChatAPI.GetAccessTokenAsync();
                        taskGetAccessToken.Wait();
                        _accessToken = Formatting<AccessToken>.JsonToClass(taskGetAccessToken.Result).access_token;
                        _cacheControl.SetCache("access_token", _accessToken, 60 * 60);
                    }
                    else
                    {
                        // access token is cached, retrieve it
                        _accessToken = _cacheControl.GetValueBykey("access_token").ToString();
                    }



                    // Different handlers for each type of message
                    if (messageXml.MsgType == "text")
                    {
                        TextMessageXml textMessageXml = Formatting<TextMessageXml>.XmlToClass(body);
                        _logger.Info($"User ({textMessageXml.FromUserName}) post text message {textMessageXml.Content}");
                        


                        // fetch user information
                        // IF username is already existed in somewhere like a local database, then we do not need to get info every time.
                        // But it is a callback so performance wise it does not really matter so much.
                        Task<string> taskGetUserInfo = _weChatAPI.GetSubscriberInfo(_accessToken, textMessageXml.FromUserName);
                        taskGetUserInfo.Wait();
                        WeChatUserInfo weChatUserInfo = Formatting<WeChatUserInfo>.JsonToClass(taskGetUserInfo.Result);


                        // TODO: auto reply


                    }
                    else if (messageXml.MsgType == "voice")
                    {
                        VoiceMessageXml voiceMessageXml = Formatting<VoiceMessageXml>.XmlToClass(body);
                        _logger.Info($"User ({voiceMessageXml.FromUserName}) post voice message {voiceMessageXml.Recognition}. (Media Id:{voiceMessageXml.MediaId}, Format: {voiceMessageXml.Format})");
                    }
                    else if (messageXml.MsgType == "image")
                    {
                        ImageMessageXml imageMessageXml = Formatting<ImageMessageXml>.XmlToClass(body);
                        _logger.Info($"User ({imageMessageXml.FromUserName}) post image {imageMessageXml.PicUrl}. (Media Id:{imageMessageXml.MediaId})");

                    }

                }
                return Ok(echostr);
                
                    
            }
            catch(Exception e)
            {
                _logger.Error("************");
                _logger.Error(e.ToString());
                _logger.Error("************");
                return Ok(echostr);
            }
        
        }



        
    }
}