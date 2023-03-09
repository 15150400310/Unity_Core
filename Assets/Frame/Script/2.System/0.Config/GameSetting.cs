﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

/// <summary>
/// 框架层面的游戏设置
/// 默认分辨率
/// 对象池缓存设置
/// UI元素的设置
/// </summary>
[CreateAssetMenu(fileName = "GameSetting", menuName = "Frame/Config/GameSetting")]
public class GameSetting : ConfigBase
{
#if UNITY_EDITOR

    [Button(Name = "初始化游戏配置",ButtonHeight = 50)]
    [GUIColor(0,1,0)]
    private void Init()
    {

    }

    /// <summary>
    /// 编译前执行函数
    /// </summary>
    [InitializeOnLoadMethod]
    private static void LoadForEditor()
    {
        GameObject.Find("GameRoot").GetComponent<GameRoot>().GameSetting.Init();
    }
#endif
}
