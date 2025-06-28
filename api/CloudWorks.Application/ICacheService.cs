﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application
{
    public interface ICacheService
    {
        T Get<T>(string key);

        void Set<T>(string key, T value, TimeSpan duration);

        void Remove(string key);
    }
}
