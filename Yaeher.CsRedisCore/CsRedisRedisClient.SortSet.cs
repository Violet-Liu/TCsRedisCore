using System.Linq;
using System.Threading.Tasks;
using Yaeher.Common.Extensions;
using Yaeher.CsRedisCore.Enums;

namespace Yaeher.CsRedisCore
{
    public partial class CsRedisRedisClient : IRedisClient
    {
        public long ZCount(string key, double min = double.NegativeInfinity, double max = double.PositiveInfinity)
        {
            return Connection.ZCount(SetPrefix(key), min, max);
        }

        public Task<long> ZCountAsync(string key, double min = double.NegativeInfinity, double max = double.PositiveInfinity)
        {
            return Connection.ZCountAsync(SetPrefix(key), min, max);
        }

        public long ZAdd<T>(string key, params (double, T)[] scoreMembers)
        {
            return Connection.ZAdd(SetPrefix(key), scoreMembers.Select(m => (m.Item1, (object)Serializer.Serialize(m.Item2))).ToArray());
        }

        public Task<long> ZAddAsync<T>(string key, params (double, T)[] scoreMembers)
        {
            return Connection.ZAddAsync(SetPrefix(key), scoreMembers.Select(m => (m.Item1, (object)Serializer.Serialize(m.Item2))).ToArray());
        }

        public double ZIncrBy<T>(string key, T memeber, double increment = 1)
        {
            return Connection.ZIncrBy(SetPrefix(key), Serializer.Serialize(memeber).ToStr(), increment);
        }

        public Task<double> ZIncrByAsync<T>(string key, T memeber, double increment = 1)
        {
            return Connection.ZIncrByAsync(SetPrefix(key), Serializer.Serialize(memeber).ToStr(), increment);
        }

        public double ZDecrBy<T>(string key, T memeber, double decrement = 1)
        {
            return Connection.ZIncrBy(SetPrefix(key), Serializer.Serialize(memeber).ToStr(), decrement * -1);
        }

        public Task<double> ZDecrByAsync<T>(string key, T memeber, double decrement = 1)
        {
            return Connection.ZIncrByAsync(SetPrefix(key), Serializer.Serialize(memeber).ToStr(), decrement * -1);
        }

        public long ZInterStore(string destination, double[] weights, RedisAggregate aggregate, params string[] keys)
        {
            return Connection.ZInterStore(SetPrefix(destination), weights, aggregate.Convert(), keys: keys.Select(m => SetPrefix(m)).ToArray());
        }

        public Task<long> ZInterStoreAsync(string destination, double[] weights, RedisAggregate aggregate, params string[] keys)
        {
            return Connection.ZInterStoreAsync(SetPrefix(destination), weights, aggregate.Convert(), keys: keys.Select(m => SetPrefix(m)).ToArray());
        }

        public long ZLexCount(string key, double min, double max)
        {
            return Connection.ZLexCount(SetPrefix(key), min.ToString(), max.ToString());
        }

        public Task<long> ZLexCountAsync(string key, double min, double max)
        {
            return Connection.ZLexCountAsync(SetPrefix(key), min.ToString(), max.ToString());
        }

        public T[] ZRange<T>(string key, long start, long stop)
        {
            return Connection.ZRange<byte[]>(SetPrefix(key), start, stop)?.Select(m => Serializer.Deserialize<T>(m)).ToArray();
        }

        public async Task<T[]> ZRangeAsync<T>(string key, long start, long stop)
        {
            var result = await Connection.ZRangeAsync<byte[]>(SetPrefix(key), start, stop);
            return result?.Select(m => Serializer.Deserialize<T>(m)).ToArray();
        }

        public T[] ZRangeByLex<T>(string key, double min, double max, long? count = null, long offset = 0)
        {
            return Connection.ZRangeByLex<byte[]>(SetPrefix(key), min.ToString(), max.ToString(), count: count, offset: offset)?.Select(m => Serializer.Deserialize<T>(m)).ToArray();
        }

        public async Task<T[]> ZRangeByLexAsync<T>(string key, double min, double max, long? count = null, long offset = 0)
        {
            var result = await Connection.ZRangeByLexAsync<byte[]>(SetPrefix(key), min.ToString(), max.ToString(), count: count, offset: offset);
            return result?.Select(m => Serializer.Deserialize<T>(m)).ToArray();
        }

