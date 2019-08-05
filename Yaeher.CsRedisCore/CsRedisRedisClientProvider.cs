using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Yaeher.CsRedisCore.Interfaces;
using Yaeher.CsRedisCore.Options;

namespace Yaeher.CsRedisCore
{
    public class CsRedisRedisClientProvider : IRedisClientProvider
    {
        private readonly IRedisBinarySerializer _redisBinarySerializer;
        private readonly RedisOption _redisOption;

        public CsRedisRedisClientProvider(IRedisBinarySerializer redisBinarySerializer,IOptionsMonitor<RedisOption> optionsMonitor)
        {
            _redisBinarySerializer = redisBinarySerializer;
            _redisOption = optionsMonitor.CurrentValue;
        }

        public IRedisClient CreateClient()
        {
            return new CsRedisRedisClient(_redisOption, _redisBinarySerializer);
        }

        public IRedisClient CreateClient(RedisOption redisCacheOption)
        {
            return new CsRedisRedisClient(redisCacheOption, _redisBinarySerializer);
        }

        public IRedisClient CreateClient(RedisOption redisCacheOption, IRedisBinarySerializer serializer)
        {
            return new CsRedisRedisClient(redisCacheOption, serializer);
        }
    }
}
