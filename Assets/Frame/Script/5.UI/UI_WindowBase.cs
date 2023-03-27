﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窗口结果
/// </summary>
public enum WindowResult
{
    None,
    Yes,
    No,
}

/// <summary>
/// 窗口基类
/// </summary>
public class UI_WindowBase : MonoBehaviour
{
    //窗口类型
    public Type Type { get { return this.GetType(); } }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {

    }

    /// <summary>
    /// 显示
    /// </summary>
    public virtual void OnShow()
    {
        OnUpdateLanguage();
        RegisterEventListener();
    }

    /// <summary>
    /// 关闭
    /// </summary>
    public virtual void Close()
    {
        UIManager.Instance.Close(Type);
        CancelEventListener();
    }

    /// <summary>
    /// 点击 否/取消
    /// </summary>
    public virtual void OnCloseClick()
    {
        Close();
    }

    /// <summary>
    /// 点击 是/确认
    /// </summary>
    public virtual void OnYesClick()
    {
        Close();
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    protected virtual void RegisterEventListener()
    {
        EventManager.AddEventListener("UpdateLanguage", OnUpdateLanguage);
    }

    /// <summary>
    /// 取消事件
    /// </summary>
    protected virtual void CancelEventListener()
    {
        EventManager.RemoveEventListener("UpdateLanguage", OnUpdateLanguage);
    }

    protected virtual void OnUpdateLanguage()
    {

    }
}
