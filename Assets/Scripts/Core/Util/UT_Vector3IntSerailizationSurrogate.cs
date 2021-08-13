using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UT_Vector3IntSerailizationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Vector3Int vec3 = (Vector3Int)obj;
            info.AddValue("x", vec3.x);
            info.AddValue("y", vec3.y);
            info.AddValue("z", vec3.z);

        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Vector3Int vec3 = (Vector3Int)obj;
            vec3.x = (int)info.GetValue("x", typeof(int));
            vec3.y = (int)info.GetValue("y", typeof(int));
            vec3.z = (int)info.GetValue("z", typeof(int));
            return vec3;
        }
    }

}