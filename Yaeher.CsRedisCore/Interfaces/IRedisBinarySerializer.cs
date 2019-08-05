using System;
using System.Collections.Generic;
using System.Text;

namespace Yaeher.CsRedisCore
{
    public interface IRedisBinarySerializer
    {/// <summary>
     /// 反序列化
     /// </summary>
     /// <typeparam name="T">对象类型</typeparam>
     /// <param name="serializedObject">字节数组</param>
     /// <returns>对象</returns>
        T Deserialize<T>(byte[] serializedObject);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="item">对象值</param>
        /// <returns>字节数组</returns>
		byte[] Serialize<T>(T item);
    }
}
