using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Services.WeChatBackend.Message
{
    [XmlRoot(Namespace = "", ElementName = "xml", DataType = "string", IsNullable = true)]
    public abstract class MessageXml
    {
        /// <summary>
        /// public account
        /// </summary>
        [XmlElement(ElementName = "ToUserName")]
        public string ToUserName { get; set; }
        /// <summary>
        /// user account
        /// </summary>
        [XmlElement(ElementName = "FromUserName")]
        public string FromUserName { get; set; }
        /// <summary>
        /// timestamp
        /// </summary>
        [XmlElement(ElementName = "CreateTime")]
        public string CreateTime { get; set; }
        /// <summary>
        /// message type
        /// </summary>
        [XmlElement(ElementName = "MsgType")]
        public string MsgType { get; set; }
    }
}
