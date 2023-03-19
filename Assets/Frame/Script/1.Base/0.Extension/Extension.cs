using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// 框架主要的拓展方法
/// </summary>
public static class Extension
{

    #region 通用
    public static void Log(this object obj)
    {
        Debug.Log(obj.ToString());
    }

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

    /// <summary>
    /// 数组相等对比
    /// </summary>
    public static bool ArrayEquals(this object[] objs, object[] other)
    {
        if(other == null||objs.GetType()!=other.GetType())
        {
            return false;
        }
        if (objs.Length == other.Length)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                if (!objs[i].Equals(other[i]))
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
        return true;
    }
    #endregion

    #region 资源管理
    /// <summary>
    /// GameObject放入对象池
    /// </summary>
    /// <param name="go"></param>
    public static void GameObjectPushPool(this GameObject go)
    {
        PoolManager.Instance.PushGameObject(go);
    }

    public static void GameObjectPushPool(this Component com)
    {
        GameObjectPushPool(com.gameObject);
    }

    /// <summary>
    /// 普通类放入对象池
    /// </summary>
    /// <param name="go"></param>
    public static void ObjectPushPool(this object obj)
    {
        PoolManager.Instance.PushObject(obj);
    }
    #endregion

    #region 本地化
    /// <summary>
    /// 从本地化系统中修改内容
    /// </summary>
    /// <param name="packName"></param>
    /// <param name="contentKey"></param>
    public static void FrameLocalSet(this Text text,string packName,string contentKey)
    {
        text.text = LocalizationManager.Instance.GetContent<L_Text>(packName, contentKey).content;
    }

    /// <summary>
    /// 从本地化系统中修改内容
    /// </summary>
    /// <param name="packName"></param>
    /// <param name="contentKey"></param>
    public static void FrameLocalSet(this Image image, string packName, string contentKey)
    {
        image.sprite = LocalizationManager.Instance.GetContent<L_Image>(packName, contentKey).content;
    }

    /// <summary>
    /// 从本地化系统中修改内容
    /// </summary>
    /// <param name="packName"></param>
    /// <param name="contentKey"></param>
    public static void FrameLocalSet(this AudioSource audioSource, string packName, string contentKey)
    {
        audioSource.clip = LocalizationManager.Instance.GetContent<L_Audio>(packName, contentKey).content;
    }

    /// <summary>
    /// 从本地化系统中修改内容
    /// </summary>
    /// <param name="packName"></param>
    /// <param name="contentKey"></param>
    public static void FrameLocalSet(this VideoPlayer videoPlayer, string packName, string contentKey)
    {
        videoPlayer.clip = LocalizationManager.Instance.GetContent<L_Video>(packName, contentKey).content;
    }
    #endregion

    #region Mono

    /// <summary>
    /// 添加Update监听
    /// </summary>
    public static void AddUpdateListener(this object obj,Action action)
    {
        MonoManager.Instance.AddUpdateListener(action);
    }

    /// <summary>
    /// 移除Update监听
    /// </summary>
    public static void RemoveUpdateListener(this object obj, Action action)
    {
        MonoManager.Instance.RemoveUpdateListener(action);
    }

    public static Coroutine StartCoroutine(this object obj,IEnumerator routine)
    {
        return MonoManager.Instance.StartCoroutine(routine);
    }

    public static void StopCoroutine(this object obj, Coroutine routine)
    {
        MonoManager.Instance.StopCoroutine(routine);
    }

    public static void StopAllCoroutines(this object obj)
    {
        MonoManager.Instance.StopAllCoroutines();
    }
    #endregion
}
