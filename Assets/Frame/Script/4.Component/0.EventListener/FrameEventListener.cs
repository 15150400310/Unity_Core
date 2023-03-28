using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Frame
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public enum FrameEventType
    {
        OnMouseEnter,
        OnMouseExit,
        OnClick,
        OnClickDown,
        OnClickUp,
        OnDrag,
        OnBeginDrag,
        OnEndDrag,
        OnCollisionEnter,
        OnCollisionStay,
        OnCollisionExit,
        OnCollisionEnter2D,
        OnCollisionStay2D,
        OnCollisionExit2D,
        OnTriggerEnter,
        OnTriggerStay,
        OnTriggerExit,
        OnTriggerEnter2D,
        OnTriggerStay2D,
        OnTriggerExit2D,
    }

    public interface IMouseEvent : IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {

    }
    /// <summary>
    /// 事件工具
    /// 可以添加 鼠标 碰撞 触发等事件
    /// </summary>
    public class FrameEventListener : MonoBehaviour, IMouseEvent
    {
        #region 内部类 接口等
        /// <summary>
        /// 某个事件中一个事件的数据包装类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class FrameEventListenerInfo<T>
        {
            //T:事件本身的参数(PointerEventData Collision)
            //object[]:事件的参数
            public Action<T, object[]> action;
            public object[] args;
            public void Init(Action<T, object[]> action, object[] args)
            {
                this.action = action;
                this.args = args;
            }

            public void Destroy()
            {
                this.action = null;
                this.args = null;
                this.ObjectPushPool();
            }

            public void TriggerEvent(T eventData)
            {
                action?.Invoke(eventData, args);
            }
        }

        interface IFrameEventListenerInfos
        {
            void RemoveAll();
        }

        /// <summary>
        /// 一类事件的数据包装类型:包含多个FrameEventListenerInfo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class FrameEventListenerInfos<T> : IFrameEventListenerInfos
        {
            //所有的事件
            private List<FrameEventListenerInfo<T>> eventList = new List<FrameEventListenerInfo<T>>();

            /// <summary>
            /// 添加事件
            /// </summary>
            public void AddListener(Action<T, object[]> action, params object[] args)
            {
                FrameEventListenerInfo<T> info = PoolManager.Instance.GetObject<FrameEventListenerInfo<T>>();
                info.Init(action, args);
                eventList.Add(info);
            }

            /// <summary>
            /// 移除事件
            /// </summary>
            public void RemoveListener(Action<T, object[]> action, bool checkArgs = false, params object[] args)
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    // 找到这个事件
                    if (eventList[i].action.Equals(action))
                    {
                        //是否需要检查参数
                        if (checkArgs && args.Length > 0)
                        {
                            //参数如果相等
                            if (args.ArrayEquals(eventList[i].args))
                            {
                                //移除
                                eventList[i].Destroy();
                                eventList.RemoveAt(i);
                                return;
                            }
                        }
                        else
                        {
                            //移除
                            eventList[i].Destroy();
                            eventList.RemoveAt(i);
                            return;
                        }
                    }
                }
            }

            /// <summary>
            /// 移除全部,全部放进对象池
            /// </summary>
            public void RemoveAll()
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    eventList[i].Destroy();
                }
                eventList.Clear();
                this.ObjectPushPool();
            }

            public void TriggerEvent(T eventData)
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    eventList[i].TriggerEvent(eventData);
                }
            }
        }

        /// <summary>
        /// 枚举比较器
        /// </summary>
        private class FrameEventTypeEnumComparer : Singleton<FrameEventTypeEnumComparer>, IEqualityComparer<FrameEventType>
        {
            public bool Equals(FrameEventType x, FrameEventType y)
            {
                return x == y;
            }

            public int GetHashCode(FrameEventType obj)
            {
                return (int)obj;
            }
        }
        #endregion

        private Dictionary<FrameEventType, IFrameEventListenerInfos> eventInfoDic = new Dictionary<FrameEventType, IFrameEventListenerInfos>(FrameEventTypeEnumComparer.Instance);

        #region 外部的访问
        /// <summary>
        /// 添加事件
        /// </summary>
        public void AddListener<T>(FrameEventType eventType, Action<T, object[]> action, params object[] args)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                (eventInfoDic[eventType] as FrameEventListenerInfos<T>).AddListener(action, args);
            }
            else
            {
                FrameEventListenerInfos<T> infos = PoolManager.Instance.GetObject<FrameEventListenerInfos<T>>();
                infos.AddListener(action, args);
                eventInfoDic.Add(eventType, infos);
            }
        }

        /// <summary>
        /// 移出事件
        /// </summary>
        public void RemoveListener<T>(FrameEventType eventType, Action<T, object[]> action, bool checkArgs = false, params object[] args)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                (eventInfoDic[eventType] as FrameEventListenerInfos<T>).RemoveListener(action, checkArgs, args);
            }
        }

        /// <summary>
        /// 移除某一个事件类型下的全部事件
        /// </summary>
        public void RemoveAllListener(FrameEventType eventType)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                eventInfoDic[eventType].RemoveAll();
                eventInfoDic.Remove(eventType);
            }
        }

        /// <summary>
        /// 移除全部事件
        /// </summary>
        public void RemoveAllListener()
        {
            foreach (IFrameEventListenerInfos infos in eventInfoDic.Values)
            {
                infos.RemoveAll();
            }
            eventInfoDic.Clear();
        }
        #endregion

        private void TriggerAction<T>(FrameEventType eventType, T eventData)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                (eventInfoDic[eventType] as FrameEventListenerInfos<T>).TriggerEvent(eventData);
            }
        }

        #region 鼠标事件
        public void OnBeginDrag(PointerEventData eventData)
        {
            TriggerAction(FrameEventType.OnBeginDrag, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            TriggerAction(FrameEventType.OnDrag, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            TriggerAction(FrameEventType.OnEndDrag, eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            TriggerAction(FrameEventType.OnClick, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            TriggerAction(FrameEventType.OnClickDown, eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TriggerAction(FrameEventType.OnMouseEnter, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TriggerAction(FrameEventType.OnMouseExit, eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TriggerAction(FrameEventType.OnClickUp, eventData);
        }
        #endregion

        #region 碰撞事件
        private void OnCollisionEnter(Collision collision)
        {
            TriggerAction(FrameEventType.OnCollisionEnter, collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            TriggerAction(FrameEventType.OnCollisionExit, collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            TriggerAction(FrameEventType.OnCollisionStay, collision);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TriggerAction(FrameEventType.OnCollisionEnter2D, collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            TriggerAction(FrameEventType.OnCollisionExit2D, collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            TriggerAction(FrameEventType.OnCollisionStay2D, collision);
        }
        #endregion

        #region  触发事件
        private void OnTriggerEnter(Collider other)
        {
            TriggerAction(FrameEventType.OnTriggerEnter, other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerAction(FrameEventType.OnTriggerExit, other);
        }

        private void OnTriggerStay(Collider other)
        {
            TriggerAction(FrameEventType.OnTriggerStay, other);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerAction(FrameEventType.OnTriggerEnter2D, other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerAction(FrameEventType.OnTriggerExit2D, other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TriggerAction(FrameEventType.OnTriggerStay2D, other);
        }
        #endregion
    }
}

