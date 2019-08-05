using CSRedis;
using System;
using System.Threading.Tasks;

namespace Yaeher.CsRedisCore
{
    public partial class CsRedisRedisClient : IRedisClient
    {
        #region lock

        /// <summary>
        /// 获取一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>成功返回true</returns>
        public bool LockTake<T>(string key, T value, TimeSpan expiry)
        {
            return Connection.Set(key, Serializer.Serialize(value), (int)expiry.TotalSeconds, RedisExistence.Nx);
        }

        /// <summary>
        /// 获取一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>成功返回true</returns>
        public CSRedisClientLock LockTake<T>(string key, int timeSeconds)
        {
            return Connection.Lock(key, timeSeconds);
        }

        /// <summary
        /// 异步获取一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>成功返回true</returns>
        public Task<bool> LockTakeAsync<T>(string key, T value, TimeSpan expiry)
        {
            return Connection.SetAsync(key, Serializer.Serialize(value), (int)expiry.TotalSeconds, RedisExistence.Nx);
        }

        /// <summary>
        /// 释放一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        public bool LockRelease<T>(string key, T value)
        {
            if (Exists(key))
            {
                var existsValue = StringGet<T>(key);
                if (value.Equals(existsValue))
                {
                    return Remove(key);
                }
            }
            return false;
        }

        /// <summary>
        /// 异步释放一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> LockReleaseAsync<T>(string key, T value)
        {
            if (await ExistsAsync(key))
            {
                var existsValue = await StringGetAsync<T>(key);
                if (value.Equals(existsValue))
                {
                    return await RemoveAsync(key);
                }
            }
            return false;
        }

        #endregion lock
    }
}