using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaeher.CsRedisCore.Enums;
using Yaeher.CsRedisCore.Geo;

namespace Yaeher.CsRedisCore
{
    public interface IRedisClient
    {
        object Connection { get; }

        #region Keys

        /// <summary>
        /// 查找当前命名前缀下共有多少个Key
        /// </summary>
        /// <returns></returns>
        int KeyCount();

        /// <summary>
        /// 查找键名
        /// </summary>
        /// <param name="pattern">匹配项</param>
        /// <returns>匹配上的所有键名</returns>
        IEnumerable<string> SearchKeys(string pattern);

        /// <summary>
        /// 判断是否存在当前的Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 判断是否存在当前的Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key);

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool Expire(string key, TimeSpan expiry);

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> ExpireAsync(string key, TimeSpan expiry);

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool Expire(string key, DateTime expiry);

        /// <summary>
        /// 设置key的失效时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> ExpireAsync(string key, DateTime expiry);

        /// <summary>
        /// 移除当前key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);

        /// <summary>
        /// 移除当前key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key);

        /// <summary>
        /// 移除全部key
        /// </summary>
        /// <param name="keys"></param>
        void RemoveAll(IEnumerable<string> keys);

        /// <summary>
        /// 移除全部key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task RemoveAllAsync(IEnumerable<string> keys);

        #endregion Keys

        #region StringSet

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        bool StringSet<T>(string key, T value);

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        Task<bool> StringSetAsync<T>(string key, T value);

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresIn">过期间隔</param>
        /// <returns>成功返回true</returns>
        bool StringSet<T>(string key, T value, TimeSpan expiresIn);

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresIn">过期间隔</param>
        /// <returns>成功返回true</returns>
        Task<bool> StringSetAsync<T>(string key, T value, TimeSpan expiresIn);

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间</param>
        /// <returns>成功返回true</returns>
        bool StringSet<T>(string key, T value, DateTimeOffset expiresAt);

        /// <summary>
        /// 设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间</param>
        /// <returns>成功返回true</returns>
        Task<bool> StringSetAsync<T>(string key, T value, DateTimeOffset expiresAt);

        /// <summary>
        /// 批量设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="items">键值列表</param>
        /// <returns>成功返回true</returns>
        bool StringSetAll<T>(IList<Tuple<string, T>> items);

        /// <summary>
        /// 批量设置string键值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="items">键值列表</param>
        /// <returns>成功返回true</returns>
        Task<bool> StringSetAllAsync<T>(IList<Tuple<string, T>> items);

        /// <summary>
        /// string获取值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        T StringGet<T>(string key);

        /// <summary>
        /// string获取值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        Task<T> StringGetAsync<T>(string key);

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        long StringIncrement(string key, long value = 1);

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        Task<long> StringIncrementAsync(string key, long value = 1);

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        double StringIncrementDouble(string key, double value);

        /// <summary>
        /// 键值累加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">增长数量</param>
        /// <returns>累加后的值</returns>
        Task<double> StringIncrementDoubleAsync(string key, double value);

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        long StringDecrement(string key, long value = 1);

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        Task<long> StringDecrementAsync(string key, long value = 1);

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        double StringDecrementDouble(string key, double value);

        /// <summary>
        /// 键值递减
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">减少数量</param>
        /// <returns>递减后的值</returns>
        Task<double> StringDecrementDoubleAsync(string key, double value);

        #endregion StringSet

        #region hash

        /// <summary>
        /// 获取所有的Hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        IEnumerable<string> HashKeys(string key);

        /// <summary>
        /// 获取hash键的个数
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        long HashLength(string key);

        /// <summary>
        /// 设置一个hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashField">hash的键值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool HashSet<T>(string key, string hashField, T value);

        /// <summary>
        /// 设置一个hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashField">hash的键值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Task<bool> HashSetAsync<T>(string key, string hashField, T value);

        /// <summary>
        /// 批量设置hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="values">键值对</param>
        void HashSet<T>(string key, Dictionary<string, T> values);

