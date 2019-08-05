using System;
using System.Collections.Generic;
using System.Text;
using Yaeher.Common.Extensions;

namespace Yaeher.CsRedisCore
{

    public class JsonRedisBinarySerializer : IRedisBinarySerializer
    {
        public virtual T Deserialize<T>(byte[] serializedObject)
        {
            return serializedObject.ToStr().ToObject<T>();
        }

        public virtual byte[] Serialize<T>(T item)
        {
            return item.ToJson().ToBytes();
        }
    }
}
