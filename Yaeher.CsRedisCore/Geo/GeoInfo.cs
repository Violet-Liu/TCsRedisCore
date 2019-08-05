using System;
using System.Collections.Generic;
using System.Text;

namespace Yaeher.CsRedisCore.Geo
{
    public class GeoInfo<T>
    {
        public T Member { get; set; }

        public GeoPositionInfo Position { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }

    public struct GeoRadiusResultInfo<T>
    {
        public T Member { get; set; }

        public double? Distance { get; set; }

        public long? Hash { get; set; }

        public GeoPositionInfo? Position { get; set; }
    }

    public enum GeoDistUnit
    {
        /// <summary>
        /// Meters
        /// </summary>
        Meters = 0,

        /// <summary>
        /// Kilometers
        /// </summary>
        Kilometers = 1,

        /// <summary>
        /// Miles
        /// </summary>
        Miles = 2,

        /// <summary>
        /// Feet
        /// </summary>
        Feet = 3
    }
}