        public T[] ZRangeByScore<T>(string key, double min, double max, long? count = null, long offset = 0)
        {
            return Connection.ZRangeByScore<byte[]>(SetPrefix(key), min, max, count: count, offset: offset)?.Select(m => Serializer.Deserialize<T>(m)).ToArray();
        }

        public async Task<T[]> ZRangeByScoreAsync<T>(string key, double min, double max, long? count = null, long offset = 0)
        {
            var result = await Connection.ZRangeByScoreAsync<byte[]>(SetPrefix(key), min, max, count: count, offset: offset);
            return result?.Select(m => Serializer.Deserialize<T>(m)).ToArray();
        }

        public (T member, double score)[] ZRangeByScoreWithScores<T>(string key, double min, double max, long? count = null, long offset = 0)
        {
            var result = Connection.ZRangeByScoreWithScores<byte[]>(SetPrefix(key), min, max, count: count, offset: offset);
            return result?.Select(m => (Serializer.Deserialize<T>(m.member), m.score)).ToArray();
        }

        public async Task<(T member, double score)[]> ZRangeByScoreWithScoresAsync<T>(string key, double min, double max, long? count = null, long offset = 0)
        {
            var result = await Connection.ZRangeByScoreWithScoresAsync<byte[]>(SetPrefix(key), min, max, count: count, offset: offset);
            return result?.Select(m => (Serializer.Deserialize<T>(m.member), m.score)).ToArray();
        }

        public (T member, double score)[] ZRangeWithScores<T>(string key, long start, long stop)
        {
            var result = Connection.ZRangeWithScores<byte[]>(SetPrefix(key), start, stop);
            return result?.Select(m => (Serializer.Deserialize<T>(m.member), m.score)).ToArray();
        }

        public async Task<(T member, double score)[]> ZRangeWithScoresAsync<T>(string key, long start, long stop)
        {
            var result = await Connection.ZRangeWithScoresAsync<byte[]>(SetPrefix(key), start, stop);
            return result?.Select(m => (Serializer.Deserialize<T>(m.member), m.score)).ToArray();
        }

        public long? ZRank<T>(string key, T member)
        {
            return Connection.ZRank(SetPrefix(key), Serializer.Serialize(member));
        }

        public Task<long?> ZRankAsync<T>(string key, T member)
        {
            return Connection.ZRankAsync(SetPrefix(key), Serializer.Serialize(member));
        }

        public long ZRem<T>(string key, params T[] member)
        {
            return Connection.ZRem(SetPrefix(key), member.Select(m => Serializer.Serialize(m)).ToArray());
        }

        public Task<long> ZRemAsync<T>(string key, params T[] member)
        {
            return Connection.ZRemAsync(SetPrefix(key), member.Select(m => Serializer.Serialize(m)).ToArray());
        }

        public long ZRemRangeByLex(string key, double min, double max)
        {
            return Connection.ZRemRangeByLex(SetPrefix(key), min.ToString(), max.ToString());
        }

        public Task<long> ZRemRangeByLexAsync(string key, double min, double max)
        {
            return Connection.ZRemRangeByLexAsync(SetPrefix(key), min.ToString(), max.ToString());
        }

        public long ZRemRangeByRank(string key, long start, long stop)
        {
            return Connection.ZRemRangeByRank(SetPrefix(key), start, stop);
        }

        public Task<long> ZRemRangeByRankAsync(string key, long start, long stop)
        {
            return Connection.ZRemRangeByRankAsync(SetPrefix(key), start, stop);
        }

        public long ZRemRangeByScore(string key, double min, double max)
        {
            return Connection.ZRemRangeByScore(SetPrefix(key), min, max);
        }

        public Task<long> ZRemRangeByScoreAsync(string key, double min, double max)
        {
            return Connection.ZRemRangeByScoreAsync(SetPrefix(key), min, max);
        }

        public T[] ZRevRange<T>(string key, long start, long stop)
        {
            var data = Connection.ZRevRange<byte[]>(SetPrefix(key), start, stop);
            return data.Select(m => Serializer.Deserialize<T>(m)).ToArray();
        }

        public async Task<T[]> ZRevRangeAsync<T>(string key, long start, long stop)
        {
            var data = await Connection.ZRevRangeAsync<byte[]>(SetPrefix(key), start, stop);
            return data.Select(m => Serializer.Deserialize<T>(m)).ToArray();
        }

