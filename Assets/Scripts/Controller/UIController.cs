using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIController : MonoBehaviour
{
    public TMP_Text textEnd;
    public TMP_Text textAmountBall;
    public Image progressFlag;
    public Transform BaseFlag;
    public Image blackScreen;
    public TMP_Text scorePlusPrefab;
    public Text textCoin;
    public Text textLevel;
    public Text textCoinGW;
    public GameObject GameWinPopup;
    public GameObject GameLosePopup;

    void Start()
    {
        textLevel.text = "Level " + (StaticData.level + 1).ToString();

        UpdateFlagFly(StaticData.level);
    }

    public void UpdateTextCoin(int coin)
    {
        textCoin.text = coin.ToString();
    }

    public void SetAmountBall(int amount)
    {
        textAmountBall.text = amount.ToString() + "/4";
    }

    public void UpdateAmountFlag(int amount)
    {
        for (int i = 0; i < BaseFlag.childCount; i++)
        {
            if (i < amount)
            {
                BaseFlag.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    Tween fillTween;
    public void UpdateFlagFly(int amount)
    {
        UpdateAmountFlag(amount);
        DOTween.Kill(fillTween);
        fillTween = progressFlag.DOFillAmount(amount * 0.2f, 1f);
    }

    public void GameWinEvent()
    {
        AudioController.instance.PlayAudio(AudioType.Victory, 0.3f);
        GameWinPopup.gameObject.SetActive(true);
        GameWinPopup.GetComponent<Image>().DOFade(0.7f, 1);
        GameWinPopup.gameObject.SetActive(true);
        textCoinGW.text = "+" + GameController.Instance.PlankParent.childCount.ToString();
        // GameWinPopup.DOFade(0.7f, 1);
        // GameWinPopup.text = "VICTORY";
    }

    public void GameLoseEvent()
    {
        AudioController.instance.PlayAudio(AudioType.Fail, 0.5f);
        GameLosePopup.gameObject.SetActive(true);
        GameLosePopup.GetComponent<Image>().DOFade(0.7f, 1);
        GameLosePopup.SetActive(true);
        // textEnd.DOFade(0.7f, 1).OnComplete(() =>
        // {
        //     textEnd.gameObject.SetActive(false);
        // });
        // textEnd.text = "DEFEATED";
    }
    public void AddScore()
    {
        StartCoroutine(DelayFunc(() =>
        {
            for (var i = 0; i < GameController.Instance.PlankParent.childCount; i++)
            {
                var rd = UnityEngine.Random.Range(1f, 2f);
                var rd2 = UnityEngine.Random.Range(0.1f, 0.8f);
                if (!GameController.Instance.PlankParent.GetChild(i).GetComponent<Plank>().isRed)
                {
                    var scorePlus = Instantiate(scorePlusPrefab);
                    scorePlus.transform.SetParent(GameController.Instance.ScorePlusParent);
                    scorePlus.transform.position = GameController.Instance.PlankParent.GetChild(i).transform.position;
                    StartCoroutine(DelayFunc(() =>
                    {
                        var seq = DOTween.Sequence()
                        .Append(scorePlus.transform.DOMoveY(scorePlus.transform.position.y + 1.6f, rd))
                        .Play();
                        var seq2 = DOTween.Sequence()
                        .Append(scorePlus.DOFade(1, 1f))
                        .Append(scorePlus.DOFade(0, 1f))
                        .Play();
                        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) + 1);
                        // Debug.Log(PlayerPrefs.GetInt("Coin", 0));
                        UpdateTextCoin(PlayerPrefs.GetInt("Coin", 0));
                    }, rd2));
                }
            }

        }, 0.3f));


    }

    public void ClickRelay()
    {
        SceneManager.LoadScene("GameScene");
        // GameController.Instance.ChangeTime(1f);
    }
    public void ClickNext()
    {
        StaticData.level += 1;
        if (StaticData.level >= 30)
        {
            StaticData.level = 0;
        }
        PlayerPrefs.SetInt("Level", StaticData.level);
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickReward()
    {
        FindObjectOfType<IronSourceAdsController>().ShowVideoAds(() =>
                {
                }, () =>
                {
                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) + GameController.Instance.PlankParent.childCount);
                    UpdateTextCoin(PlayerPrefs.GetInt("Coin", 0));
                    textCoinGW.text = "+" + (GameController.Instance.PlankParent.childCount * 2).ToString();
                });
    }
    IEnumerator DelayFunc(Action call, float time)
    {
        yield return new WaitForSeconds(time);
        call();
    }
}
