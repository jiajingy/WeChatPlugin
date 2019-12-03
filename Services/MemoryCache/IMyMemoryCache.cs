using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;


namespace Services.MyMemoryCache
{
    public interface IMyMemoryCache
    {
        public IMemoryCache Cache { get; set; }
    }
}
