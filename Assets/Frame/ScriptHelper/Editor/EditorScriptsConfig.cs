using System.IO;
using UnityEditor;
using UnityEngine;
namespace Frame
{
    public class EditorScriptsConfig  : ScriptableObject
	{
        /// <summary>
        /// 作者
        /// </summary>
        [Header("作者")]
        public  string Author = "Author";

        /// <summary>
        /// 命名空间
        /// </summary>
        [Header("名称空间")]
        public string NameSpace= "NameSpace.Author";

        /// <summary>
        /// 模板路径
        /// </summary>
        [Header("模板路径")]
        public  string TemplatePath = "Assets/Frame/ScriptHelper/Template/";
  

        private const string EditorScriptsConfigFile = "EditorScriptsConfigFile.asset";

        private static string editorScriptsConfigCsPath;
        public static string EditorScriptsConfigCsPath
        {
            get
            {
                if (!string.IsNullOrEmpty(editorScriptsConfigCsPath))
                {
                    return editorScriptsConfigCsPath;
                }

                var result = Directory.GetFiles(Application.dataPath+ "/Frame/ScriptHelper", "EditorScriptsConfig.cs", SearchOption.AllDirectories);
                if (result.Length >= 1)
                {
                    editorScriptsConfigCsPath = Path.GetDirectoryName(result[0]);
                    editorScriptsConfigCsPath = editorScriptsConfigCsPath.Replace('\\', '/');
                    editorScriptsConfigCsPath = editorScriptsConfigCsPath.Replace(Application.dataPath, "Assets");

                    editorScriptsConfigCsPath = editorScriptsConfigCsPath + "/" + EditorScriptsConfigFile;
                }

                return editorScriptsConfigCsPath;
            }
        }

        private static EditorScriptsConfig instanceField;
        public static EditorScriptsConfig Instance
        {
            get
            {
                if (instanceField != null)
                {
                    return instanceField;
                }

                instanceField = (EditorScriptsConfig)AssetDatabase.LoadAssetAtPath(EditorScriptsConfigCsPath, typeof(EditorScriptsConfig));
                if (instanceField == null)
                {
                    instanceField = ScriptableObject.CreateInstance<EditorScriptsConfig>();
                    AssetDatabase.CreateAsset(instanceField, EditorScriptsConfigCsPath);
                }

                return instanceField;
            }
        }

    }
}

