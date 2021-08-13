using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace BestGameEver
{
    public class UT_Vector2IntSerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Vector2Int vec2 = (Vector2Int)obj;
            info.AddValue("x", vec2.x);
            info.AddValue("y", vec2.y);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Vector2Int vec2 = (Vector2Int)obj;
            vec2.x = (int)info.GetValue("x", typeof(int));
            vec2.y = (int)info.GetValue("y", typeof(int));
            return vec2;
        }
    }
}
