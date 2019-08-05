using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yaeher.CsRedisCore.Options
{
    public class RedisOption : IOptions<RedisOption>
    {
        /// <summary>
        /// 连接信息
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 分隔符
        /// </summary>
        public string NamespaceSplitSymbol { get; set; } = ":";

        public int? expireSeconds { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }

        public RedisOption Value
        {
            get { return this; }
        }

    }
}
