using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Services.WeChatBackend.Message
{
    [XmlRoot(Namespace = "", ElementName = "xml", DataType = "string", IsNullable = true)]
    public class ImageMessageXml : MessageXml
    {
        [XmlElement(ElementName = "PicUrl")]
        public string PicUrl { get; set; }


        [XmlElement(ElementName = "MediaId")]
        public string MediaId { get; set; }
    }
}
