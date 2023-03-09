﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : ManagerBase<ResManager>
{
    //需要缓存的类型
    private Dictionary<Type, bool> wantCacheDic;

    public override void Init()
    {
        base.Init();
        //TODO:替换成真实的配置
        wantCacheDic = new Dictionary<Type, bool>();
        wantCacheDic.Add(typeof(SphereController), true);
    }

    /// <summary>
    /// 检查一个类型是否需要缓存
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private bool CheckCacheDic(Type type)
    {
        return wantCacheDic.ContainsKey(type);
    }

    /// <summary>
    /// 加载Unity资源 如AudioClip Sprite
    /// </summary>
    public T LoadAsset<T>(string path) where T :UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }

    /// <summary>
    /// 获取实例-普通Class
    /// 如果类型需要缓存,会从对象池中获取
    /// </summary>
    public T Load<T>() where T : class, new()
    {
        //需要缓存
        if (CheckCacheDic(typeof(T)))
        {
            return PoolManager.Instance.GetObject<T>();
        }
        else
        {
            return new T();
        }
    }

    /// <summary>
    /// 获取实例-组件
    /// </summary>
    public T Load<T>(string path,Transform parent = null) where T: Component
    {
        //需要缓存
        if (CheckCacheDic(typeof(T)))
        {
            return PoolManager.Instance.GetGameObject<T>(GetPrefab(path),parent);
        }
        else
        {
            return InstantiateForPrefab(path).GetComponent<T>();
        }
    }

    /// <summary>
    /// 异步加载游戏物体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="callBack"></param>
    /// <param name="parent"></param>
    public void LoadGameObjectAsync<T>(string path,Action<T> callBack = null,Transform parent = null) where T : UnityEngine.Object
    {
        //对象池里面有
        if (CheckCacheDic(typeof(T)))
        {
            GameObject go = PoolManager.Instance.CheckCacheAndLoadGameObject(path, parent);
            if(go != null)
            {
                callBack?.Invoke(go.GetComponent<T>());
            }
            //对象池没有
            else
            {
                StartCoroutine(DoLoadGameObjectAsync<T>(path, callBack, parent));
            }
        }
        //对象池没有
        else
        {
            StartCoroutine(DoLoadGameObjectAsync<T>(path, callBack, parent));
        }
    }

    IEnumerator DoLoadGameObjectAsync<T>(string path, Action<T> callBack = null, Transform parent = null) where T : UnityEngine.Object
    {
        ResourceRequest request = Resources.LoadAsync<GameObject>(path);
        yield return request;
        GameObject go = InstantiateForPrefab(request.asset as GameObject,parent);
        callBack?.Invoke(go.GetComponent<T>());
    }
    /// <summary>
    /// 异步加载Unity资源 AudioClip Sprite GameObject(预制体)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="callBack"></param>
    public void LoadAssetAsync<T>(string path,Action<T> callBack) where T : UnityEngine.Object
    {
        StartCoroutine(DoLoadAssetAsync<T>(path, callBack));
    }

    IEnumerator DoLoadAssetAsync<T>(string path,Action<T> callBack) where T : UnityEngine.Object
    {
        ResourceRequest request = Resources.LoadAsync<T>(path);
        yield return request;
        callBack?.Invoke(request.asset as T);
    }

    /// <summary>
    /// 获取预制体
    /// </summary>
    public GameObject GetPrefab(string path)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        if (prefab != null)
        {
            return prefab;
        }
        else
        {
            throw new Exception("Frame:预制体路径有误,没有找到预制体");
        }
    }

    /// <summary>
    /// 基于预制体实例化
    /// </summary>
    public GameObject InstantiateForPrefab(string path, Transform parent = null)
    {
        return InstantiateForPrefab(GetPrefab(path), parent);
    }

    /// <summary>
    /// 基于预制体实例化
    /// </summary>
    public GameObject InstantiateForPrefab(GameObject prefab, Transform parent = null)
    {
        GameObject go = Instantiate<GameObject>(prefab, parent);
        go.name = prefab.name;
        return go;
    }
}