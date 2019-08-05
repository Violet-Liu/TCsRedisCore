using CSRedis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Yaeher.Common.Utils;

namespace Yaeher.CsRedisCore
{
    public class ClientFactory
    {
        private static ConcurrentDictionary<string, CSRedisClient> _connectionMultiplexerCache = new ConcurrentDictionary<string, CSRedisClient>();

        private ClientFactory()
        {
        }

        /// <summary>
        /// 获取一个连接
        /// </summary>
        /// <param name="connection">连接字符信息</param>
        /// <returns></returns>
        public static CSRedisClient GetConnection(string connection)
        {
            if (!_connectionMultiplexerCache.ContainsKey(connection))
            {
                lock (_connectionMultiplexerCache)
                {
                    if (!_connectionMultiplexerCache.ContainsKey(connection))
                    {
                        _connectionMultiplexerCache[connection] = CreateConnection(connection);
                    }
                }
            }
            return _connectionMultiplexerCache[connection];
        }

        /// <summary>
        /// 创建一个连接
        /// </summary>
        /// <param name="connection">连接字符信息</param>
        /// <returns></returns>
        private static CSRedisClient CreateConnection(string connection)
        {
            return new CSRedisClient(connection);
        }

        /// <summary>
        /// 释放全部连接
        /// </summary>
        public static void DisposeConn()
        {
            lock (_connectionMultiplexerCache)
            {
                ExceptionUtil.EatException(() =>
                {
                    foreach (var item in _connectionMultiplexerCache.Values)
                    {
                        ExceptionUtil.EatException(() => item.Dispose());
                    }
                    _connectionMultiplexerCache.Clear();
                });
            }
        }
    }
}
