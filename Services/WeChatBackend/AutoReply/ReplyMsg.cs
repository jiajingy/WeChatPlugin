using System;
using System.Collections.Generic;
using System.Text;

namespace Services.WeChatBackend.AutoReply
{
    public class ReplyMsg
    {
        /// <summary>
        /// Keyword for search mapping
        /// </summary>
        public List<string> Keywords { get; set; }


        /// <summary>
        /// Reply title
        /// </summary>
        public string ReplyTitle { get; set; }


        /// <summary>
        /// Reply content
        /// </summary>
        public string ReplyContent { get; set; }


    }
}
