using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsButtonManager : MonoBehaviour
{
#if UNITY_ANDROID
    private string goldID = "ca-app-pub-3690871787222843/2280743785";
    private string diaID = "ca-app-pub-3690871787222843/5118622126";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
  private string _adUnitId = "unused";
#endif

    private RewardedAd goldAd;
    private RewardedAd diaAd;

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus => { });
        LoadGoldRewardedAd();
        LoadDiaRewardedAd();
    }

    public void LoadDiaRewardedAd()
    {        // Clean up the old ad before loading a new one.
        if (diaAd != null)
        {
            diaAd.Destroy();
            diaAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("DangerousOutSideDia");

        // send the request to load the ad.
        RewardedAd.Load(diaID, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());
                // Raised when the ad closed full screen content.
                ad.OnAdFullScreenContentClosed += () =>
                {
                    Debug.Log("Rewarded Ad full screen content closed.");

                    // Reload the ad so that we can show another as soon as possible.
                    LoadDiaRewardedAd();
                };
                // Raised when the ad failed to open full screen content.
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    Debug.LogError("Rewarded ad failed to open full screen content " +
                                   "with error : " + error);

                    // Reload the ad so that we can show another as soon as possible.
                    LoadDiaRewardedAd();
                };


                diaAd = ad;
            });
    }
    public void LoadGoldRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (goldAd != null)
        {
            goldAd.Destroy();
            goldAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("DangerousOutSideGold");

        // send the request to load the ad.
        RewardedAd.Load(goldID, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());
                // Raised when the ad closed full screen content.
                ad.OnAdFullScreenContentClosed += () =>
                {
                    Debug.Log("Rewarded Ad full screen content closed.");

                    // Reload the ad so that we can show another as soon as possible.
                    LoadGoldRewardedAd();
                };
                // Raised when the ad failed to open full screen content.
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    Debug.LogError("Rewarded ad failed to open full screen content " +
                                   "with error : " + error);

                    // Reload the ad so that we can show another as soon as possible.
                    LoadGoldRewardedAd();
                };


                goldAd = ad;
            });
    }
    public void GoldHundredButton()
    {
        ShowRewardedAd(goldAd,() => { GameManager.instance.money += 200 * (GameManager.instance.highstStage+1);
            UIManager.Instance.ShowMoney();
        });
    }
    public void DiaTwentyButton()
    {
        ShowRewardedAd(diaAd,() => { GameManager.instance.dia += 20;
            UIManager.Instance.ShowDiaCount();
        });
    }

    public void ShowRewardedAd(RewardedAd rewardedAd, Action action)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                action();
                PlayCloudDataManager.Instance.SaveCurState();
            });
        }
    }
}

public enum ADSKIND { 
    GOLD,
    DIA
}
