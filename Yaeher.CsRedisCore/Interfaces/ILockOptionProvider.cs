using System;
using System.Collections.Generic;
using System.Text;
using Yaeher.CsRedisCore.Options;

namespace Yaeher.CsRedisCore.Interfaces
{
    interface ILockOptionProvider
    {
        RedisOption GetCacheOption();
    }
}
