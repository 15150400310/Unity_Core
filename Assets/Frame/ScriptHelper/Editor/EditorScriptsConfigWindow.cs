using UnityEditor;
using UnityEngine;

namespace Frame
{
    /// <summary>
    /// 脚本配置窗口
    /// </summary>
    public class EditorScriptsConfigWindow : EditorWindow
    {
        /// <summary>
        /// 脚本配置菜单
        /// </summary>
        [MenuItem("Frame/ScriptsConfig &F")]
        static void ScriptsConfig()
        {
            EditorScriptsConfigWindow configWindow = new EditorScriptsConfigWindow();
            configWindow.minSize = new Vector2(600, 300);
            configWindow.maxSize = new Vector2(600, 300);
            configWindow.Show();
        }
        private void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(30);
            GUI.skin.label.fontSize = 15;

            GUI.skin.label.alignment = TextAnchor.UpperLeft;
            this.titleContent = new GUIContent("脚本生成配置");

            EditorScriptsConfig.Instance.Author = EditorGUILayout.TextField("作者:", EditorScriptsConfig.Instance.Author);
            GUILayout.Space(10);
            EditorScriptsConfig.Instance.NameSpace = EditorGUILayout.TextField("名称空间:", EditorScriptsConfig.Instance.NameSpace);

            GUILayout.Space(10);
            EditorScriptsConfig.Instance.TemplatePath = EditorGUILayout.TextField("模板路径: ", EditorScriptsConfig.Instance.TemplatePath);

            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Space(20);
            GUI.color = Color.green;

            if (GUILayout.Button("完成"))
            {
                if (!EditorScriptsConfig.Instance.TemplatePath.EndsWith("/"))
                    EditorScriptsConfig.Instance.TemplatePath += "/";
                EditorUtility.SetDirty(EditorScriptsConfig.Instance);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                this.Close();
            }
            GUILayout.EndVertical();
        }
    }
}