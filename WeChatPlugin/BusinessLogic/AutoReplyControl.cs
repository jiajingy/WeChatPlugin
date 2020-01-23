using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Services.WeChatBackend.AutoReply;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WeChatPlugin.Settings;

namespace WeChatPlugin.BusinessLogic
{
    public class AutoReplyControl
    {
        /// <summary>
        /// Reply Message Library list
        /// </summary>
        private List<ReplyMsg> _replyMsgLib = new List<ReplyMsg>();

        /// <summary>
        /// Reply Message Library Json path
        /// </summary>
        private string _replyMsgJsonPath;

        /// <summary>
        /// Auto Reply service class
        /// </summary>
        private IAutoReply _autoReply;

        public AutoReplyControl(IOptions<ReplyMsgPathSettings> opReplyMsgPath, IAutoReply autoReply)
        {
            _replyMsgJsonPath = opReplyMsgPath.Value.path;
            ConstructorReadReplyMsgFromJson();

            _autoReply = autoReply;
        }

        private void ConstructorReadReplyMsgFromJson()
        {
            using (StreamReader r = new StreamReader(_replyMsgJsonPath))
            {
                string json = r.ReadToEnd();
                _replyMsgLib = JsonConvert.DeserializeObject<List<ReplyMsg>>(json);
            }
        }

        public List<ReplyMsg> FilteredResult(string keywords)
        {
            List<string> keywordList = _autoReply.ProcessKeywords(keywords);


            return null;

        }

    }
}
