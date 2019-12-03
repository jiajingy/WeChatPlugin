using Services.MyMemoryCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.MyMemoryCache
{
    public interface ICacheControl
    {
        bool IsCacheExist(string key);

        object GetValueBykey(string key);

        void ResetExpirationTime(string key, int seconds);

        void SetCache(string key, object value, int seconds);

        void SetCache(string key, string value, int seconds);

        void RemoveCache(string key);
    }
}
