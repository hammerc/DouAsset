using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BuildPipeline
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class BuildUtil
    {
        public static void CreateSerializeFile(Type type, string filePath)
        {
            var data = ScriptableObject.CreateInstance(type);
            AssetDatabase.CreateAsset(data, filePath);
            AssetDatabase.Refresh();
        }

        public static void CreateSerializeFile<T>(string filePath) where T : ScriptableObject
        {
            var data = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(data, filePath);
            AssetDatabase.Refresh();
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void SetData<T>(T origin, T data)
        {
            var type = typeof(T);
            var fieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var fieldInfo in fieldInfos)
            {
                var value = fieldInfo.GetValue(data);
                fieldInfo.SetValue(origin, value);
            }
        }
    }
}
