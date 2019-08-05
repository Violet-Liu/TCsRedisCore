using CSRedis;
using System;
using System.Collections.Generic;
using System.Text;
using Yaeher.CsRedisCore.Geo;

namespace Yaeher.CsRedisCore.Enums
{
    public static class InternalExtension
    {
        public static GeoOrderBy? Convert(this Order? order)
        {
            GeoOrderBy? orderBy = null;
            if (order != null)
            {
                orderBy = (GeoOrderBy)(int)order.Value;
            }
            return orderBy;
        }

        public static GeoUnit Convert(this GeoDistUnit geoDistUnit)
        {
            return (GeoUnit)(int)geoDistUnit;
        }

        public static long? Convert(this int count)
        {
            long? countValue = null;
            if (count != -1)
            {
                countValue = count;
            }
            return countValue;
        }

        public static CSRedis.RedisAggregate Convert(this RedisAggregate redisAggregate)
        {
            return (CSRedis.RedisAggregate)(int)redisAggregate;
        }
    }
}
