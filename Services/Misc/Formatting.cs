using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Services.Misc
{
    public static class Formatting<T>
    {

        public static T JsonToClass(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T XmlToClass(string xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StringReader stringReader = new StringReader(xml);

            return (T)xmlSerializer.Deserialize(stringReader);
        }
    }
}
