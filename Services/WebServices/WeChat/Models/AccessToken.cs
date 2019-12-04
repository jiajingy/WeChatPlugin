using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.WebServices.WeChat.Models
{
    public class AccessToken
    {
        [JsonProperty("access_token")]
        public string access_token { get; set; }



        /// <summary>
        /// Seconds
        /// </summary>
        [JsonProperty("expires_in")]
        public int expires_in { get; set; }
    }
}
