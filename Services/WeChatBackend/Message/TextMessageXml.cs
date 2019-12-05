using Services.WeChatBackend.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Services.WeChatBackend.Message
{
    [XmlRoot(Namespace = "", ElementName = "xml", DataType = "string", IsNullable = true)]
    public class TextMessageXml : MessageXml
    {

        /// <summary>
        /// message content
        /// </summary>
        [XmlElement(ElementName = "Content")]
        public string Content { get; set; }




    }
}
