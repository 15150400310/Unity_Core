/*******************************************************************************
* 类 名 称：AdsInitializer
* 创建日期：2023-04-24 19:44:59
* 作者名称：
* 功能描述：
* 备注：
******************************************************************************/
using Frame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        //if (!Advertisement.isInitialized && Advertisement.isSupported)
        //{
        //    Advertisement.Initialize(_gameId, _testMode, this);
        //}
        if (Advertisement.isSupported)
        {
            Debug.Log(Application.platform + " supported by Advertisement");
        }
        Advertisement.Initialize(_gameId, _testMode, this);
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        RewardedAds.Instance.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}