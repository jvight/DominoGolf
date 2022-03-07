using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HomeController : MonoBehaviour
{
    public Image blackScreen;
    public GameObject SettingPopup;
    public GameObject UIGame;
    void Start()
    {
        FindObjectOfType<IronSourceAdsController>().ShowBanner();
        if (StaticData.loadding)
        {
            StaticData.loadding = false;
            StaticData.game_start = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
        // progress.DOFillAmount(1f, 1f).OnComplete(() =>
        // {
        //     SceneManager.LoadScene("GameScene");
        // });
    }

    void GameStart()
    {
        StaticData.game_start = true;
    }

    public void OnClickRating()
    {
        AudioController.instance.PlayAudio(AudioType.Click, true, 0, 0.5f);
        SettingPopup.SetActive(false);
        IARManager.Instance.ShowBox();
    }

    public void OnClickSetting()
    {
        AudioController.instance.PlayAudio(AudioType.Click, true, 0, 0.5f);
        blackScreen.gameObject.SetActive(true);
        SettingPopup.SetActive(true);
        SettingPopup.transform.DOScale(1, 0.5f);
    }

    public void OnClickXSetting()
    {
        AudioController.instance.PlayAudio(AudioType.Click, true, 0, 0.5f);
        blackScreen.gameObject.SetActive(false);
        SettingPopup.transform.DOScale(0, 0.5f).OnComplete(() =>
        {
            SettingPopup.SetActive(false);
        });
    }

    public void ClickStart()
    {
        AudioController.instance.PlayAudio(AudioType.Click, true, 0, 0.5f);
        Invoke("GameStart", 0.1f);
        // SceneManager.LoadScene("LoadScene");
        transform.GetChild(0).gameObject.SetActive(false);
        UIGame.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
