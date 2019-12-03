using Microsoft.Extensions.Caching.Memory;
using Services.MyMemoryCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.MyMemoryCache
{
    public class CacheControl : ICacheControl
    {
        private IMyMemoryCache _cache;
        public CacheControl(IMyMemoryCache cache)
        {
            _cache = cache;
        }

        public bool IsCacheExist(string key)
        {
            return _cache.Cache.TryGetValue(key, out object value);
        }

        public object GetValueBykey(string key)
        {
            if (!_cache.Cache.TryGetValue(key, out object value))
                return value;
            else
                throw new Exception("Either cache does not exist or cache has expired");
            
        }

        public void ResetExpirationTime(string key, int seconds)
        {
            object value = GetValueBykey(key);
            _cache.Cache.Set<object>(key, value,DateTimeOffset.Now.AddSeconds(seconds));
        }
    }
}
