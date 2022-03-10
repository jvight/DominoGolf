using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Purchasing;

public class HomeController : MonoBehaviour
{
    public Image blackScreen;
    public GameObject SettingPopup;
    public GameObject UIGame;
    public GameObject ShopPopup;
    public GameObject ButtonNoAds;
    public Transform tutHand;
    void Start()
    {
        CheckNoAds();
        // SkyboxController.instance.ChangeSkybox();
        FindObjectOfType<IronSourceAdsController>().ShowBanner();
        // AdsController.Instance.ShowBanner();
        if (StaticData.loadding)
        {
            StaticData.loadding = false;
            StaticData.game_start = false;
        }
        else
        {
            UIGame.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
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
        AudioController.instance.PlayAudio(AudioType.Click, 0.5f);
        SettingPopup.SetActive(false);
        IARManager.Instance.ShowBox();
    }

    public void OnClickSetting()
    {
        AudioController.instance.PlayAudio(AudioType.Click, 0.5f);
        // blackScreen.gameObject.SetActive(true);
        SettingPopup.SetActive(true);
        // SettingPopup.transform.DOScale(1, 0.5f);
    }

    public void OnClickXSetting()
    {
        AudioController.instance.PlayAudio(AudioType.Click, 0.5f);
        // blackScreen.gameObject.SetActive(false);
        // SettingPopup.transform.DOScale(0, 0.5f).OnComplete(() =>
        // {
        // });
        SettingPopup.SetActive(false);
    }

    public void OnClickShop()
    {
        ShopPopup.SetActive(true);
    }

    public void CheckNoAds() {
        if (PlayerPrefs.GetInt("NoAds", 0) == 1) {
            ButtonNoAds.SetActive(false);
        }
    }

    public void OnPurchaseCompele(Product product)
    {
        PlayerPrefs.SetInt("NoAds", 1);
        Invoke("CheckNoAds", 0.1f);
    }
    
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log(product.definition.id + "failed because" + reason);
    }

    public void ClickStart()
    {
        if(StaticData.level==14){
            tutHand.gameObject.SetActive(true);
        }
        if(StaticData.level==0){
            GameController.Instance.Tutorial.gameObject.SetActive(true);
            GameController.Instance.Tutorial.hand.gameObject.SetActive(true);
            GameController.Instance.Tutorial.tutText.gameObject.SetActive(true);
            GameController.Instance.Tutorial.MoveTut();
        }
        AudioController.instance.PlayAudio(AudioType.Click, 0.5f);
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
