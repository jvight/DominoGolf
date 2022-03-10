using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class TutController : MonoBehaviour
{
    public Transform hand;
    public TMP_Text tutText;
    public float timeChange;
    public bool isActive=false;
    public bool canActive=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private Sequence seq;
    public void MoveTut(){
        seq = DOTween.Sequence()
        .SetLoops(-1)
        .Append(hand.DOMoveX(-2f, 1f))
        .Append(hand.DOMoveX(0f, 1f))
        .Append(hand.DOMoveX(2f, 1f))
        .Append(hand.DOMoveX(0f, 1f))
        .AppendInterval(0.3f)
        .AppendCallback(() =>
        {
            hand.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("callback");
        })
        .AppendInterval(1f)
        .AppendCallback(() =>
        {
            hand.GetComponent<MeshRenderer>().enabled = true;
            tutText.text = "DRAG TO ADJUST POWER";

        })
        .AppendInterval(0.3f)
        .Append(hand.DOLocalMove(new Vector3(0, 0.8f, -4.98f), 1f))
        .Append(hand.DOLocalMove(new Vector3(0, 2.38f, -3.25f), 1f))
        .Append(hand.DOLocalMove(new Vector3(0, 4f, -1.97f), 1f))
        .Append(hand.DOLocalMove(new Vector3(0, 2.38f, -3.25f), 1f))
        .AppendInterval(0.3f)
        .AppendCallback(() =>
        {
            hand.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("callback");
        })
        .AppendInterval(1f)
        .AppendCallback(() =>
        {
            hand.GetComponent<MeshRenderer>().enabled = true;
            tutText.text = "HOLD AND MOVE TO AIM";
        })
        .AppendInterval(0.3f)
        .Play();
    }
    public void ResetTut(){
        seq.Kill();
        tutText.text="HOLD AND MOVE TO AIM";
        hand.localPosition=new Vector3(0, 2.38f, -3.25f);
    }
    public void ActiveTut()
    {
        gameObject.SetActive(true);
        hand.gameObject.SetActive(true);
        tutText.gameObject.SetActive(true);
        isActive=true;
    }

    // Update is called once per frame
    void Update()
    {
        

        // Debug.Log("update");
    }
}
