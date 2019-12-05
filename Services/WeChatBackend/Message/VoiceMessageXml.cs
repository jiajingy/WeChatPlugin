using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Services.WeChatBackend.Message
{
    [XmlRoot(Namespace = "", ElementName = "xml", DataType = "string", IsNullable = true)]
    public class VoiceMessageXml : MessageXml
    {
        
        [XmlElement(ElementName = "MediaId")]
        public string MediaId { get; set; }
        
        [XmlElement(ElementName = "Format")]
        public string Format { get; set; }
        
        [XmlElement(ElementName = "Recognition")]
        public string Recognition { get; set; }

        [XmlElement(ElementName = "MsgId")]
        public string MsgId { get; set; }
    }
}
