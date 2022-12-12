using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HmsPlugin;
using UnityEngine.Events;

public class RewardedVideo : MonoBehaviour
{
    public UnityEvent endRewardAction;
    public bool completeReward;
    public string adUnitID;

    private void Start()
    {
        HMSAdsKitManager.Instance.OnRewardAdCompleted = HandleUserEarnedReward;
    }

    public void UserChoseToWatchAd()
    {
        HMSAdsKitManager.Instance.ShowRewardedAd();


    }
    public void HandleUserEarnedReward()
    {
        completeReward = true;
        endRewardAction?.Invoke();
    }
}