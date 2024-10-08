using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace Services
{
    public static class JsonTool
    {
        public readonly static JsonSerializerSettings DefaultSettings;
        /// <summary>
        /// �漰��̬�����л�ʱ,ʹ�ô�����
        /// </summary>
        public readonly static JsonSerializerSettings PolyMorphicSettings;

        static JsonTool()
        {
            DefaultSettings = new JsonSerializerSettings();
            PolyMorphicSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            };
        }

        /// <summary>
        /// ����Ϊjson
        /// </summary>
        /// <param name="path">·����Ҫ����չ��</param>
        public static void SaveAsJson<T>(T t, string path)
            => SaveAsJson(t, path, DefaultSettings);

        public static void SaveAsJson<T>(T t, string path, JsonSerializerSettings settings)
        {
            try
            {
                FileInfo info = FileTool.GetFileInfo(path, true);
                using StreamWriter writer = info.CreateText();
                string str = JsonConvert.SerializeObject(t, Newtonsoft.Json.Formatting.Indented, settings);
                writer.WriteLine(str);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e.ToString());
            }
        }

        /// <summary>
        /// ��ȡjson
        /// </summary>
        /// <param name="path">·����Ҫ����չ��</param>
        public static T LoadFromJson<T>(string path) where T : class
            => LoadFromJson<T>(path, DefaultSettings);

        public static T LoadFromJson<T>(string path, JsonSerializerSettings settings) where T : class
        {
            FileTool.GetFileInfo(path);
            try
            {
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(path), settings);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogWarning(e.ToString());
                //Debugger.LogWarning(e.ToString(), EMessageType.System);
                return null;
            }
        }
    }
}