        /// <summary>
        /// 批量设置hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="values">键值对</param>
        Task HashSetAsync<T>(string key, Dictionary<string, T> values);

        /// <summary>
        /// 获取一个hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        T HashGet<T>(string key, string hashField);

        /// <summary>
        /// 获取一个hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        Task<T> HashGetAsync<T>(string key, string hashField);

        /// <summary>
        /// 获取hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashFields">hash键组合</param>
        /// <returns></returns>
        Dictionary<string, T> HashGet<T>(string key, IEnumerable<string> hashFields);

        /// <summary>
        /// 获取hash值
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="hashFields">hash键组合</param>
        /// <returns></returns>
        Task<Dictionary<string, T>> HashGetAsync<T>(string key, IEnumerable<string> hashFields);

        /// <summary>
        /// 获取全部hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        Dictionary<string, T> HashGetAll<T>(string key);

        /// <summary>
        /// 获取全部hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        Task<Dictionary<string, T>> HashGetAllAsync<T>(string key);

        /// <summary>
        /// 获取全部hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        IEnumerable<T> HashValues<T>(string key);

        /// <summary>
        /// 获取全部hash值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        Task<IEnumerable<T>> HashValuesAsync<T>(string key);

        /// <summary>
        /// 判断是否存在hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        bool HashExists(string key, string hashField);

        /// <summary>
        /// 判断是否存在hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        Task<bool> HashExistsAsync(string key, string hashField);

        /// <summary>
        /// 删除一个hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        bool HashDelete(string key, string hashField);

        /// <summary>
        /// 删除一个hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <returns></returns>
        Task<bool> HashDeleteAsync(string key, string hashField);

        /// <summary>
        /// 删除hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashFields">hash键集合</param>
        /// <returns></returns>
        long HashDelete(string key, IEnumerable<string> hashFields);

        /// <summary>
        /// 删除hash键
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashFields">hash键集合</param>
        /// <returns></returns>
        Task<long> HashDeleteAsync(string key, IEnumerable<string> hashFields);

        /// <summary>
        /// hash递增
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递增值</param>
        /// <returns></returns>
        long HashIncrement(string key, string hashField, long value = 1);

        /// <summary>
        /// hash递增
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递增值</param>
        /// <returns></returns>
        Task<long> HashIncrementAsync(string key, string hashField, long value = 1);

        /// <summary>
        /// hash递减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递减值</param>
        /// <returns></returns>
        long HashDecrement(string key, string hashField, long value = 1);

        /// <summary>
        /// hash递减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递减值</param>
        /// <returns></returns>
        Task<long> HashDecrementAsync(string key, string hashField, long value = 1);

        /// <summary>
        /// hash递增
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递增值</param>
        /// <returns></returns>
        double HashIncrementDouble(string key, string hashField, double value);

        /// <summary>
        /// hash递增
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递增值</param>
        /// <returns></returns>
        Task<double> HashIncrementDoubleAsync(string key, string hashField, double value);

        /// <summary>
        /// hash递减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递减值</param>
        /// <returns></returns>
        double HashDecrementDouble(string key, string hashField, double value);

        /// <summary>
        /// hash递减
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">hash键</param>
        /// <param name="value">递减值</param>
        /// <returns></returns>
        Task<double> HashDecrementDoubleAsync(string key, string hashField, double value);

        #endregion hash

        #region Public

        /// <summary>
        ///  Execute a Lua script against the server.
        /// </summary>
        /// <param name="script">The script to execute.</param>
        /// <param name="key">The keys to execute against.</param>
        /// <param name="args">The values to execute against.</param>
        /// <returns>A dynamic representation of the script's result</returns>
        object Eval(string script, string key, params object[] args);

        /// <summary>
        ///  Execute a Lua script against the server.
        /// </summary>
        /// <param name="script">The script to execute.</param>
        /// <param name="key">The keys to execute against.</param>
        /// <param name="args">The values to execute against.</param>
        /// <returns>A dynamic representation of the script's result</returns>
        Task<object> EvalAsync(string script, string key, params object[] args);

