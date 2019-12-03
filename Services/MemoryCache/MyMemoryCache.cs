using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace Services.MyMemoryCache
{
    public class MyMemoryCache : IMyMemoryCache
    {
        public IMemoryCache Cache { get; set; }

        public MyMemoryCache(int cache=1024)
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = cache
            });
        }

        
    }
}
