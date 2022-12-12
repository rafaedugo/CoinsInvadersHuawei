using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HmsPlugin;
using System;
public class BannerAds : MonoBehaviour
{
    public GameObject logo;
    public Image[] imgArray;
    public static Action HideCall;
    private void Start()
    {
        FindLogo();
        HMSAdsKitManager.Instance.OnBannerLoadEvent += HideLogo;
        HMSAdsKitManager.Instance.OnBannerFailedToLoadEvent += ActiveLogo;
        HideCall += HideBanner;

        if (SplashScreen.isFirstTime == false)
        {
            HideBanner();
            ShowBanner();
        }
    }
    private void Awake()
    {
        /* HMSAdsKitManager.Instance.OnBannerLoadEvent += HideLogo;
         HMSAdsKitManager.Instance.OnBannerFailedToLoadEvent += ActiveLogo;*/
    }
    private void OnEnable()
    {
        FindLogo();
        

    }

    private void FindLogo()
    {
        if (logo == null || imgArray == null)
        {
            logo = GameObject.Find("BannerLogo");
            if (logo)
            {
                imgArray = logo.GetComponentsInChildren<Image>();
            }
        }
        
    }
    public static void ShowBanner() => HMSAdsKitManager.Instance.ShowBannerAd();

    public void HideBanner() {
        HideLogo();
        HMSAdsKitManager.Instance.HideBannerAd();

    }

    private void OnDisable()
    {

        HideBanner();
        HMSAdsKitManager.Instance.OnBannerLoadEvent -= HideLogo;
        HMSAdsKitManager.Instance.OnBannerFailedToLoadEvent -= ActiveLogo;
    }

    public void ActiveLogo()
    {
        for (int i = 0; i < imgArray.Length; i++)
        {
            imgArray[i].enabled = true;
        }
    }
    public void HideLogo()
    {
        for(int i = 0; i < imgArray.Length;i++)
        {
            imgArray[i].enabled = false;
        }
    }
}