        /// <summary>
        /// 清除key
        /// </summary>
        void FlushDb();

        /// <summary>
        /// 清除key
        /// </summary>
        Task FlushDbAsync();

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveType"></param>
        void Save(RedisSaveType saveType);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="saveType"></param>
        /// <returns></returns>
        Task SaveAsync(RedisSaveType saveType);

        /// <summary>
        /// 清除当前db的所有数据
        /// </summary>
        void Clear();

        #endregion Public

        #region lock

        /// <summary>
        /// 获取一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>成功返回true</returns>
        bool LockTake<T>(string key, T value, TimeSpan expiry);

        /// <summary>
        /// 获取一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>成功返回true</returns>
        //bool LockTake<T>(string key,int timeSeconds);

        /// <summary>
        /// 异步获取一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>成功返回true</returns>
        Task<bool> LockTakeAsync<T>(string key, T value, TimeSpan expiry);

        //bool LockTakeAsync<T>(string key, int timeSeconds);

        /// <summary>
        /// 释放一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        bool LockRelease<T>(string key, T value);

        /// <summary>
        /// 异步释放一个锁
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <returns>成功返回true</returns>
        Task<bool> LockReleaseAsync<T>(string key, T value);

        #endregion lock

        #region Geo

        /// <summary>
        /// 将指定的地理空间位置（纬度、经度、成员）添加到指定的key中。这些数据将会存储到sorted set这样的目的是为了方便使用GEORADIUS或者GEORADIUSBYMEMBER命令对数据进行半径查询等操作。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="geoInfos">地理空间位置</param>
        /// <returns></returns>
        long GeoAdd<T>(string key, GeoInfo<T>[] geoInfos);

        /// <summary>
        /// 将指定的地理空间位置（纬度、经度、成员）添加到指定的key中。这些数据将会存储到sorted set这样的目的是为了方便使用GEORADIUS或者GEORADIUSBYMEMBER命令对数据进行半径查询等操作。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="geoInfo">地理空间位置</param>
        /// <returns></returns>
        bool GeoAdd<T>(string key, GeoInfo<T> geoInfo);

        /// <summary>
        /// 将指定的地理空间位置（纬度、经度、成员）添加到指定的key中。这些数据将会存储到sorted set这样的目的是为了方便使用GEORADIUS或者GEORADIUSBYMEMBER命令对数据进行半径查询等操作。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="member">成员</param>
        /// <returns></returns>
        bool GeoAdd<T>(string key, double longitude, double latitude, T member);

        /// <summary>
        /// 将指定的地理空间位置（纬度、经度、成员）添加到指定的key中。这些数据将会存储到sorted set这样的目的是为了方便使用GEORADIUS或者GEORADIUSBYMEMBER命令对数据进行半径查询等操作。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="geoInfos">地理空间位置</param>
        /// <returns></returns>
        Task<long> GeoAddAsync<T>(string key, GeoInfo<T>[] geoInfos);

        /// <summary>
        /// 将指定的地理空间位置（纬度、经度、成员）添加到指定的key中。这些数据将会存储到sorted set这样的目的是为了方便使用GEORADIUS或者GEORADIUSBYMEMBER命令对数据进行半径查询等操作。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="geoInfo">地理空间位置</param>
        /// <returns></returns>
        Task<bool> GeoAddAsync<T>(string key, GeoInfo<T> geoInfo);

        /// <summary>
        /// 将指定的地理空间位置（纬度、经度、成员）添加到指定的key中。这些数据将会存储到sorted set这样的目的是为了方便使用GEORADIUS或者GEORADIUSBYMEMBER命令对数据进行半径查询等操作。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="member">成员</param>
        /// <returns></returns>
        Task<bool> GeoAddAsync<T>(string key, double longitude, double latitude, T member);

