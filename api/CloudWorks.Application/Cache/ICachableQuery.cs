using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Cache
{
    public interface ICachableQuery
    {
        string CacheKey { get; }
        TimeSpan CacheDuration { get; }
    }
}
