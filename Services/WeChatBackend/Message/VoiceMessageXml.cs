using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Services.WeChatBackend.Message
{
    [XmlRoot(Namespace = "", ElementName = "xml", DataType = "string", IsNullable = true)]
    public class VoiceMessageXml : MessageXml
    {
        /// <summary>
        /// message content
        /// </summary>
        [XmlElement(ElementName = "MediaId")]
        public string ContMediaIdent { get; set; }
        /// <summary>
        /// message content
        /// </summary>
        [XmlElement(ElementName = "Format")]
        public string Format { get; set; }
        /// <summary>
        /// message content
        /// </summary>
        [XmlElement(ElementName = "Recognition")]
        public string Recognition { get; set; }
    }
}
