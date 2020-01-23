using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.WeChatBackend.AutoReply
{
    public class AutoReply : IAutoReply
    {

        public AutoReply()
        {

        }

        private List<string> ProcessKeywords(string keywords)
        {
            string[] keywordArray = keywords.Split(' ');

            List<string> keywordList = keywordArray.ToList();

            var result = keywordList
                .Select(s => s.Trim()) //trim
                .Distinct() //distinct
                .ToList();
            
            return result;

        }
    }
}
