﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.WebServices.WeChat
{
    public class WeChatAPI : WebService, IWeChatAPI
    {
        private string _appid { get; set; }
        private string _appsecret { get; set; }

        public WeChatAPI(string appid, string appsecret)
        {
            _appid = appid;
            _appsecret = appsecret;
            BASE_URL = "https://api.weixin.qq.com/cgi-bin/";
        }

        public WeChatAPI()
        {
            BASE_URL = "https://api.weixin.qq.com/cgi-bin/";
        }

        public async Task<string> GetAccessTokenAsync()
        {
            string url = $"{BASE_URL}token?grant_type=client_credential&appid={_appid}&secret={_appsecret}";
            return await GetAsync(url);
        }

       


    }
}
