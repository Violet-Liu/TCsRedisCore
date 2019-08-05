using CSRedis;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
using System.Linq;
using System.Threading.Tasks;
using Yaeher.CsRedisCore.Enums;
using Yaeher.CsRedisCore.Geo;
using Order = Yaeher.CsRedisCore.Enums.Order;

namespace Yaeher.CsRedisCore
{
    public partial class CsRedisRedisClient : IRedisClient
    {
        #region Geo

        public long GeoAdd<T>(string key, GeoInfo<T>[] geoInfos)
        {
            (double longitude, double latitude, object member)[] values = geoInfos?.Select(m => (m.Longitude, m.Latitude, (object)Serializer.Serialize(m.Member)))?.ToArray();
            return Connection.GeoAdd(SetPrefix(key), values: values);
        }

        public bool GeoAdd<T>(string key, GeoInfo<T> geoInfo)
        {
            return Connection.GeoAdd(SetPrefix(key), geoInfo.Longitude, geoInfo.Latitude, Serializer.Serialize(geoInfo.Member));
        }

        public bool GeoAdd<T>(string key, double longitude, double latitude, T member)
        {
            return Connection.GeoAdd(SetPrefix(key), longitude, latitude, Serializer.Serialize(member));
        }

        public Task<long> GeoAddAsync<T>(string key, GeoInfo<T>[] geoInfos)
        {
            (double longitude, double latitude, object member)[] values = geoInfos?.Select(m => (m.Longitude, m.Latitude, (object)Serializer.Serialize(m.Member)))?.ToArray();
            return Connection.GeoAddAsync(SetPrefix(key), values);
        }

        public Task<bool> GeoAddAsync<T>(string key, GeoInfo<T> geoInfo)
        {
            return Connection.GeoAddAsync(SetPrefix(key), geoInfo.Longitude, geoInfo.Latitude, Serializer.Serialize(geoInfo.Member));
        }

        public Task<bool> GeoAddAsync<T>(string key, double longitude, double latitude, T member)
        {
            return Connection.GeoAddAsync(SetPrefix(key), longitude, latitude, Serializer.Serialize(member));
        }

        public double? GeoDist<T>(string key, T member1, T member2, GeoDistUnit unit = GeoDistUnit.Meters)
        {
            return Connection.GeoDist(SetPrefix(key), Serializer.Serialize(member1), Serializer.Serialize(member2), unit: (GeoUnit)(int)unit);
        }

        public Task<double?> GeoDistAsync<T>(string key, T member1, T member2, GeoDistUnit unit = GeoDistUnit.Meters)
        {
            return Connection.GeoDistAsync(SetPrefix(key), Serializer.Serialize(member1), Serializer.Serialize(member2), unit: (GeoUnit)(int)unit);
        }

        public string[] GeoHash<T>(string key, T[] members)
        {
            return Connection.GeoHash(SetPrefix(key), members.Select(m => Serializer.Serialize(m)).ToArray());
        }

        public Task<string[]> GeoHashAsync<T>(string key, T[] members)
        {
            return Connection.GeoHashAsync(SetPrefix(key), members.Select(m => Serializer.Serialize(m)).ToArray());
        }

        public GeoPositionInfo?[] GeoPos<T>(string key, T[] members)
        {
            var positions = Connection.GeoPos(SetPrefix(key), members.Select(m => Serializer.Serialize(m)).ToArray());
            if (positions != null)
            {
                var geoPositionInfos = new GeoPositionInfo?[positions.Length];
                for (int i = 0; i < positions.Length; i++)
                {
                    if (positions[i] != null)
                    {
                        geoPositionInfos[i] = new GeoPositionInfo { Latitude = positions[i].Value.latitude, Longitude = positions[i].Value.longitude };
                    }
                    else
                    {
                        geoPositionInfos[i] = null;
                    }
                }
                return geoPositionInfos;
            }
            return null;
        }

        public async Task<GeoPositionInfo?[]> GeoPosAsync<T>(string key, T[] members)
        {
            var positions = await Connection.GeoPosAsync(SetPrefix(key), members.Select(m => Serializer.Serialize(m)).ToArray());
            if (positions != null)
            {
                var geoPositionInfos = new GeoPositionInfo?[positions.Length];
                for (int i = 0; i < positions.Length; i++)
                {
                    if (positions[i] != null)
                    {
                        geoPositionInfos[i] = new GeoPositionInfo { Latitude = positions[i].Value.latitude, Longitude = positions[i].Value.longitude };
                    }
                    else
                    {
                        geoPositionInfos[i] = null;
                    }
                }
                return geoPositionInfos;
            }
            return null;
        }

