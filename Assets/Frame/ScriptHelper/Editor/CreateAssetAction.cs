using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
namespace Frame
{
    public class CreateAssetAction
    {
        /// <summary>
        /// 创建UIPanel脚本
        /// </summary>
        [MenuItem("Assets/Create/创建 UI 脚本（C#）", false, 60)]
        public static void CreateUIPanelCS()
        {
            //参数为传递给CreateEventCSScriptAsset类action方法的参数  
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,ScriptableObject.CreateInstance<CreateEventCSScriptAsset>(),GetSelectPathOrFallback() + "/UI_.cs", EditorGUIUtility.FindTexture("cs Script Icon"),EditorScriptsConfig.Instance.TemplatePath+ "UI_.cs.txt");
            
        }
        /// <summary>
        /// 创建实验流程逻辑脚本
        /// </summary>
        [MenuItem("Assets/Create/创建逻辑控制脚本（C#）", false, 60)]
        public static void CreateProcessLogicCS()
        {
            //参数为传递给CreateEventCSScriptAsset类action方法的参数  
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance<CreateEventCSScriptAsset>(), GetSelectPathOrFallback() + "/LogicManager.cs", EditorGUIUtility.FindTexture("cs Script Icon"), EditorScriptsConfig.Instance.TemplatePath + "LogicManager.cs.txt");
        }
        /// <summary>
        /// 创建UIPanel脚本
        /// </summary>
        [MenuItem("Assets/Create/创建普通脚本（C#）", false, 60)]
        public static void CreateNormalCS()
        {
            //参数为传递给CreateEventCSScriptAsset类action方法的参数  
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance<CreateEventCSScriptAsset>(), GetSelectPathOrFallback() + "/NewScript.cs", EditorGUIUtility.FindTexture("cs Script Icon"), EditorScriptsConfig.Instance.TemplatePath + "NewScript.cs.txt");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetSelectPathOrFallback()
        {
            string path = "Assets";
            //遍历选中的资源以获得路径  
            //Selection.GetFiltered是过滤选择文件或文件夹下的物体，assets表示只返回选择对象本身  
            foreach (
                UnityEngine.Object obj in 
                Selection.GetFiltered(typeof(UnityEngine.Object), 
                SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }
            return path;
        }
    }
    class CreateEventCSScriptAsset : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            //创建资源  
            UnityEngine.Object obj = CreateScriptAssetFromTemplate(pathName, resourceFile);
            ProjectWindowUtil.ShowCreatedAsset(obj);//高亮显示资源  
        }

        internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile)
        {
            //获取要创建资源的绝对路径  
            string fullPath = Path.GetFullPath(pathName);

            //读取本地的模板文件  
            StreamReader streamReader = new StreamReader(resourceFile);
            string text = streamReader.ReadToEnd();
            streamReader.Close();

            //获取文件名，不含扩展名  
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);

            //将模板类中的类名替换成你创建的文件名  
            text = Regex.Replace(text, "#ScriptName#", fileNameWithoutExtension);
            text = Regex.Replace(text, "#Author#", EditorScriptsConfig.Instance.Author);
            text = Regex.Replace(text, "#NameSpace#", EditorScriptsConfig.Instance.NameSpace);
            text = Regex.Replace(text, "#NowTime#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            text = Regex.Replace(text, "#ReleasePathName#", EditorScriptsConfig.Instance.NameSpace);
            //写入配置文件  
            bool encoderShouldEmitUTF8Identifier = true; //参数指定是否提供 Unicode 字节顺序标记  
            bool throwOnInvalidBytes = false;//是否在检测到无效的编码时引发异常  
            bool append = false;
            UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
            StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
            streamWriter.Write(text);
            streamWriter.Close();
            
            //刷新资源管理器  
            AssetDatabase.ImportAsset(pathName);
            AssetDatabase.Refresh();
            return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
        }
    }

}