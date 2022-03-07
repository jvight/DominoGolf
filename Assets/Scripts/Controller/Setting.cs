using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject BoxViewMusic;
    public GameObject TextViewMusic;
    public GameObject BoxViewSound;
    public GameObject TextViewSound;
    public Sprite sprBoxOn;
    public Sprite sprBoxOff;
    bool isMusic = true;
    bool isSound = true;
    // Start is called before the first frame update
    void Start()
    {
        isMusic = PlayerPrefs.GetInt("Music", 0) == 0 ? true : false;
        isSound = PlayerPrefs.GetInt("Sound", 0) == 0 ? true : false;
        //Music
        int xM = isMusic ? 290 : 155;
        BoxViewMusic.GetComponent<Image>().sprite = isMusic ? sprBoxOn : sprBoxOff;
        BoxViewMusic.transform.localPosition = new Vector3(xM, 0, 0);
        int x2M = isMusic ? 155 : 290;
        TextViewMusic.transform.localPosition = new Vector3(x2M, -3, 0);
        TextViewMusic.GetComponent<Text>().text = isMusic ? "ON" : "OFF";
        TextViewMusic.GetComponent<Text>().color = isMusic ? new Color(87, 87, 87, 255) : new Color(31, 197, 23, 255);
        //Sound
        int xS = isSound ? 290 : 155;
        BoxViewSound.GetComponent<Image>().sprite = isSound ? sprBoxOn : sprBoxOff;
        BoxViewSound.transform.localPosition = new Vector3(xS, 0, 0);
        int x2S = isSound ? 155 : 290;
        TextViewSound.transform.localPosition = new Vector3(x2S, -3, 0);
        TextViewSound.GetComponent<Text>().text = isSound ? "ON" : "OFF";
        TextViewSound.GetComponent<Text>().color = isSound ? new Color(87, 87, 87, 255) : new Color(31, 197, 23, 255);
    }

    public void OnClickMusic()
    {
        isMusic = !isMusic;
        int x = isMusic ? 290 : 155;
        BoxViewMusic.transform.DOLocalMoveX(x, 0.3f);
        BoxViewMusic.GetComponent<Image>().sprite = isMusic ? sprBoxOn : sprBoxOff;
        int x2 = isMusic ? 155 : 290;
        TextViewMusic.transform.localPosition = new Vector3(x2, -3, 0);
        TextViewMusic.GetComponent<Text>().text = isMusic ? "ON" : "OFF";
        TextViewMusic.GetComponent<Text>().color = isMusic ? new Color(87, 87, 87, 255) : new Color(31, 197, 23, 255);
        PlayerPrefs.SetInt("Music", isMusic ? 0 : 1);
    }
    public void OnClickSound()
    {
        isSound = !isSound;
        int x = isSound ? 290 : 155;
        BoxViewSound.transform.DOLocalMoveX(x, 0.3f);
        BoxViewSound.GetComponent<Image>().sprite = isSound ? sprBoxOn : sprBoxOff;
        int x2 = isSound ? 155 : 290;
        TextViewSound.transform.localPosition = new Vector3(x2, -3, 0);
        TextViewSound.GetComponent<Text>().text = isSound ? "ON" : "OFF";
        TextViewSound.GetComponent<Text>().color = isSound ? new Color(87, 87, 87, 255) : new Color(31, 197, 23, 255);
        PlayerPrefs.SetInt("Sound", isSound ? 0 : 1);
    }
}
