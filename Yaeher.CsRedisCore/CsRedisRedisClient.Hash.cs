using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaeher.Common.Extensions;

namespace Yaeher.CsRedisCore
{

    public partial class CsRedisRedisClient : IRedisClient
    {
        #region hash

        /// <summary>
        /// 获取所有的Hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public IEnumerable<string> HashKeys(string key)
        {
            return Connection.HKeys(SetPrefix(key)).Select(x => x.ToString());
        }

        /// <summary>
        /// 获取hash键的个数
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="commandFlags"></param>
        /// <returns></returns>
        public long HashLength(string key)
        {
            return Connection.HLen(SetPrefix(key));
        }

        /// <summary>
        /// 设置一个hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashField">hash的键值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool HashSet<T>(string key, string hashField, T value)
        {
            return Connection.HSet(SetPrefix(key), hashField, Serializer.Serialize(value).ToStr());
        }

        /// <summary>
        /// 设置一个hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashField">hash的键值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public Task<bool> HashSetAsync<T>(string key, string hashField, T value)
        {
            return Connection.HSetAsync(SetPrefix(key), hashField, Serializer.Serialize(value).ToStr());
        }

        /// <summary>
        /// 批量设置hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="values">键值对</param>
        public void HashSet<T>(string key, Dictionary<string, T> values)
        {
            var dic = new Dictionary<string, string>();
            foreach (KeyValuePair<string, T> item in values)
            {
                dic.Add(item.Key, Serializer.Serialize(item.Value).ToStr());
            }
            Connection.HMSet(SetPrefix(key), dic);
        }

        /// <summary>
        /// 批量设置hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="values">键值对</param>
        public Task HashSetAsync<T>(string key, Dictionary<string, T> values)
        {
            var dic = new Dictionary<string, string>();
            foreach (KeyValuePair<string, T> item in values)
            {
                dic.Add(item.Key, Serializer.Serialize(item.Value).ToStr());
            }
            return Connection.HMSetAsync(SetPrefix(key), dic);
        }

        /// <summary>
        /// 获取一个hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        public T HashGet<T>(string key, string hashField)
        {
            var redisValue = Connection.HMGet(SetPrefix(key), hashField);
            return redisValue.Any() ? Serializer.Deserialize<T>(redisValue[0].ToBytes()) : default(T);
        }

        /// <summary>
        /// 获取一个hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string key, string hashField)
        {
            var redisValue = await Connection.HMGetAsync(SetPrefix(key), hashField);
            return redisValue.Any() ? Serializer.Deserialize<T>(redisValue[0].ToBytes()) : default(T);
        }

        /// <summary>
        /// 获取hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashFields">hash键组合</param>
        /// <returns></returns>
        public Dictionary<string, T> HashGet<T>(string key, IEnumerable<string> hashFields)
        {
            var result = new Dictionary<string, T>();
            foreach (var hashField in hashFields)
            {
                var value = HashGet<T>(key, hashField);
                result.Add(key, value);
            }
            return result;
        }

        /// <summary>
        /// 获取hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashFields">hash键组合</param>
        /// <returns></returns>
        public async Task<Dictionary<string, T>> HashGetAsync<T>(string key, IEnumerable<string> hashFields)
        {
            var result = new Dictionary<string, T>();
            foreach (var hashField in hashFields)
            {
                var value = await HashGetAsync<T>(key, hashField);
                result.Add(key, value);
            }
            return result;
        }

        /// <summary>
        /// 获取全部hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public Dictionary<string, T> HashGetAll<T>(string key)
        {
            return (Connection
                        .HGetAll(SetPrefix(key)))
                        .ToDictionary(
                            x => x.Key.ToString(),
                            x => Serializer.Deserialize<T>(x.Value.ToBytes()),
                            StringComparer.Ordinal);
        }

        /// <summary>
        /// 获取全部hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public async Task<Dictionary<string, T>> HashGetAllAsync<T>(string key)
        {
            return (await Connection
                        .HGetAllAsync(SetPrefix(key)))
                        .ToDictionary(
                            x => x.Key.ToString(),
                            x => Serializer.Deserialize<T>(x.Value.ToBytes()),
                            StringComparer.Ordinal);
        }

