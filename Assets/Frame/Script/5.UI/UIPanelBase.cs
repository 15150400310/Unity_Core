using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Frame
{
    /// <summary>
    /// 窗口基类
    /// </summary>
    public class UIPanelBase : MonoBehaviour
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
        /// 关闭时额外执行的内容
        /// </summary>
        public virtual void OnClose()
        {
            CancelEventListener();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            UIManager.Instance.Close(Type);
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

        public GameObject GetGameObject(string name)
        {
            if (transform.Find(name) == null)
                return null;
            else
                return transform.Find(name).gameObject;
        }

        public Transform GetTransform(string name)
        {
            if (transform.Find(name) == null)
                return null;
            else
                return transform.Find(name);
        }

        public T GetUI<T>(string name) where T : UIBehaviour
        {
            if (transform.Find(name) != null)
                return transform.Find(name).GetComponent<T>();
            else
            {
                return default(T);
            }
        }
        public T GetUIInChildren<T>(string name) where T : UIBehaviour
        {
            T t = transform.Find(name).GetComponentInChildren<T>();
            if (t != null)
                return transform.Find(name).GetComponentInChildren<T>();
            else
                return default(T);
        }

        public T[] GetUIsInChildren<T>(string name) where T : UIBehaviour
        {
            T[] t = transform.Find(name).GetComponentsInChildren<T>();
            if (t != null)
                return transform.Find(name).GetComponentsInChildren<T>();
            else
                return default(T[]);
        }
    }
}

