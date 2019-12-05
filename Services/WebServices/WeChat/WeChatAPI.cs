using System;
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
            BASE_URL = "https://api.weixin.qq.com";
        }

        public WeChatAPI()
        {
            BASE_URL = "https://api.weixin.qq.com";
        }

        public async Task<string> GetAccessTokenAsync()
        {
            string url = $"{BASE_URL}/cgi-bin/token?grant_type=client_credential&appid={_appid}&secret={_appsecret}";
            return await GetAsync(url);
        }

        public async Task<string> GetSubscriberInfo(string access_token, string openid)
        {
            string url = $"{BASE_URL}/cgi-bin/user/info?access_token={access_token}&openid={openid}&lang=en";
            return await GetAsync(url);
        }

        public async Task<string> GetUserInfo(string access_token, string openid)
        {
            string url = $"{BASE_URL}/sns/userinfo?access_token={access_token}&openid={openid}&lang=en";
            return await GetAsync(url);

        }


       


    }
}
