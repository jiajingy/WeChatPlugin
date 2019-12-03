using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.WebServices.WeChat
{
    public interface IWeChatAPI
    {
        Task<string> GetAccessTokenAsync();
    }
}
