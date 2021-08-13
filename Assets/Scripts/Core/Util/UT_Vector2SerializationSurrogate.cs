
using UnityEngine;
using System.Runtime.Serialization;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UT_Vector2SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Vector2 vec2 = (Vector2)obj;
            info.AddValue("x", vec2.x);
            info.AddValue("y", vec2.y);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Vector2 vec2 = (Vector2)obj;
            vec2.x = (float)info.GetValue("x", typeof(float));
            vec2.y = (float)info.GetValue("y", typeof(float));
            return vec2;
        }
    }

}