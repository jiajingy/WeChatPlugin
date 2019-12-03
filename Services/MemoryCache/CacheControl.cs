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
            if (_cache.Cache.TryGetValue(key, out object value))
                return value;
            else
                throw new Exception("Either cache does not exist or cache has expired");
            
        }

        public void ResetExpirationTime(string key, int seconds)
        {
            object value = GetValueBykey(key);
            _cache.Cache.Set<object>(key, value,DateTimeOffset.Now.AddSeconds(seconds));
        }

        public void SetCache(string key, object value, int seconds)
        {
            if (IsCacheExist(key))
                throw new Exception("Cache already exists");

            _cache.Cache.Set<object>(key, value, DateTimeOffset.Now.AddSeconds(seconds));
        }

        public void SetCache(string key, string value, int seconds)
        {
            if (IsCacheExist(key))
                throw new Exception("Cache already exists");

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetPriority(CacheItemPriority.Normal)
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(seconds));

            _cache.Cache.Set<object>(key, value,cacheEntryOptions);
        }

        public void RemoveCache(string key)
        {
            if(IsCacheExist(key))
                _cache.Cache.Remove(key);
        }

    }
}
