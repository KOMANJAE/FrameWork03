using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ADManager : MonoBehaviour
{
    //public static AdManager instance;
    bool isActive;
    public TMP_Text errorText;
    //  public RewardedAd rewardedAD;
    public Dictionary<string, RewardedAd> rewardedAD = new Dictionary<string, RewardedAd>();
    public RewardedAd firstRewardAd;
    [SerializeField]
    bool isTest = true;
    private readonly string firstRewardADID = "ca-app-pub-5379397473067544/7086267694"; //보상형 광고 ID
    private readonly string rewardTestADID = "ca-app-pub-3940256099942544/5224354917"; //보상형 테스트 광고 ID
    public OnPayed onPayed;
    public delegate void OnPayed(string command);
    public OnAdLoaded onAdLoaded;
    public delegate void OnAdLoaded(string adID);
    public OnAdClosed onAdClosed;
    public delegate void OnAdClosed(string adID);
    string savedReward;
    public GameObject blackScreen;
    public List<string> preLoadList = new List<string>();
    public List<string> testDeviceIds = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        //instance = this;

        MobileAds.Initialize(initStatus => { isActive = true; });

        testDeviceIds.Add(AdRequest.TestDeviceSimulator);
        RequestConfiguration config = new RequestConfiguration.Builder().SetTestDeviceIds(testDeviceIds).build();
        // RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().build();
        MobileAds.SetRequestConfiguration(new RequestConfiguration
        {

        }
            );

        //LoadAds(firstRewardADID);

        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        for (int i = 0; i < preLoadList.Count; i++)
        {
            LoadAds(preLoadList[i], false);
        }
    }

    public bool IsCheckSkipAd()
    {
        return Managers.PF.CheckInventory("buy_remove_ad");
    }

    public void LoadAds(string adId, bool isInvoke = true)
    {

        if (rewardedAD != null && rewardedAD.ContainsKey(adId))
        {
            ShowAds(adId);
            return;
            // rewardedAD[adId].Destroy();
            // rewardedAD.Remove(adId);
            // rewardedAD = null;
        }

        //  blackScreen.SetActive(true);
        RewardedAd.Load(isTest ? rewardTestADID : adId,
            new AdRequest
            {

            },
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.Log(error.GetResponseInfo() + "::" + error.GetMessage());
                    errorText.text = error.GetMessage();
                    //    blackScreen.SetActive(false);
                }
                else
                {
                    // savedReward =  ad.GetRewardItem().Type;
                    RegisterEventHandlers(ad, adId); //이벤트 등록
                    //rewardedAD = ad;
                    rewardedAD.Add(adId, ad);
                    if (adId == firstRewardADID)
                    {

                    }
                    if (isInvoke)
                        onAdLoaded?.Invoke(adId);
                    blackScreen.SetActive(false);
                    // ShowAds();
                }
            }
            );

        //rewardedAD = new RewardedAd(isTest ? rewardTestADID : rewardADID);
    }

    public void ShowAds(string adId)
    {
        /*const string rewardMsg =
          "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
          */
        blackScreen.SetActive(true);
        Debug.Log($"isRewardedAD {rewardedAD} isContains{rewardedAD.ContainsKey(adId)}, IsLoaded{rewardedAD[adId].CanShowAd()}");
        if (rewardedAD != null && rewardedAD.ContainsKey(adId) && rewardedAD[adId].CanShowAd())
        {
            rewardedAD[adId].Show((Reward reward) =>
            {
                ////보상 획득하기
                //customAccount.IncreaseAccount(1);
                //#if UNITY_EDITOR
                //    errorText.text = reward.Type+"::"+reward.Amount;

                onPayed?.Invoke(rewardedAD[adId].GetRewardItem().Type);
                blackScreen.SetActive(false);
                if (rewardedAD.ContainsKey(adId))
                {
                    rewardedAD[adId].Destroy();
                    rewardedAD.Remove(adId);
                }

                LoadAds(adId, false); //닫기 버튼 누를때 광고 재로드
                //#endif        
            });

        }
        else
        {
            blackScreen.SetActive(false);
        }
    }



    private void RegisterEventHandlers(RewardedAd ad, string adId) //광고 이벤트
    {

        ad.OnAdPaid += (AdValue adValue) =>
        {

            //보상 주기

            errorText.text = (string.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
            //  onPayed?.Invoke(ad.GetRewardItem().Type);
            //  errorText.text = ad.GetRewardItem().Type;
            //지금은 광고 보상이 부활 하나밖에 없으니까

            // onPayed?.Invoke("Resurrect");
        };
        ad.OnAdImpressionRecorded += () =>
        {
            errorText.text = ("Interstitial ad recorded an impression.");
        };
        ad.OnAdClicked += () =>
        {
            errorText.text = ("Interstitial ad was clicked.");
        };
        ad.OnAdFullScreenContentOpened += () =>
        {
            errorText.text = ("Interstitial ad full screen content opened.");
        };
        ad.OnAdFullScreenContentClosed += () =>
        {


            errorText.text = ("Interstitial ad full screen content closed.");
            onAdClosed?.Invoke(adId);
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            errorText.text = ("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }
}
