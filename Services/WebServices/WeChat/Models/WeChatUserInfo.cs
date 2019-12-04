using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.WebServices.WeChat.Models
{
    public class WeChatUserInfo
    {
        [JsonProperty("openid")]
        public string openid { get; set; }

        [JsonProperty("nickname")]
        public string nickname { get; set; }

        [JsonProperty("sex")]
        public string sex { get; set; }

        [JsonProperty("province")]
        public string province { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("headimgurl")]
        public string headimgurl { get; set; }

        [JsonProperty("previlege")]
        public IList<string> previlege { get; set; }

        [JsonProperty("unionid")]
        public string unionid { get; set; }
    }
}
