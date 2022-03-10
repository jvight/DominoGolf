using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public UIController uiController;
    public Character character;
    public Transform PlankParent;
    public Transform FlagParent;
    public Transform ObjParent;
    public Transform ScorePlusParent;
    public GameObject Tutorial;

    List<Plank> listPlank = new List<Plank>();
    List<Flag> listFlag = new List<Flag>();
    public Golf golf;
    public int AmountBall = 4;
    public bool GameDone = false;
    public JSONReader jsonReader;
    public Texture2D[] TextureBall;
    int coin = 0;
    void Awake()
    {
        Instance = this;
        // GameAnalytics.Initialize();
    }
    void Start()
    {
        // AdsController.Instance.LoadInterstitial();
        SkyboxController.instance.ChangeSkybox();
        coin = PlayerPrefs.GetInt("Coin", 0);
        uiController.UpdateTextCoin(coin);
        uiController.SetAmountBall(AmountBall);
        Time.timeScale = 1;
    }

    public void CreateDone()
    {
        SetBallTexture();
        for (int i = 0; i < PlankParent.childCount; i++)
        {
            listPlank.Add(PlankParent.GetChild(i).GetComponent<Plank>());
        }
        for (int i = 0; i < FlagParent.childCount; i++)
        {
            listFlag.Add(FlagParent.GetChild(i).GetComponent<Flag>());
        }
        // uiController.UpdateAmountFlag(listFlag.Count);
    }

    public void SetBallTexture() {
        golf.GetComponent<MeshRenderer>().material.mainTexture = TextureBall[PlayerPrefs.GetInt("BallUse", 0)];
    }

    public void PlayGolf()
    {
        AmountBall--;
        uiController.SetAmountBall(AmountBall);
    }

    public void PourDone()
    {
        StopAllCoroutines();
        StartCoroutine(DelayFunc(() =>
        {
            character.Idle();
            // Time.timeScale = 1;
            ChangeTime(1);
            CheckEnd();
        }, 2f));
    }

    public void ChangeTime(float time)
    {
        Time.timeScale = time;
        for (int i = 0; i < ObjParent.childCount; i++)
        {
            ObjParent.GetChild(i).GetComponent<ObjMap>().Change();
        }
    }

    public void CheckEnd()
    {
        Plank plankRed = listPlank.Find(plank => plank.isRed && plank.poured);
        if (plankRed != null)
        {
            // GameLose();
            StartCoroutine(DelayFunc(() =>
           {
               if (AmountBall > 0)
               {
                   jsonReader.Read();

                   golf.ReBack();
                   character.Veldle();


               }
               else
               {
                   GameLose();
               }
           }, 0.5f));

            return;

        }
        List<Plank> whitePlank = new List<Plank>();
        listPlank.ForEach(plank =>
        {
            if (!plank.isRed)
            {
                whitePlank.Add(plank);
            }
        });
        bool whiteDone = whitePlank.TrueForAll(plank => !plank.isRed && plank.poured);
        bool flagDone = listFlag.TrueForAll(flag => flag.isFly);
        Debug.Log(whiteDone);
        Debug.Log(flagDone);
        if (whiteDone && flagDone)
        {
            GameWin();
        }
        else
        {
            if (AmountBall <= 0)
            {
                GameLose();
            }
            else
            {
                golf.ReBack();
                character.Veldle();
            }
        }
    }

    public void UpdateFlagFly()
    {
        int flagFly = 0;
        listFlag.ForEach(flag =>
        {
            if (flag.isFly)
            {
                flagFly++;
            }
        });
        // uiController.UpdateFlagFly(flagFly);
    }

    public void GameLose()
    {
        StartCoroutine(DelayFunc(() =>
        {
            uiController.GameLoseEvent();
        }, 0.5f));
        GameDone = true;
    }

    public void GameWin()
    {
        character.Victory();
        uiController.AddScore();
        StartCoroutine(DelayFunc(() =>
        {
            if (StaticData.level >= 4) {
                // AdsController.Instance.ShowInterstitial();
            }
            int numOff = PlayerPrefs.GetInt("RateOff", 0);
            Debug.Log(numOff);
            if (StaticData.level == 2 && numOff == 0 || StaticData.level == 9 && numOff == 1 || StaticData.level == 14 && numOff == 2)
            {
                IARManager.Instance.ShowBox();
                uiController.blackScreen.gameObject.SetActive(true);
            }
            uiController.GameWinEvent();
        }, 2f));
        GameDone = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DelayFunc(Action call, float time)
    {
        yield return new WaitForSeconds(time);
        call();
    }

}
