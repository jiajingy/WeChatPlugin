using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Misc;
using Services.MyLogger;
using Services.MyMemoryCache;
using Services.WebServices.WeChat;
using Services.WebServices.WeChat.Models;

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



            //Task<string> taskGetAccessToken = _weChatAPI.GetAccessTokenAsync();

            //taskGetAccessToken.Wait();

            //string _accessToken = JSONFormatting<AccessToken>.JsonToClass(taskGetAccessToken.Result).access_token;

            string _accessToken = "28_K2O8C0JWZGM2XguSgS8x-frs3nQyiQywTn85uuqubhMhnBGNCmVD96p0YoqtQ7GF_v9_sT9-20ORuN1FDlI4c3ecSZi1Whx3RQbQbocvaTWHq4u_VZEmOopvcBWBh6GjTHuNvmJj2WY91VdZGVBjABANCI";

            _logger.Debug(_accessToken);

            Task<string> getUserInfo = _weChatAPI.GetSubscriberInfo(_accessToken, "oQn7Pv0i5Y65EL-mdgT11KbhLK6g");
            getUserInfo.Wait();

            var userInfo = Formatting<WeChatUserInfo>.JsonToClass(getUserInfo.Result);

            _logger.Debug(userInfo.nickname);


            return Ok();
        }
    }
}