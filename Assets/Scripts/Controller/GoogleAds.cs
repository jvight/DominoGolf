using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleAds : MonoBehaviour
{
    bool readyAds = true;
    int count = 0;
    // Start is called before the first frame update
    public void Start()
    {
        // Load an app open ad when the scene starts
        AppOpenAdManager.Instance.LoadAd();
    }

    public void OnApplicationPause(bool paused)
    {
        // Display the app open ad when the app is foregrounded
        if (!paused)
        {
            AppOpenAdManager.Instance.ShowAdIfAvailable();
        }
    }
}
