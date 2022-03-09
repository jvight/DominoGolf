using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{
    bool readyAds = true;
    int count = 0;
    // Start is called before the first frame update
    public void Start()
    {
        List<string> deviceIds = new List<string>();
        deviceIds.Add("4f0b4883-08ef-4acf-b0c1-ec01e7e318b9");
        RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(deviceIds)
            .build();
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
