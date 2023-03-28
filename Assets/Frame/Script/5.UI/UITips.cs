using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Frame
{
    /// <summary>
    /// UI提示窗
    /// </summary>
    public class UITips : MonoBehaviour
    {
        [SerializeField]
        private Text infoText;
        [SerializeField]
        private Animator animator;
        private Queue<string> tipsQue = new Queue<string>();
        private bool isShow = false;

        /// <summary>
        /// 添加提示
        /// </summary>
        /// <param name="info"></param>
        public void AddTips(string info)
        {
            tipsQue.Enqueue(info);
            ShowTips();
        }

        public void ShowTips()
        {
            if (tipsQue.Count > 0 && !isShow)
            {
                infoText.text = tipsQue.Dequeue();
                animator.Play("Show", 0, 0);
            }
        }
        #region 动画事件
        private void StartTips()
        {
            isShow = true;
        }

        private void EndTips()
        {
            isShow = false;
            ShowTips();
        }
        #endregion
    }
}


