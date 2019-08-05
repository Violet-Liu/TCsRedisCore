using System;
using System.Collections.Generic;
using System.Text;
using Yaeher.CsRedisCore.Options;

namespace Yaeher.CsRedisCore
{
    public interface IRedisClientProvider
    {
        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <returns></returns>
        IRedisClient CreateClient();
        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="redisCacheOption">redis配置信息</param>
        /// <returns></returns>
        IRedisClient CreateClient(RedisOption redisCacheOption);

        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="redisCacheOption">redis配置信息</param>
        /// <param name="serializer">序列化类</param>
        /// <returns></returns>
        IRedisClient CreateClient(RedisOption redisCacheOption, IRedisBinarySerializer serializer);
    }
}
