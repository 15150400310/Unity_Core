using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Frame
{
    public static class UI
    {
        public static T GetUI<T>(this Component trans, string name) where T : UIBehaviour
        {
            if (trans.transform.Find(name) != null)
                return trans.transform.Find(name).GetComponent<T>();
            else
            {
                return default(T);
            }
        }
        public static void Hide<T>(this T obj) where T : UIBehaviour
        {
            obj.gameObject.SetActive(false);
        }
        public static void Show<T>(this T obj) where T : UIBehaviour
        {
            obj.gameObject.SetActive(true);
        }

        public static void AddTips(string info)
        {
            UIManager.Instance.AddTips(info);
        }

        /// <summary>
        /// 打开UI面板
        /// </summary>
        public static T Show<T>() where T : UIPanelBase
        {
            return UIManager.Instance.Show<T>();
        }

        /// <summary>
        /// 打开UI面板
        /// </summary>
        public static void Hide<T>() where T : UIPanelBase
        {
            UIManager.Instance.Close<T>();
        }
    }
}



