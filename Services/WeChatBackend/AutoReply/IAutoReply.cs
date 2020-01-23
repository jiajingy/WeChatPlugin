using System;
using System.Collections.Generic;
using System.Text;

namespace Services.WeChatBackend.AutoReply
{
    public interface IAutoReply
    {
        List<string> ProcessKeywords(string keywords);
    }
}