        /// <summary>
        /// 返回两个给定位置之间的距离。如果两个位置之间的其中一个不存在， 那么命令返回空值。GEODIST 命令在计算距离时会假设地球为完美的球形， 在极限情况下，这一假设最大会造成 0.5% 的误差。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="member1">成员1</param>
        /// <param name="member2">成员2</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <returns>计算出的距离会以双精度浮点数的形式被返回。 如果给定的位置元素不存在， 那么命令返回空值。</returns>
        double? GeoDist<T>(string key, T member1, T member2, GeoDistUnit unit = GeoDistUnit.Meters);

        /// <summary>
        /// 返回两个给定位置之间的距离。如果两个位置之间的其中一个不存在， 那么命令返回空值。GEODIST 命令在计算距离时会假设地球为完美的球形， 在极限情况下，这一假设最大会造成 0.5% 的误差。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="member1">成员1</param>
        /// <param name="member2">成员2</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺</param>
        /// <returns>计算出的距离会以双精度浮点数的形式被返回。 如果给定的位置元素不存在， 那么命令返回空值。</returns>
        Task<double?> GeoDistAsync<T>(string key, T member1, T member2, GeoDistUnit unit = GeoDistUnit.Meters);

        /// <summary>
        /// 返回一个或多个位置元素的 Geohash 表示。通常使用表示位置的元素使用不同的技术，使用Geohash位置52点整数编码。由于编码和解码过程中所使用的初始最小和最大坐标不同，编码的编码也不同于标准。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="members">成员</param>
        /// <returns>一个数组， 数组的每个项都是一个 geohash 。 命令返回的 geohash 的位置与用户给定的位置元素的位置一一对应。</returns>
        string[] GeoHash<T>(string key, T[] members);

        /// <summary>
        /// 返回一个或多个位置元素的 Geohash 表示。通常使用表示位置的元素使用不同的技术，使用Geohash位置52点整数编码。由于编码和解码过程中所使用的初始最小和最大坐标不同，编码的编码也不同于标准。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="members">成员</param>
        /// <returns>一个数组， 数组的每个项都是一个 geohash 。 命令返回的 geohash 的位置与用户给定的位置元素的位置一一对应。</returns>
        Task<string[]> GeoHashAsync<T>(string key, T[] members);

        /// <summary>
        /// 从key里返回所有给定位置元素的位置（经度和纬度）。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="members">成员</param>
        /// <returns>GEOPOS 命令返回一个数组， 数组中的每个项都由两个元素组成： 第一个元素为给定位置元素的经度， 而第二个元素则为给定位置元素的纬度。当给定的位置元素不存在时，对应的数组项为空值。</returns>
        GeoPositionInfo?[] GeoPos<T>(string key, T[] members);

        /// <summary>
        /// 从key里返回所有给定位置元素的位置（经度和纬度）。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="members">成员</param>
        /// <returns>GEOPOS 命令返回一个数组， 数组中的每个项都由两个元素组成： 第一个元素为给定位置元素的经度， 而第二个元素则为给定位置元素的纬度。当给定的位置元素不存在时，对应的数组项为空值。</returns>
        Task<GeoPositionInfo?[]> GeoPosAsync<T>(string key, T[] members);

        /// <summary>
        /// 以给定的成员为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="member">成员</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时，即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        GeoRadiusResultInfo<T>[] GeoRadius<T>(string key, T member, double radius, GeoDistUnit unit = GeoDistUnit.Meters, int count = -1, Order? order = null);

        /// <summary>
        /// 以给定的成员为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="member">成员</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时，即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="order">排序</param>
        /// <returns>以给定的成员为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。</returns>
        Task<GeoRadiusResultInfo<T>[]> GeoRadiusAsync<T>(string key, T member, double radius, GeoDistUnit unit = GeoDistUnit.Meters, int count = -1, Order? order = null);

        /// <summary>
        /// 以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时，即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="order">排序</param>
        /// <returns>以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。</returns>
        GeoRadiusResultInfo<T>[] GeoRadius<T>(string key, double longitude, double latitude, double radius, GeoDistUnit unit = GeoDistUnit.Meters, int count = -1, Order? order = null);

