using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HomeController : MonoBehaviour
{
    public static HomeController Instance;
    public Image blackScreen;
    public GameObject SettingPopup;
    public GameObject UIGame;
    void Start()
    {
        FindObjectOfType<IronSourceAdsController>().ShowBanner();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // progress.DOFillAmount(1f, 1f).OnComplete(() =>
        // {
        //     SceneManager.LoadScene("GameScene");
        // });
    }

    public void OnClickRating()
    {
        SettingPopup.SetActive(false);
        IARManager.Instance.ShowBox();
    }

    public void OnClickSetting()
    {
        blackScreen.gameObject.SetActive(true);
        SettingPopup.SetActive(true);
        SettingPopup.transform.DOScale(1, 0.5f);
    }

    public void OnClickXSetting()
    {
        blackScreen.gameObject.SetActive(false);
        SettingPopup.transform.DOScale(0, 0.5f).OnComplete(() =>
        {
            SettingPopup.SetActive(false);
        });
    }

    public void ClickStart()
    {
        // SceneManager.LoadScene("LoadScene");
        transform.GetChild(0).gameObject.SetActive(false);
        UIGame.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