        /// <summary>
        /// 获取全部hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public IEnumerable<T> HashValues<T>(string key)
        {
            return Connection.HGetAll(SetPrefix(key)).Select(m => Serializer.Deserialize<T>(m.Value.ToBytes()));
        }

        /// <summary>
        /// 获取全部hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> HashValuesAsync<T>(string key)
        {
            return (await Connection.HGetAllAsync(SetPrefix(key))).Select(m => Serializer.Deserialize<T>(m.Value.ToBytes()));
        }

        /// <summary>
        /// 判断是否存在hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        public bool HashExists(string key, string hashField)
        {
            return Connection.HExists(SetPrefix(key), hashField);
        }

        /// <summary>
        /// 判断是否存在hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        public Task<bool> HashExistsAsync(string key, string hashField)
        {
            return Connection.HExistsAsync(SetPrefix(key), hashField);
        }

        /// <summary>
        /// 删除一个hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        public bool HashDelete(string key, string hashField)
        {
            return Connection.HDel(SetPrefix(key), hashField) > 0;
        }

        /// <summary>
        /// 删除一个hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, string hashField)
        {
            return (await Connection.HDelAsync(SetPrefix(key), hashField)) > 0;
        }

        /// <summary>
        /// 删除hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashFields">hash键集合</param>
        /// <returns></returns>
        public long HashDelete(string key, IEnumerable<string> hashFields)
        {
            return Connection.HDel(SetPrefix(key), hashFields.Select(x => x).ToArray());
        }

        /// <summary>
        /// 删除hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashFields">hash键集合</param>
        /// <returns></returns>
        public Task<long> HashDeleteAsync(string key, IEnumerable<string> hashFields)
        {
            return Connection.HDelAsync(SetPrefix(key), hashFields.Select(x => x).ToArray());
        }

        /// <summary>
        /// hash递增
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递增值</param>
        /// <returns></returns>
        public long HashIncrement(string key, string hashField, long value = 1)
        {
            return Connection.HIncrBy(SetPrefix(key), hashField, value);
        }

        /// <summary>
        /// hash递增
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递增值</param>
        /// <returns></returns>
        public Task<long> HashIncrementAsync(string key, string hashField, long value = 1)
        {
            return Connection.HIncrByAsync(SetPrefix(key), hashField, value);
        }

        /// <summary>
        /// hash递减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递减值</param>
        /// <returns></returns>
        public long HashDecrement(string key, string hashField, long value = 1)
        {
            return Connection.HIncrBy(SetPrefix(key), hashField, value * -1);
        }

        /// <summary>
        /// hash递减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递减值</param>
        /// <returns></returns>
        public Task<long> HashDecrementAsync(string key, string hashField, long value = 1)
        {
            return Connection.HIncrByAsync(SetPrefix(key), hashField, value * -1);
        }

        /// <summary>
        /// hash递增
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递增值</param>
        /// <returns></returns>
        public double HashIncrementDouble(string key, string hashField, double value)
        {
            return Connection.HIncrByFloat(SetPrefix(key), hashField, value);
        }

        /// <summary>
        /// hash递增
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递增值</param>
        /// <returns></returns>
        public Task<double> HashIncrementDoubleAsync(string key, string hashField, double value)
        {
            return Connection.HIncrByFloatAsync(SetPrefix(key), hashField, value);
        }

        /// <summary>
        /// hash递减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递减值</param>
        /// <returns></returns>
        public double HashDecrementDouble(string key, string hashField, double value)
        {
            return Connection.HIncrByFloat(SetPrefix(key), hashField, value * -1);
        }

        /// <summary>
        /// hash递减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递减值</param>
        /// <returns></returns>
        public Task<double> HashDecrementDoubleAsync(string key, string hashField, double value)
        {
            return Connection.HIncrByFloatAsync(SetPrefix(key), hashField, value * -1);
        }

        #endregion hash
    }
}