        public GeoRadiusResultInfo<T>[] GeoRadius<T>(string key, T member, double radius, GeoDistUnit unit = GeoDistUnit.Meters, int count = -1, Order? order = null)
        {
            var result = Connection.GeoRadiusByMemberWithDistAndCoord<byte[]>(SetPrefix(key), Serializer.Serialize(member), radius, unit: unit.Convert(), count: count.Convert(), sorting: order.Convert());
            if (result != null)
            {
                var geoRadiusResult = new GeoRadiusResultInfo<T>[result.Length];
                for (int i = 0; i < result.Length; i++)
                {
                    geoRadiusResult[i] = new GeoRadiusResultInfo<T>
                    {
                        Distance = result[i].dist,
                        Member = Serializer.Deserialize<T>(result[i].member),
                        Position = new GeoPositionInfo
                        {
                            Latitude = result[i].latitude,
                            Longitude = result[i].longitude
                        }
                    };
                }
                return geoRadiusResult;
            }
            return null;
        }

        public async Task<GeoRadiusResultInfo<T>[]> GeoRadiusAsync<T>(string key, T member, double radius, GeoDistUnit unit = GeoDistUnit.Meters, int count = -1, Order? order = null)
        {
            var result = await Connection.GeoRadiusByMemberWithDistAndCoordAsync<byte[]>(SetPrefix(key), Serializer.Serialize(member), radius, unit: unit.Convert(), count: count.Convert(), sorting: order.Convert());
            if (result != null)
            {
                var geoRadiusResult = new GeoRadiusResultInfo<T>[result.Length];
                for (int i = 0; i < result.Length; i++)
                {
                    geoRadiusResult[i] = new GeoRadiusResultInfo<T>
                    {
                        Distance = result[i].dist,
                        Member = Serializer.Deserialize<T>(result[i].member),
                        Position = new GeoPositionInfo
                        {
                            Latitude = result[i].latitude,
                            Longitude = result[i].longitude
                        }
                    };
                }
                return geoRadiusResult;
            }
            return null;
        }

        public GeoRadiusResultInfo<T>[] GeoRadius<T>(string key, double longitude, double latitude, double radius, GeoDistUnit unit = GeoDistUnit.Meters, int count = -1, Order? order = null)
        {
            var result = Connection.GeoRadiusWithDistAndCoord<byte[]>(SetPrefix(key), longitude, latitude, radius, unit: unit.Convert(), count: count.Convert(), sorting: order.Convert());
            if (result != null)
            {
                var geoRadiusResult = new GeoRadiusResultInfo<T>[result.Length];
                for (int i = 0; i < result.Length; i++)
                {
                    geoRadiusResult[i] = new GeoRadiusResultInfo<T>
                    {
                        Distance = result[i].dist,
                        Member = Serializer.Deserialize<T>(result[i].member),
                        Position = new GeoPositionInfo
                        {
                            Latitude = result[i].latitude,
                            Longitude = result[i].longitude
                        }
                    };
                }
                return geoRadiusResult;
            }
            return null;
        }

        public async Task<GeoRadiusResultInfo<T>[]> GeoRadiusAsync<T>(string key, double longitude, double latitude, double radius, GeoDistUnit unit = GeoDistUnit.Meters, int count = -1, Order? order = null)
        {
            var result = await Connection.GeoRadiusWithDistAndCoordAsync<byte[]>(SetPrefix(key), longitude, latitude, radius, unit: unit.Convert(), count: count.Convert(), sorting: order.Convert());
            if (result != null)
            {
                var geoRadiusResult = new GeoRadiusResultInfo<T>[result.Length];
                for (int i = 0; i < result.Length; i++)
                {
                    geoRadiusResult[i] = new GeoRadiusResultInfo<T>
                    {
                        Distance = result[i].dist,
                        Member = Serializer.Deserialize<T>(result[i].member),
                        Position = new GeoPositionInfo
                        {
                            Latitude = result[i].latitude,
                            Longitude = result[i].longitude
                        }
                    };
                }
                return geoRadiusResult;
            }
            return null;
        }

        public bool GeoRemove<T>(string key, T member)
        {
            return Connection.ZRem(SetPrefix(key), Serializer.Serialize(member)) > 0;
        }

        public async Task<bool> GeoRemoveAsync<T>(string key, T member)
        {
            return await Connection.ZRemAsync(SetPrefix(key), Serializer.Serialize(member)) > 0;
        }

        #endregion Geo
    }
}