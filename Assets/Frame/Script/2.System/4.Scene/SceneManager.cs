using System;
using System.Collections;
using UnityEngine;

namespace Frame
{
    //public interface ILoadPage
    //{
    //    /// <summary>
    //    /// 当进度条变动时调用
    //    /// </summary>
    //    /// <param name="_curProgress">当前加载进度（0=>1）</param>
    //    void OnProgressChange(float _curProgress);
    //    /// <summary>
    //    /// 当加载完时调用
    //    /// </summary>
    //    void OnLoadingComplete();
    //}
    /// <summary>
    /// 场景管理器
    /// </summary>
    public static class SceneManager
    {
        /// <summary>
        /// 同步加载场景
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        /// <param name="callBack">回调函数</param>
        public static void LoadScene(string sceneName, Action callBack = null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            callBack?.Invoke();
        }

        /// <summary>
        /// 异步加载场景
        /// 会自动分发进度到事件中心,事件名称"LoadingSceneProgress"
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        /// <param name="callBack">回调函数</param>
        public static void LoadSceneAsync(string sceneName, Action callBack = null)
        {
            MonoManager.Instance.StartCoroutine(DoLoadSceneAsync(sceneName, callBack));
        }

        private static IEnumerator DoLoadSceneAsync(string sceneName, Action callBack = null)
        {
            AsyncOperation ao = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            while (ao.isDone == false)
            {
                //把加载进度分发到事件中心
                EventManager.EventTrigger("LoadingSceneProgress", ao.progress);
                yield return ao.progress;
            }
            EventManager.EventTrigger<float>("LoadingSceneProgress", 1f);
            EventManager.EventTrigger("LoadSceneSucceed");
            callBack?.Invoke();
        }
        

        //private static float loadingSpeed = 3;
        //private static bool startLoading = false;
        //private static float targetValue;
        //private static AsyncOperation operation;
        //private static ILoadPage loadPage;

        ///// <summary>
        ///// 异步加载场景
        ///// </summary>
        ///// <param name="sceneName">场景名称</param>
        ///// <param name="panelName">加载面板名称</param>
        ///// <param name="onFinish">加载结束后执行的事儿</param>
        //public static void LoadScene(string sceneName, string panelName, Action onFinish = null)
        //{
        //    loadPage = null;
        //    targetValue = 0;
        //    float panelLoadTime = 1;
        //    UIPanelBase uIPanelBase = UI.Show<UI_LoadingWindow>();
        //    loadPage = uIPanelBase.GetComponent<ILoadPage>();
        //    //panelLoadTime = uIPanelBase.uIPanelEffectBase.showDuration;
        //    startLoading = true;
        //    GameRoot.Instance.StartCoroutine(WaitToLoad(panelLoadTime, sceneName, onFinish));
        //}
        //static IEnumerator WaitToLoad(float time, string sceneName, Action onFinish = null)
        //{
        //    yield return new WaitForSeconds(time);
        //    operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        //    GameRoot.Instance.StartCoroutine(LoadIE(onFinish));
        //    GameRoot.Instance.StartCoroutine(AsyncLoading());
        //}
        //static IEnumerator AsyncLoading()
        //{
        //    operation.allowSceneActivation = false;
        //    yield return operation;
        //}
        //static IEnumerator LoadIE(Action onFinsh = null)
        //{
        //    while (startLoading)
        //    {
        //        if (operation.progress < 0.9f)
        //        {
        //            targetValue = Mathf.Lerp(targetValue, operation.progress, Time.deltaTime * loadingSpeed);
        //        }
        //        else
        //        {
        //            targetValue = Mathf.Lerp(targetValue, 1, Time.deltaTime * loadingSpeed);
        //            if (targetValue >= 0.99f)
        //            {
        //                operation.allowSceneActivation = true;
        //                targetValue = 1;
        //                while (!operation.isDone)
        //                {
        //                    yield return new WaitForEndOfFrame();
        //                }
        //                loadPage.OnProgressChange(targetValue);
        //                loadPage.OnLoadingComplete();
        //                onFinsh?.Invoke();
        //                startLoading = false;
        //            }
        //        }
        //        loadPage.OnProgressChange(targetValue);
        //        yield return new WaitForFixedUpdate();
        //    }
        //}
    }
}

