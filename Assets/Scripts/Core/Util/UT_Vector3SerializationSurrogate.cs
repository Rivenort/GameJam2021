
using System.Runtime.Serialization;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UT_Vector3SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Vector3 vec3 = (Vector3)obj;
            info.AddValue("x", vec3.x);
            info.AddValue("y", vec3.y);
            info.AddValue("z", vec3.z);

        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Vector3 vec3 = (Vector3)obj;
            vec3.x = (float)info.GetValue("x", typeof(float));
            vec3.y = (float)info.GetValue("y", typeof(float));
            vec3.z = (float)info.GetValue("z", typeof(float));
            return vec3;
        }
    }

}