        /// <summary>
        /// 以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时，即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="order">排序</param>
        /// <returns>以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。</returns>
        Task<GeoRadiusResultInfo<T>[]> GeoRadiusAsync<T>(string key, double longitude, double latitude, double radius, GeoDistUnit unit = GeoDistUnit.Meters, int count = -1, Order? order = null);

        /// <summary>
        /// 移除点
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="member">成员</param>
        /// <returns></returns>
        bool GeoRemove<T>(string key, T member);

        /// <summary>
        /// 移除点
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="member">成员</param>
        /// <returns></returns>
        Task<bool> GeoRemoveAsync<T>(string key, T member);

        #endregion Geo

        #region SortSet

        /// <summary>
        /// 计算在有序集合中指定区间分数的成员数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"> 分数最小值 double.MinValue</param>
        /// <param name="max">分数最大值 double.MaxValue</param>
        /// <returns>在有序集合中指定区间分数的成员数量</returns>
        long ZCount(string key, double min = double.NegativeInfinity, double max = double.PositiveInfinity);

        /// <summary>
        /// 计算在有序集合中指定区间分数的成员数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"> 分数最小值 double.MinValue</param>
        /// <param name="max">分数最大值 double.MaxValue</param>
        /// <returns>在有序集合中指定区间分数的成员数量</returns>
        Task<long> ZCountAsync(string key, double min = double.NegativeInfinity, double max = double.PositiveInfinity);

        /// <summary>
        /// 向有序集合添加一个或多个成员，或者更新已存在成员的分数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="scoreMembers"> 一个或多个成员分数</param>
        /// <returns></returns>
        long ZAdd<T>(string key, params (double, T)[] scoreMembers);

        /// <summary>
        /// 向有序集合添加一个或多个成员，或者更新已存在成员的分数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="scoreMembers"> 一个或多个成员分数</param>
        /// <returns></returns>
        Task<long> ZAddAsync<T>(string key, params (double, T)[] scoreMembers);

        /// <summary>
        /// 有序集合中对指定成员的分数加上增量 increment
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="memeber">成员</param>
        /// <param name="increment">增量值(默认=1)</param>
        /// <returns></returns>
        double ZIncrBy<T>(string key, T memeber, double increment = 1);

        /// <summary>
        /// 有序集合中对指定成员的分数加上增量 increment
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="memeber">成员</param>
        /// <param name="increment">增量值(默认=1)</param>
        /// <returns></returns>
        Task<double> ZIncrByAsync<T>(string key, T memeber, double increment = 1);

        /// <summary>
        /// 有序集合中对指定成员的分数加上减量 decrement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="memeber">成员</param>
        /// <param name="decrement">减量值(默认=1</param>
        /// <returns></returns>
        double ZDecrBy<T>(string key, T memeber, double decrement = 1);

        /// <summary>
        /// 有序集合中对指定成员的分数加上减量 decrement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="memeber">成员</param>
        /// <param name="decrement">减量值(默认=1</param>
        /// <returns></returns>
        Task<double> ZDecrByAsync<T>(string key, T memeber, double decrement = 1);

        /// <summary>
        /// 计算给定的一个或多个有序集的交集，将结果集存储在新的有序集合 destination 中
        /// </summary>
        /// <param name="destination">新的有序集合，不含prefix前辍</param>
        /// <param name="weights">使用 WEIGHTS 选项，你可以为 每个 给定有序集 分别 指定一个乘法因子。如果没有指定 WEIGHTS 选项，乘法因子默认设置为 1 。</param>
        /// <param name="aggregate">Sum | Min | Max</param>
        /// <param name="keys">一个或多个有序集合，不含prefix前辍</param>
        /// <returns></returns>
        long ZInterStore(string destination, double[] weights, RedisAggregate aggregate, params string[] keys);

