﻿using System;
using System.Collections.Generic;

/// <summary>
/// 事件系统管理器
/// </summary>
public static class EventManager
{
    #region 内部接口 内部类

    /// <summary>
    /// 事件信息接口
    /// </summary>
    private interface IEventInfo 
    {
        void Destroy();
    }
    /// <summary>
    /// 无参-事件信息
    /// </summary>
    private class EventInfo : IEventInfo
    {
        public Action action;
        public void Init(Action action)
        {
            this.action = action;
        }
        public void Destroy()
        {
            action = null;
            this.ObjectPushPool();
        }
    }

    /// <summary>
    /// 一个参数-事件信息
    /// </summary>
    private class EventInfo<T> : IEventInfo
    {
        public Action<T> action;
        public void Init(Action<T> action)
        {
            this.action = action;
        }
        public void Destroy()
        {
            action = null;
            this.ObjectPushPool();
        }
    }

    /// <summary>
    /// 两个参数-事件信息
    /// </summary>
    private class EventInfo<T, K> : IEventInfo
    {
        public Action<T, K> action;
        public void Init(Action<T, K> action)
        {
            this.action = action;
        }
        public void Destroy()
        {
            action = null;
            this.ObjectPushPool();
        }
    }

    /// <summary>
    /// 三个参数-事件信息
    /// </summary>
    private class EventInfo<T, K, L> : IEventInfo
    {
        public Action<T, K, L> action;
        public void Init(Action<T, K, L> action)
        {
            this.action = action;
        }
        public void Destroy()
        {
            action = null;
            this.ObjectPushPool();
        }
    }
    #endregion

    private static Dictionary<string, IEventInfo> eventInfoDic = new Dictionary<string, IEventInfo>();

    #region 添加事件的监听

    /// <summary>
    /// 添加无参事件
    /// </summary>
    public static void AddEventListener(string eventName, Action action)
    {
        //有没有对应的事件可以监听
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo).action += action;
        }
        //没有的话,需要新增到字典中,并添加对应的Action
        else
        {
            EventInfo eventInfo = PoolManager.Instance.GetObject<EventInfo>();
            eventInfo.Init(action);
            eventInfoDic.Add(eventName, eventInfo);
        }
    }

    /// <summary>
    /// 添加一个参数事件
    /// </summary>
    public static void AddEventListener<T>(string eventName, Action<T> action)
    {
        //有没有对应的事件可以监听
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T>).action += action;
        }
        //没有的话,需要新增到字典中,并添加对应的Action
        else
        {
            EventInfo<T> eventInfo = PoolManager.Instance.GetObject<EventInfo<T>>();
            eventInfo.Init(action);
            eventInfoDic.Add(eventName, eventInfo);
        }
    }

    /// <summary>
    /// 添加两个参数事件
    /// </summary>
    public static void AddEventListener<T, K>(string eventName, Action<T, K> action)
    {
        //有没有对应的事件可以监听
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T, K>).action += action;
        }
        //没有的话,需要新增到字典中,并添加对应的Action
        else
        {
            EventInfo<T, K> eventInfo = PoolManager.Instance.GetObject<EventInfo<T, K>>();
            eventInfo.Init(action);
            eventInfoDic.Add(eventName, eventInfo);
        }
    }

    /// <summary>
    /// 添加三个参数事件
    /// </summary>
    public static void AddEventListener<T, K, L>(string eventName, Action<T, K, L> action)
    {
        //有没有对应的事件可以监听
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T, K, L>).action += action;
        }
        //没有的话,需要新增到字典中,并添加对应的Action
        else
        {
            EventInfo<T, K, L> eventInfo = PoolManager.Instance.GetObject<EventInfo<T, K, L>>();
            eventInfo.Init(action);
            eventInfoDic.Add(eventName, eventInfo);
        }
    }
    #endregion

    #region 触发事件

    /// <summary>
    /// 触发无参事件
    /// </summary>
    public static void EventTrigger(string eventName)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo).action?.Invoke();
        }
    }

    /// <summary>
    /// 触发一个参数事件
    /// </summary>
    public static void EventTrigger<T>(string eventName, T args)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T>).action?.Invoke(args);
        }
    }

    /// <summary>
    /// 触发两个参数事件
    /// </summary>
    public static void EventTrigger<T, K>(string eventName, T arg1, K arg2)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T, K>).action?.Invoke(arg1, arg2);
        }
    }

    /// <summary>
    /// 触发三个参数事件
    /// </summary>
    public static void EventTrigger<T, K, L>(string eventName, T arg1, K arg2, L arg3)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T, K, L>).action?.Invoke(arg1, arg2, arg3);
        }
    }
    #endregion

    #region 取消事件的监听
    /// <summary>
    /// 移除无参的事件监听
    /// </summary>
    /// <param name="eventName"></param>
    public static void RemoveEventListener(string eventName, Action action)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo).action -= action;
        }
    }

    /// <summary>
    /// 移除一个参数的事件监听
    /// </summary>
    /// <param name="eventName"></param>
    public static void RemoveEventListener<T>(string eventName, Action<T> action)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T>).action -= action;
        }
    }

    /// <summary>
    /// 移除两个参数的事件监听
    /// </summary>
    /// <param name="eventName"></param>
    public static void RemoveEventListener<T, K>(string eventName, Action<T, K> action)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T, K>).action -= action;
        }
    }

    /// <summary>
    /// 移除三个参数的事件监听
    /// </summary>
    /// <param name="eventName"></param>
    public static void RemoveEventListener<T, K, L>(string eventName, Action<T, K, L> action)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T, K, L>).action -= action;
        }
    }
    #endregion

    #region 移除事件

    /// <summary>
    /// 移除/删除一个事件
    /// </summary>
    public static void RemoveEventListener(string eventName)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            eventInfoDic[eventName].Destroy();
            eventInfoDic.Remove(eventName);
        }
    }

    /// <summary>
    /// 清空事件中心
    /// </summary>
    public static void Clear()
    {
        foreach (string eventName in eventInfoDic.Keys)
        {
            eventInfoDic[eventName].Destroy ();
        }
        eventInfoDic.Clear();
    }
    #endregion
}
