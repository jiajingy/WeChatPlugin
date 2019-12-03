using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.MyLogger;
using Services.MyMemoryCache;
using Services.WebServices.WeChat;

namespace WeChatPlugin.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IMyLogger _logger;
        private IWeChatAPI _weChatAPI;
        private ICacheControl _cacheControl;

        public TestController(IMyLogger logger,IWeChatAPI weChatAPI, ICacheControl cacheControl)
        {
            _logger = logger;
            _weChatAPI = weChatAPI;
            _cacheControl = cacheControl;
        }


        [HttpGet]
        [Route("HttpGetTest")]
        public IActionResult HttpGetTest()
        {

            //Task<string> taskGetToken = _weChatAPI.GetAccessTokenAsync();
            //taskGetToken.Wait();
            //_logger.Debug(taskGetToken.Result);

            _cacheControl.SetCache("access_token", "xinwei", 120);
            
            

            string accessTOken = _cacheControl.GetValueBykey("access_token").ToString();

            _logger.Debug(accessTOken);

            _cacheControl.RemoveCache("access_token");
            bool cacheExist = _cacheControl.IsCacheExist("access_token");

            _logger.Debug(cacheExist.ToString());



            return Ok();
        }
    }
}