        /// <summary>
        /// 计算给定的一个或多个有序集的交集，将结果集存储在新的有序集合 destination 中
        /// </summary>
        /// <param name="destination">新的有序集合，不含prefix前辍</param>
        /// <param name="weights">使用 WEIGHTS 选项，你可以为 每个 给定有序集 分别 指定一个乘法因子。如果没有指定 WEIGHTS 选项，乘法因子默认设置为 1 。</param>
        /// <param name="aggregate">Sum | Min | Max</param>
        /// <param name="keys">一个或多个有序集合，不含prefix前辍</param>
        /// <returns></returns>
        Task<long> ZInterStoreAsync(string destination, double[] weights, RedisAggregate aggregate, params string[] keys);

        /// <summary>
        /// 当有序集合的所有成员都具有相同的分值时，有序集合的元素会根据成员的字典序来进行排序，这个命令可以返回给定的有序集合键 key 中，值介于 min 和 max之间成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        long ZLexCount(string key, double min, double max);

        /// <summary>
        /// 当有序集合的所有成员都具有相同的分值时，有序集合的元素会根据成员的字典序来进行排序，这个命令可以返回给定的有序集合键 key 中，值介于 min 和 max之间成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        Task<long> ZLexCountAsync(string key, double min, double max);

        /// <summary>
        /// 通过索引区间返回有序集合成指定区间内的成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        T[] ZRange<T>(string key, long start, long stop);

        /// <summary>
        /// 通过索引区间返回有序集合成指定区间内的成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<T[]> ZRangeAsync<T>(string key, long start, long stop);

        /// <summary>
        /// 当有序集合的所有成员都具有相同的分值时，有序集合的元素会根据成员的字典序来进行排序，这个命令可以返回给定的有序集合键 key 中，值介于 min 和 max之间成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="count">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        T[] ZRangeByLex<T>(string key, double min, double max, long? count = null, long offset = 0);

        /// <summary>
        /// 当有序集合的所有成员都具有相同的分值时，有序集合的元素会根据成员的字典序来进行排序，这个命令可以返回给定的有序集合键 key 中，值介于 min 和 max之间成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="count">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        Task<T[]> ZRangeByLexAsync<T>(string key, double min, double max, long? count = null, long offset = 0);

        /// <summary>
        /// 通过分数返回有序集合指定区间内的成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        T[] ZRangeByScore<T>(string key, double min, double max, long? count = null, long offset = 0);

        /// <summary>
        /// 通过分数返回有序集合指定区间内的成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        Task<T[]> ZRangeByScoreAsync<T>(string key, double min, double max, long? count = null, long offset = 0);

        /// <summary>
        ///  通过分数返回有序集合指定区间内的成员和分数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        (T member, double score)[] ZRangeByScoreWithScores<T>(string key, double min, double max, long? count = null, long offset = 0);

        /// <summary>
        ///  通过分数返回有序集合指定区间内的成员和分数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        Task<(T member, double score)[]> ZRangeByScoreWithScoresAsync<T>(string key, double min, double max, long? count = null, long offset = 0);

        /// <summary>
        /// 通过分数返回有序集合指定区间内的成员和分数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        (T member, double score)[] ZRangeWithScores<T>(string key, long start, long stop);

        /// <summary>
        /// 通过分数返回有序集合指定区间内的成员和分数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        Task<(T member, double score)[]> ZRangeWithScoresAsync<T>(string key, long start, long stop);

        /// <summary>
        /// 返回有序集合中指定成员的索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        long? ZRank<T>(string key, T member);

        /// <summary>
        /// 返回有序集合中指定成员的索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<long?> ZRankAsync<T>(string key, T member);

        /// <summary>
        /// 移除有序集合中的一个或多个成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        long ZRem<T>(string key, params T[] member);

        /// <summary>
        /// 移除有序集合中的一个或多个成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<long> ZRemAsync<T>(string key, params T[] member);

        /// <summary>
        /// 当有序集合的所有成员都具有相同的分值时，有序集合的元素会根据成员的字典序来进行排序，这个命令可以返回给定的有序集合键 key 中，值介于 min 和 max之间成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        long ZRemRangeByLex(string key, double min, double max);

