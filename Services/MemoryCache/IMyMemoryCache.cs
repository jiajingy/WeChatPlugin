using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;


namespace Services.MyMemoryCache
{
    public interface IMyMemoryCache
    {
        IMemoryCache Cache { get; set; }
    }
}
