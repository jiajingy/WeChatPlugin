using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Misc
{
    public static class JSONFormatting<T>
    {

        public static T JsonToClass(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