        /// <summary>
        /// 当有序集合的所有成员都具有相同的分值时，有序集合的元素会根据成员的字典序来进行排序，这个命令可以返回给定的有序集合键 key 中，值介于 min 和 max之间成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        Task<long> ZRemRangeByLexAsync(string key, double min, double max);

        /// <summary>
        /// 移除有序集合中给定的排名区间的所有成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        long ZRemRangeByRank(string key, long start, long stop);

        /// <summary>
        /// 移除有序集合中给定的排名区间的所有成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        Task<long> ZRemRangeByRankAsync(string key, long start, long stop);

        /// <summary>
        /// 移除有序集合中给定的分数区间的所有成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        long ZRemRangeByScore(string key, double min, double max);

        /// <summary>
        /// 移除有序集合中给定的分数区间的所有成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        Task<long> ZRemRangeByScoreAsync(string key, double min, double max);

        /// <summary>
        /// 返回有序集中指定区间内的成员，通过索引，分数从高到底
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        T[] ZRevRange<T>(string key, long start, long stop);

        /// <summary>
        /// 返回有序集中指定区间内的成员，通过索引，分数从高到底
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        Task<T[]> ZRevRangeAsync<T>(string key, long start, long stop);

        /// <summary>
        /// 返回有序集中指定分数区间内的成员，分数从高到低排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        T[] ZRevRangeByScore<T>(string key, double max, double min, long? count = null, long offset = 0);

        /// <summary>
        /// 返回有序集中指定分数区间内的成员，分数从高到低排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        Task<T[]> ZRevRangeByScoreAsync<T>(string key, double max, double min, long? count = null, long offset = 0);

        /// <summary>
        /// 返回有序集中指定分数区间内的成员，分数从高到低排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        (T member, double score)[] ZRevRangeByScoreWithScores<T>(string key, double max, double min, long? count = null, long offset = 0);

        /// <summary>
        /// 返回有序集中指定分数区间内的成员，分数从高到低排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        Task<(T member, double score)[]> ZRevRangeByScoreWithScoresAsync<T>(string key, double max, double min, long? count = null, long offset = 0);

        /// <summary>
        /// 返回有序集中指定分数区间内的成员，分数从高到低排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        (T member, double score)[] ZRevRangeWithScores<T>(string key, long start, long stop);

        /// <summary>
        /// 返回有序集中指定分数区间内的成员，分数从高到低排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        Task<(T member, double score)[]> ZRevRangeWithScoresAsync<T>(string key, long start, long stop);

        /// <summary>
        /// 返回有序集合中指定成员的排名，有序集成员按分数值递减(从大到小)排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        long? ZRevRank<T>(string key, T member);

        /// <summary>
        /// 返回有序集合中指定成员的排名，有序集成员按分数值递减(从大到小)排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<long?> ZRevRankAsync<T>(string key, T member);

        /// <summary>
        /// 迭代有序集合中的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="cursor"></param>
        /// <param name="pattern"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        (long cursor, (T member, double score)[] items)? ZScan<T>(string key, int cursor, string pattern = null, int? count = null);

        /// <summary>
        /// 迭代有序集合中的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="cursor"></param>
        /// <param name="pattern"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<(long cursor, (T member, double score)[] items)?> ZScanAsync<T>(string key, int cursor, string pattern = null, int? count = null);

        /// <summary>
        /// 返回有序集中，成员的分数值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        double? ZScore<T>(string key, T member);

        /// <summary>
        /// 返回有序集中，成员的分数值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<double?> ZScoreAsync<T>(string key, T member);

        /// <summary>
        /// 计算给定的一个或多个有序集的并集，将结果集存储在新的有序集合 destination 中
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="weights"></param>
        /// <param name="aggregate"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        long ZUnionStore(string destination, double[] weights, RedisAggregate aggregate, params string[] keys);

        /// <summary>
        /// 计算给定的一个或多个有序集的并集，将结果集存储在新的有序集合 destination 中
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="weights"></param>
        /// <param name="aggregate"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<long> ZUnionStoreAsync(string destination, double[] weights, RedisAggregate aggregate, params string[] keys);

        #endregion SortSet
    }
}
