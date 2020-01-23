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

        public List<string> ProcessKeywords(string keywords)
        {
            string[] keywordArray = keywords.Split(' ');

            List<string> keywordList = keywordArray.ToList();

            var result = keywordList
                .Select(s => s.Trim()) //trim
                .Distinct() //distinct
                .ToList();
            
            return result;

        }

        public List<ReplyMsg> CompareKeywords(List<string> keywordList, List<ReplyMsg> replyMsgLib)
        {
            
            var query = from a in replyMsgLib
                        join b in keywordList
                        on a.ReplyContent equals b
                        select new
                        {
                            a.Keywords,
                            a.ReplyTitle,
                            a.ReplyContent
                        };

            return null;
        }

         
    }
}
