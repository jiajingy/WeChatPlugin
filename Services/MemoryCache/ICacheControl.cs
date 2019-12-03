using Services.MyMemoryCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.MyMemoryCache
{
    public interface ICacheControl
    {
        public bool IsCacheExist(string key);

        public object GetValueBykey(string key);

        public void ResetExpirationTime(string key, int seconds);
    }
}
