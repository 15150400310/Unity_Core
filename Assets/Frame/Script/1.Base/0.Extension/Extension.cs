using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 框架主要的拓展方法
/// </summary>
public static class Extension
{
    public static void Log(this object obj)
    {
        Debug.Log(obj.ToString());
    }
    #region 通用
    /// <summary>
    /// 获取特性
    /// </summary>
    public static T GetAttribute<T>(this object obj) where T : Attribute
    {
        return obj.GetType().GetCustomAttribute<T>();
    }
    /// <summary>
    /// 获取特性
    /// </summary>
    /// <param name="type">特性所在的类型</param>
    public static T GetAttribute<T>(this object obj, Type type) where T : Attribute
    {
        return type.GetCustomAttribute<T>();
    }
    #endregion

}
