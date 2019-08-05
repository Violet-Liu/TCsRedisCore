using System;
using System.Collections.Generic;
using System.Text;

namespace Yaeher.CsRedisCore.Enums
{
    public enum RedisSaveType
    {
        /// <summary>
        /// Instruct Redis to start an Append Only File rewrite process. The rewrite will create a small optimized version of the current Append Only File.
        /// <see cref=" http://redis.io/commands/bgrewriteaof"/>
        /// </summary>
        BackgroundRewriteAppendOnlyFile = 0,

        /// <summary>
        /// Save the DB in background. The OK code is immediately returned. Redis forks,
        /// the parent continues to serve the clients, the child saves the DB on disk then  exits.
        ///  A client my be able to check if the operation succeeded using the LASTSAVE command.
        /// <see cref="http://redis.io/commands/bgsave"/>
        /// </summary>
        BackgroundSave = 1
    }

    public enum Order
    {
        Ascending = 0,

        Descending = 1
    }

    public enum RedisAggregate
    {
        /// <summary>
        /// Sum
        /// </summary>
        Sum = 0,

        /// <summary>
        /// Min
        /// </summary>
        Min = 1,

        /// <summary>
        /// Max
        /// </summary>
        Max = 2
    }
}
