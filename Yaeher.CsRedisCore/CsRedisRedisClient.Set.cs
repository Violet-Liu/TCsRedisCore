using System;
using System.Collections.Generic;
using System.Text;

namespace Yaeher.CsRedisCore
{
    public partial class CsRedisRedisClient : IRedisClient
    {
        public long SAdd<T>(string key,params T[] members)
        {
            return Connection.SAdd(key, members);
        }
    }
}
