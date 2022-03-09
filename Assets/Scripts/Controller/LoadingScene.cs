using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public Image progress;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<IronSourceAdsController>().ShowBanner();
        // AdsController.Instance.ShowBanner();
        // IronSource.Agent.init("13c1e9481", IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);
        // Invoke("InitBanner", 3);
        // AppOpenAdManager.Instance.LoadAd();
        StaticData.loadding = true;
        AudioController.instance.PlayAudio(AudioType.BGM, 0.5f);
        progress.DOFillAmount(1f, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }

    void InitBanner()
    {
        // IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
        IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, IronSourceBannerPosition.TOP);
        IronSource.Agent.displayBanner();
    }

    public void OnApplicationPause(bool paused)
    {
        // Display the app open ad when the app is foregrounded
        if (!paused)
        {
            AppOpenAdManager.Instance.ShowAdIfAvailable();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
