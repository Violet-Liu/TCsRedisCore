using CSRedis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaeher.Common.Extensions;
using Yaeher.CsRedisCore.Enums;
using Yaeher.CsRedisCore.Interfaces;
using Yaeher.CsRedisCore.Options;

namespace Yaeher.CsRedisCore
{
    public partial class CsRedisRedisClient:IRedisClient
    {
        private CSRedisClient _csRedisClient;

        public CsRedisRedisClient(RedisOption redisOption, IRedisBinarySerializer redisBinarySerializer)
        {
            RedisOption = redisOption;
            Serializer = redisBinarySerializer;
        }

        public IRedisBinarySerializer Serializer { get; }

        public RedisOption RedisOption { get; }

        private CSRedisClient Connection => _csRedisClient ?? (_csRedisClient = ClientFactory.GetConnection(RedisOption.ConnectionString));

        object IRedisClient.Connection => Connection;

        private string SetPrefix(string key)
        {
            return RedisOption.Prefix.IsNullOrEmptyWhiteSpace() ? key : $"{RedisOption.Prefix}{RedisOption.NamespaceSplitSymbol}{key}";
        }

        #region Keys

        /// <summary>
        /// 查找当前命名前缀下共有多少个Key
        /// </summary>
        /// <returns></returns>
        public int KeyCount()
        {
            return CalcuteKeyCount("*");
        }

        /// <summary>
        /// 查找键名
        /// </summary>
        /// <param name="pattern">匹配项</param>
        /// <returns>匹配上的所有键名</returns>
        public IEnumerable<string> SearchKeys(string pattern)
        {
            return Connection.Keys(pattern);
        }

        /// <summary>
        /// 判断是否存在当前的Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return Connection.Exists(SetPrefix(key));
        }

        /// <summary>
        /// 判断是否存在当前的Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> ExistsAsync(string key)
        {
            return Connection.ExistsAsync(SetPrefix(key));
        }

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Expire(string key, TimeSpan expiry)
        {
            return Connection.Expire(SetPrefix(key), expiry);
        }

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> ExpireAsync(string key, TimeSpan expiry)
        {
            return Connection.ExpireAsync(SetPrefix(key), expiry);
        }

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Expire(string key, DateTime expiry)
        {
            return Connection.Expire(SetPrefix(key), expiry - DateTime.Now);
        }

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> ExpireAsync(string key, DateTime expiry)
        {
            return Connection.ExpireAsync(SetPrefix(key), expiry - DateTime.Now);
        }

        /// <summary>
        /// 移除当前key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return Connection.Del(SetPrefix(key)) > 0;
        }

        /// <summary>
        /// 移除当前key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string key)
        {
            return (await Connection.DelAsync(SetPrefix(key))) > 0;
        }

        /// <summary>
        /// 移除全部key
        /// </summary>
        /// <param name="keys"></param>
        public void RemoveAll(IEnumerable<string> keys)
        {
            keys.ForEach(key =>
            {
                Remove(key);
            });
        }

        /// <summary>
        /// 移除全部key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public Task RemoveAllAsync(IEnumerable<string> keys)
        {
            return keys.ForEachAsync(RemoveAsync);
        }

        public object Eval(string script, string key, params object[] args)
        {
            return Connection.Eval(script, key, args: args);
        }

        public Task<object> EvalAsync(string script, string key, params object[] args)
        {
            return Connection.EvalAsync(script, key, args: args);
        }

        #endregion Keys

        #region Public

        /// <summary>
        /// 清除key
        /// </summary>
        public void FlushDb()
        {
            Connection.ScriptFlush();
        }

        /// <summary>
        /// 清除key
        /// </summary>
        public Task FlushDbAsync()
        {
            return Connection.ScriptFlushAsync();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveType"></param>
        public void Save(RedisSaveType saveType)
        {
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveType"></param>
        /// <returns></returns>
        public Task SaveAsync(RedisSaveType saveType)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 清除当前db的所有数据
        /// </summary>
        public void Clear()
        {
            DeleteKeyWithKeyPrefix("*");
        }

        /// <summary>
        /// 计算当前prefix开头的key总数
        /// </summary>
        /// <param name="prefix">key前缀</param>
        /// <returns></returns>
        private int CalcuteKeyCount(string prefix)
        {
            var retVal = Connection.Eval(@" return table.getn(redis.call('keys', ARGV[1]))", "", SetPrefix(prefix));
            if (retVal == null)
            {
                return 0;
            }
            return (int)retVal;
        }

        /// <summary>
        /// 删除以当前prefix开头的所有key缓存
        /// </summary>
        /// <param name="prefix">key前缀</param>
        private void DeleteKeyWithKeyPrefix(string prefix)
        {
            Connection.Eval(@"
                local keys = redis.call('keys', ARGV[1])
                for i=1,#keys,5000 do
                redis.call('del', unpack(keys, i, math.min(i+4999, #keys)))
                end", "", SetPrefix(prefix));
        }

        #endregion Public
    }
}