        public T[] ZRevRangeByScore<T>(string key, double max, double min, long? count = null, long offset = 0)
        {
            var data = Connection.ZRevRangeByScore<byte[]>(SetPrefix(key), max, min, count: count, offset: offset);
            return data.Select(m => Serializer.Deserialize<T>(m)).ToArray();
        }

        public async Task<T[]> ZRevRangeByScoreAsync<T>(string key, double max, double min, long? count = null, long offset = 0)
        {
            var data = await Connection.ZRevRangeByScoreAsync<byte[]>(SetPrefix(key), max, min, count: count, offset: offset);
            return data.Select(m => Serializer.Deserialize<T>(m)).ToArray();
        }

        public (T member, double score)[] ZRevRangeByScoreWithScores<T>(string key, double max, double min, long? count = null, long offset = 0)
        {
            var result = Connection.ZRevRangeByScoreWithScores<byte[]>(SetPrefix(key), max, min, count: count, offset: offset);
            return result?.Select(m => (Serializer.Deserialize<T>(m.member), m.score)).ToArray();
        }

        public async Task<(T member, double score)[]> ZRevRangeByScoreWithScoresAsync<T>(string key, double max, double min, long? count = null, long offset = 0)
        {
            var result = await Connection.ZRevRangeByScoreWithScoresAsync<byte[]>(SetPrefix(key), max, min, count: count, offset: offset);
            return result?.Select(m => (Serializer.Deserialize<T>(m.member), m.score)).ToArray();
        }

        public (T member, double score)[] ZRevRangeWithScores<T>(string key, long start, long stop)
        {
            var result = Connection.ZRevRangeWithScores<byte[]>(SetPrefix(key), start, stop);
            return result?.Select(m => (Serializer.Deserialize<T>(m.member), m.score)).ToArray();
        }

        public async Task<(T member, double score)[]> ZRevRangeWithScoresAsync<T>(string key, long start, long stop)
        {
            var result = await Connection.ZRevRangeWithScoresAsync<byte[]>(SetPrefix(key), start, stop);
            return result?.Select(m => (Serializer.Deserialize<T>(m.member), m.score)).ToArray();
        }

        public long? ZRevRank<T>(string key, T member)
        {
            return Connection.ZRevRank(SetPrefix(key), Serializer.Serialize(member));
        }

        public Task<long?> ZRevRankAsync<T>(string key, T member)
        {
            return Connection.ZRevRankAsync(SetPrefix(key), Serializer.Serialize(member));
        }

        public (long cursor, (T member, double score)[] items)? ZScan<T>(string key, int cursor, string pattern = null, int? count = null)
        {
            var result = Connection.ZScan<byte[]>(SetPrefix(key), cursor, pattern: pattern, count: count);
            if (result != null)
                return (result.Cursor, result.Items.Select(m => (Serializer.Deserialize<T>(m.member), m.score)).ToArray());
            return null;
        }

        public async Task<(long cursor, (T member, double score)[] items)?> ZScanAsync<T>(string key, int cursor, string pattern = null, int? count = null)
        {
            var result = await Connection.ZScanAsync<byte[]>(SetPrefix(key), cursor, pattern: pattern, count: count);
            if (result != null)
                return (result.Cursor, result.Items.Select(m => (Serializer.Deserialize<T>(m.member), m.score)).ToArray());
            return null;
        }

        public double? ZScore<T>(string key, T member)
        {
            return Connection.ZScore(SetPrefix(key), Serializer.Serialize(member));
        }

        public Task<double?> ZScoreAsync<T>(string key, T member)
        {
            return Connection.ZScoreAsync(SetPrefix(key), Serializer.Serialize(member));
        }

        public long ZUnionStore(string destination, double[] weights, RedisAggregate aggregate, params string[] keys)
        {
            return Connection.ZUnionStore(SetPrefix(destination), weights, aggregate.Convert(), keys: keys.Select(m => SetPrefix(m)).ToArray());
        }

        public Task<long> ZUnionStoreAsync(string destination, double[] weights, RedisAggregate aggregate, params string[] keys)
        {
            return Connection.ZUnionStoreAsync(SetPrefix(destination), weights, aggregate.Convert(), keys: keys.Select(m => SetPrefix(m)).ToArray());
        }
    }
}