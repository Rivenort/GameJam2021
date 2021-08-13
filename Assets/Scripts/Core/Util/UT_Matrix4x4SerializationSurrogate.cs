using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UT_Matrix4x4SerializationSurrogate : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Matrix4x4 matrix = (Matrix4x4)obj;
            info.AddValue("m00", matrix.m00);
            info.AddValue("m01", matrix.m01);
            info.AddValue("m02", matrix.m02);
            info.AddValue("m03", matrix.m03);
            info.AddValue("m10", matrix.m10);
            info.AddValue("m11", matrix.m11);
            info.AddValue("m12", matrix.m12);
            info.AddValue("m13", matrix.m13);
            info.AddValue("m20", matrix.m20);
            info.AddValue("m21", matrix.m21);
            info.AddValue("m22", matrix.m22);
            info.AddValue("m23", matrix.m23);
            info.AddValue("m30", matrix.m30);
            info.AddValue("m31", matrix.m31);
            info.AddValue("m32", matrix.m32);
            info.AddValue("m33", matrix.m33);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Matrix4x4 matrix = (Matrix4x4)obj;
            matrix.m00 = (float)info.GetValue("m00", typeof(float));
            matrix.m01 = (float)info.GetValue("m01", typeof(float));
            matrix.m02 = (float)info.GetValue("m02", typeof(float));
            matrix.m03 = (float)info.GetValue("m03", typeof(float));
            matrix.m10 = (float)info.GetValue("m10", typeof(float));
            matrix.m11 = (float)info.GetValue("m11", typeof(float));
            matrix.m12 = (float)info.GetValue("m12", typeof(float));
            matrix.m13 = (float)info.GetValue("m13", typeof(float));
            matrix.m20 = (float)info.GetValue("m20", typeof(float));
            matrix.m21 = (float)info.GetValue("m21", typeof(float));
            matrix.m22 = (float)info.GetValue("m22", typeof(float));
            matrix.m23 = (float)info.GetValue("m23", typeof(float));
            matrix.m30 = (float)info.GetValue("m30", typeof(float));
            matrix.m31 = (float)info.GetValue("m31", typeof(float));
            matrix.m32 = (float)info.GetValue("m32", typeof(float));
            matrix.m33 = (float)info.GetValue("m33", typeof(float));
            return matrix;
        }
    }

}