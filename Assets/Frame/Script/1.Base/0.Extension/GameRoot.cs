﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : SingletonMono<GameRoot>
{
    /// <summary>
    /// 框架设置
    /// </summary>
    [SerializeField]
    private GameSetting gameSetting;
    public GameSetting GameSetting { get { return gameSetting; } }
    protected override void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        base.Awake();
        DontDestroyOnLoad(gameObject);
        InitManager();
    }
    /// <summary>
    /// 初始化所有管理器
    /// </summary>
    private void InitManager()
    {
        ManagerBase[] managers = GetComponents<ManagerBase>();
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Init();
        }
    }
}
