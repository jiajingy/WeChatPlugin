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
        private List<ReplyMsg> _replyMsgLib = new List<ReplyMsg>();
        private string _replyMsgJsonPath;
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



    }
}
