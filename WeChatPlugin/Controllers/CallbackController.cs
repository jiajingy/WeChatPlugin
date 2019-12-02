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
using Services.MyLogger;
using Services.WeChatBackend;
using WeChatPlugin.Settings;

namespace WeChatPlugin.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private IMyLogger _logger;
        private IMemoryCache _cache;
        private IOptions<WeChatSettings> _weChatSettings;

        public CallbackController(IMyLogger logger, IMemoryCache cache,IOptions<WeChatSettings> weChatSettings)
        {
            _logger = logger;
            _cache = cache;
            _weChatSettings = weChatSettings;
        }

        [HttpGet]
        [Route("Test")]
        public IActionResult Test([FromQuery]string signature, [FromQuery]string timestamp, [FromQuery]string nonce, [FromQuery]string echostr)
        {
            MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions();
            memoryCacheEntryOptions.AbsoluteExpiration=DateTime.Now.AddMinutes(60);
            memoryCacheEntryOptions.Priority = CacheItemPriority.Normal;


            _cache.Set<string>("key", "value", memoryCacheEntryOptions);
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

                if (checkSignature.IsValidSignature(timestamp, nonce, signature))
                {
                    // Authorized call
                    using (var reader = new StreamReader(Request.Body))
                    {
                        var body = await reader.ReadToEndAsync();
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(MessageXml));
                        StringReader stringReader = new StringReader(body);
                        MessageXml messageXml = (MessageXml)xmlSerializer.Deserialize(stringReader);

                        if (messageXml.MsgType == "text")
                        {

                        }
                        else
                        {

                        }

                        _logger.Info("Post3", messageXml.Content, messageXml.FromUserName, messageXml.ToUserName);
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