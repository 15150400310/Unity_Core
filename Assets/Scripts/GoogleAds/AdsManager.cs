/*******************************************************************************
* 类 名 称：AdsInit
* 创建日期：2023-04-11 17:26:38
* 作者名称：
* 功能描述：
* 备注：
******************************************************************************/
using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Frame;

/// <summary>
/// 
/// </summary>
public class AdsManager : SingletonMono<AdsManager>
{
    private RewardedAd rewardedAd;
    private string adUnitId = "ca-app-pub-3940256099942544/5224354917";
    private void Start()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus => { });
        //LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        // Load a rewarded ad
        RewardedAd.Load(adUnitId,new AdRequest.Builder().Build(),
            (RewardedAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    Debug.Log("Rewarded ad failed to load with error: " +
                               loadError.GetMessage());
                    return;
                }
                else if (ad == null)
                {
                    Debug.Log("Rewarded ad failed to load.");
                    return;
                }

                Debug.Log("Rewarded ad loaded.");
                rewardedAd = ad;
            });
    }

    public void ShowRewardedAd(Action action)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                action.Invoke();
                Debug.Log("Rewarded ad granted a reward: " +
                        reward.Amount);
            });
        }
        else
        {
            Debug.Log("Rewarded ad cannot be shown.");
        }
    }
}