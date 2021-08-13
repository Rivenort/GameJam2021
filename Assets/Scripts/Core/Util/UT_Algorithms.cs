using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// Utils
    /// @author Rivenort
    /// </summary>
    public static class UT_Algorithms
    {

        public static List<Type> GetSingletons(string assemblyName)
        {
            List<Type> res = new List<Type>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(asm => asm.GetName().Name.Equals(assemblyName)).ToArray();




            List<Type> types = assemblies.SelectMany(asm => asm.GetTypes())
                .Where(type => !type.IsAbstract && !type.IsInterface).ToList();

            for (int i = 0; i < types.Count; i++)
            {
                bool isPrivate = true;
                MethodInfo getInstanceMethod = null;
                foreach (ConstructorInfo info in types[i].GetConstructors())
                {
                    if (info.IsPublic)
                    {
                        isPrivate = false;
                        break;
                    }
                }
                if (isPrivate)
                {
                    foreach (MethodInfo info in types[i].GetMethods())
                    {
                        if (info.ReturnType == types[i] && info.IsStatic && info.GetParameters().Length == 0)
                        {
                            getInstanceMethod = info;
                            break;
                        }
                    }
                }

                if (isPrivate && getInstanceMethod != null) // is Singleton
                {
                    res.Add(types[i]);
                }
            }

            return res;
        }

        public static List<Type> GetSingletonsOf(string assemblyName, Type type)
        {
            List<Type> singletons = GetSingletons(assemblyName);
            List<Type> res = new List<Type>();
            foreach (Type singleton in singletons)
            {
                if (type.IsAssignableFrom(singleton))
                {
                    res.Add(singleton);
                }
            }
            return res;
        }


        public static List<T> GetSingletonsOf<T>(string assemblyName)
        {
            List<Type> singletons = GetSingletonsOf(assemblyName, typeof(T));
            List<T> ret = new List<T>();
            foreach (Type type in singletons)
            {
                MethodInfo[] methods = type.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    if (method.IsStatic && method.ReturnType == type)
                    {
                        ret.Add((T)method.Invoke(null, null));
                        break;
                    }
                }
            }
            return ret;
        }

        public static Type[] GetAllClassesOf(string assemblyName, Type type)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies().
                Where(asm => asm.GetName().Name.Equals(assemblyName)).ToArray(); ;
            Type[] res = assemblies.SelectMany(a => a.GetTypes())
                                             .Where(x => type.IsAssignableFrom(x)
                                             && !x.IsAbstract && !x.IsInterface).ToArray();
            return res;
        }

        /// <summary>
        /// Extension: destroys and detaches all the children.
        /// </summary>
        public static void Clear(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            transform.DetachChildren();
        }

        public static string ToStringElements(this IEnumerable target)
        {
            StringBuilder builder = new StringBuilder();
            IEnumerator enumerator = target.GetEnumerator();
            builder.Append(target.GetType().Name);
            builder.AppendLine("{");
            while (enumerator.MoveNext())
            {
                builder.AppendLine(enumerator.Current.ToString());
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
