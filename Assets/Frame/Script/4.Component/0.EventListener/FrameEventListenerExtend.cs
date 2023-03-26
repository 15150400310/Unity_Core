using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class FrameEventListenerExtend
{
    #region 工具函数
    private static FrameEventListener GetOrAddFrameEventListener(Component com)
    {
        FrameEventListener lis = com.GetComponent<FrameEventListener>();
        if (lis == null) return com.gameObject.AddComponent<FrameEventListener>();
        else return lis;
    }
    public static void AddEventListener<T>(this Component com, FrameEventType eventType, Action<T, object[]> action, params object[] args)
    {
        FrameEventListener lis = GetOrAddFrameEventListener(com);
        lis.AddListener(eventType, action, args);
    }

    public static void RemoveEventListener<T>(this Component com, FrameEventType eventType, Action<T, object[]> action, bool checkArgs = false, params object[] args)
    {
        FrameEventListener lis = GetOrAddFrameEventListener(com);
        lis.RemoveListener(eventType, action, checkArgs, args);
    }

    public static void RemoveAllListener(this Component com, FrameEventType eventType)
    {
        FrameEventListener lis = GetOrAddFrameEventListener(com);
        lis.RemoveAllListener(eventType);
    }

    public static void RemoveAllListener(this Component com)
    {
        FrameEventListener lis = GetOrAddFrameEventListener(com);
        lis.RemoveAllListener();
    }
    #endregion

    #region 鼠标相关事件
    public static void OnMouseEnter(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnMouseEnter, action, args);
    }

    public static void OnMouseExit(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnMouseExit, action, args);
    }

    public static void OnClick(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnClick, action, args);
    }

    public static void OnClickDown(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnClickDown, action, args);
    }

    public static void OnClickUp(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnClickUp, action, args);
    }

    public static void OnDrag(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnDrag, action, args);
    }

    public static void OnBeginDrag(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnBeginDrag, action, args);
    }

    public static void OnEndDrag(this Component com, Action<PointerEventData, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnEndDrag, action, args);
    }

    public static void RemoveClick(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnClick, action, checkArgs, args);
    }

    public static void RemoveClickDown(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnClickDown, action, checkArgs, args);
    }

    public static void RemoveClickUp(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnClickUp, action, checkArgs, args);
    }

    public static void RemoveDrag(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnDrag, action, checkArgs, args);
    }

    public static void RemoveBeginDrag(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnBeginDrag, action, checkArgs, args);
    }

    public static void RemoveEndDrag(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnEndDrag, action, checkArgs, args);
    }
    #endregion

    #region 碰撞相关事件
    public static void OnCollisionEnter(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnCollisionEnter, action, args);
    }

    public static void OnCollisionStay(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnCollisionStay, action, args);
    }

    public static void OnCollisionExit(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnCollisionExit, action, args);
    }

    public static void OnCollisionEnter2D(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnCollisionEnter2D, action, args);
    }

    public static void OnCollisionStay2D(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnCollisionStay2D, action, args);
    }

    public static void OnCollisionExit2D(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnCollisionExit2D, action, args);
    }

    public static void RemoveCollisionEnter(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnCollisionEnter, action, checkArgs, args);
    }

    public static void RemoveCollisionStay(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnCollisionStay, action, checkArgs, args);
    }

    public static void RemoveCollisionExit(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnCollisionExit, action, checkArgs, args);
    }

    public static void RemoveCollisionEnter2D(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnCollisionEnter2D, action, checkArgs, args);
    }

    public static void RemoveCollisionStay2D(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnCollisionStay2D, action, checkArgs, args);
    }

    public static void RemoveCollisionExit2D(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnCollisionExit2D, action, checkArgs, args);
    }
    #endregion

    #region 触发相关事件
    public static void OnTriggerEnter(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnTriggerEnter, action, args);
    }

    public static void OnTriggerStay(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnTriggerStay, action, args);
    }

    public static void OnTriggerExit(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnTriggerExit, action, args);
    }

    public static void OnTriggerEnter2D(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnTriggerEnter2D, action, args);
    }

    public static void OnTriggerStay2D(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnTriggerStay2D, action, args);
    }

    public static void OnTriggerExit2D(this Component com, Action<Collision, object[]> action, params object[] args)
    {
        AddEventListener(com, FrameEventType.OnTriggerExit2D, action, args);
    }

    public static void RemoveTriggerEnter(this Component com, Action<Collision, object[]> action,bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnTriggerEnter, action, checkArgs, args);
    }

    public static void RemoveTriggerStay(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnTriggerStay, action, checkArgs, args);
    }

    public static void RemoveTriggerExit(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnTriggerExit, action, checkArgs, args);
    }

    public static void RemoveTriggerEnter2D(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnTriggerEnter2D, action, checkArgs, args);
    }

    public static void RemoveTriggerStay2D(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnTriggerStay2D, action, checkArgs, args);
    }

    public static void RemoveTriggerExit2D(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        RemoveEventListener(com, FrameEventType.OnTriggerExit2D, action, checkArgs, args);
    }
    #endregion
}
