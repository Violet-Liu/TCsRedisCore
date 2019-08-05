using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaeher.Common.Extensions;

namespace Yaeher.CsRedisCore
{
    public partial class CsRedisRedisClient: IRedisClient
    {
        #region StringSet

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        public bool StringSet<T>(string key, T value)
        {
            var objBytes = Serializer.Serialize(value);
            return Connection.Set(SetPrefix(key), objBytes);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> StringSetAsync<T>(string key, T value)
        {
            var objBytes = Serializer.Serialize(value);
            return await Connection.SetAsync(SetPrefix(key), objBytes);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresIn">过期间隔</param>
        /// <returns>成功返回true</returns>
        public bool StringSet<T>(string key, T value, TimeSpan expiresIn)
        {
            var objBytes = Serializer.Serialize(value);
            return Connection.Set(SetPrefix(key), objBytes, (int)expiresIn.TotalSeconds);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresIn">过期间隔</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> StringSetAsync<T>(string key, T value, TimeSpan expiresIn)
        {
            var objBytes = Serializer.Serialize(value);
            return await Connection.SetAsync(SetPrefix(key), objBytes, (int)expiresIn.TotalSeconds);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间</param>
        /// <returns>成功返回true</returns>
        public bool StringSet<T>(string key, T value, DateTimeOffset expiresAt)
        {
            var objBytes = Serializer.Serialize(value);
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);
            return Connection.Set(SetPrefix(key), objBytes, (int)expiration.TotalSeconds);
        }

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> StringSetAsync<T>(string key, T value, DateTimeOffset expiresAt)
        {
            var objBytes = Serializer.Serialize(value);
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);
            return await Connection.SetAsync(SetPrefix(key), objBytes, (int)expiration.TotalSeconds);
        }

        /// <summary>
        /// 批量设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="items">键值列表</param>
        /// <returns>成功返回true</returns>
        public bool StringSetAll<T>(IList<Tuple<string, T>> items)
        {
            foreach (var item in items)
            {
                Connection.Set(SetPrefix(item.Item1), Serializer.Serialize(item.Item2));
            }
            return true;
        }

        /// <summary>
        /// 批量设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="items">键值列表</param>
        /// <returns>成功返回true</returns>
        public async Task<bool> StringSetAllAsync<T>(IList<Tuple<string, T>> items)
        {
            foreach (var item in items)
            {
                await Connection.SetAsync(SetPrefix(item.Item1), Serializer.Serialize(item.Item2));
            }
            return true;
        }

        /// <summary>
        /// string获取值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            var valuesBytes = Connection.Get<byte[]>(SetPrefix(key));
            if (valuesBytes.ToString().IsNullOrEmptyWhiteSpace())
            {
                return default(T);
            }
            return Serializer.Deserialize<T>(valuesBytes);
        }

        /// <summary>
        /// string获取值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        public async Task<T> StringGetAsync<T>(string key)
        {
            var valuesBytes = await Connection.GetAsync<byte[]>(SetPrefix(key));
            if (valuesBytes.ToString().IsNullOrEmptyWhiteSpace())
            {
                return default(T);
            }
            return Serializer.Deserialize<T>(valuesBytes);
        }

        public async Task<T> StringGetAsync<T>(string key,Func<Task<Object>> func,int? expireSeconds=null) where T:class
        {
            var valuesBytes = await Connection.GetAsync<byte[]>(SetPrefix(key));
            if (valuesBytes.ToString().IsNullOrEmptyWhiteSpace())
            {
                T obj =(await func()) as T;
                if(obj==null)
                {
                    return default(T);
                }
                else
                {
                    var result= await Connection.SetAsync(key, obj, expireSeconds ?? RedisOption.expireSeconds ?? -1);
                    if (result)
                        return obj;
                }
            }
            return Serializer.Deserialize<T>(valuesBytes);
        }

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        public long StringIncrement(string key, long value = 1)
        {
            return Connection.IncrBy(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        public Task<long> StringIncrementAsync(string key, long value = 1)
        {
            return Connection.IncrByAsync(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        public double StringIncrementDouble(string key, double value)
        {
            return Connection.IncrByFloat(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        public Task<double> StringIncrementDoubleAsync(string key, double value)
        {
            return Connection.IncrByFloatAsync(SetPrefix(key), value);
        }

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        public long StringDecrement(string key, long value = 1)
        {
            return Connection.IncrBy(SetPrefix(key), value * -1);
        }

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        public Task<long> StringDecrementAsync(string key, long value = 1)
        {
            return Connection.IncrByAsync(SetPrefix(key), value * -1);
        }

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        public double StringDecrementDouble(string key, double value)
        {
            return Connection.IncrByFloat(SetPrefix(key), value * -1);
        }

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        public Task<double> StringDecrementDoubleAsync(string key, double value)
        {
            return Connection.IncrByFloatAsync(SetPrefix(key), value * -1);
        }

        #endregion StringSet
    }
}
