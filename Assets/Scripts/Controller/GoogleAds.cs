using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleAds : MonoBehaviour
{
    bool readyAds = true;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        AppOpenAdManager.Instance.LoadAd();
        AppOpenAdManager.Instance.ShowAdIfAvailable();
        readyAds = false;
        InvokeRepeating("Count", 1, 10);
    }

    public void OnApplicationStart()
    {
        // if (!readyAds) { return; }
        AppOpenAdManager.Instance.LoadAd();
        AppOpenAdManager.Instance.ShowAdIfAvailable();
        readyAds = false;
        // InvokeRepeating("Count", 1, 10);
    }
    public void OnApplicationPause(bool paused)
    {
        // Display the app open ad when the app is foregrounded
        if (!paused)
        {
            if (!readyAds) { return; }
            AppOpenAdManager.Instance.ShowAdIfAvailable();
            readyAds = false;
            InvokeRepeating("Count", 1, 10);
        }
    }

    void Count()
    {
        count++;
        if (count == 10)
        {
            readyAds = true;
        }
    }
}
