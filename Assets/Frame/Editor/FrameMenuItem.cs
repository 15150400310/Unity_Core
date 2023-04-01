using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FrameMenuItem
{
    [MenuItem("Frame/打开存档路径")]
    public static void OpenArchiveDirPath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
}
