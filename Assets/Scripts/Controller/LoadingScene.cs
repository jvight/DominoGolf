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
        StaticData.loadding = true;
        AudioController.instance.PlayAudio(AudioType.BGM, 0.4f);
        progress.DOFillAmount(1f, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
