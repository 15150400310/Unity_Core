﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Frame
{
    [UIElement(true, "UI/UI_LoadingWindow", 4)]
    public class UI_LoadingWindow : UIPanelBase/*, ILoadPage*/
    {
        [SerializeField] private Text progress_Text;

        [SerializeField] private Image fill_Image;

        public override void Init()
        {
            progress_Text = GetUI<Text>("Progress/Progress_Text");
            fill_Image = GetUI<Image>("Progress/Fill_Image");
        }

        public override void OnShow()
        {
            base.OnShow();
            UpdateProgress(0);
        }

        protected override void RegisterEventListener()
        {
            base.RegisterEventListener();
            EventManager.AddEventListener<float>("LoadingSceneProgress", UpdateProgress);
            EventManager.AddEventListener("LoadSceneSucceed", OnLoadSceneSucceed);
        }

        protected override void CancelEventListener()
        {
            base.CancelEventListener();
            EventManager.RemoveEventListener<float>("LoadingSceneProgress", UpdateProgress);
            EventManager.RemoveEventListener("LoadSceneSucceed", OnLoadSceneSucceed);
        }

        /// <summary>
        /// 当场景加载成功
        /// </summary>
        private void OnLoadSceneSucceed()
        {
            Close();
        }

        /// <summary>
        /// 更新进度
        /// </summary>
        /// <param name="progressValue"></param>
        private void UpdateProgress(float progressValue)
        {
            Debug.Log((int)(progressValue * 100) + "%");
            progress_Text.text = (int)(progressValue * 100) + "%";
            fill_Image.fillAmount = progressValue;
        }

        //public void OnProgressChange(float _curProgress)
        //{
        //    fill_Image.fillAmount = _curProgress;
        //    progress_Text.text = (int)(_curProgress * 100) + "%";
        //}

        //public void OnLoadingComplete()
        //{
        //    Close();
        //}
    }
}

