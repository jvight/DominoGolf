using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutController : MonoBehaviour
{
    public Transform hand;
    
    // Start is called before the first frame update
    void Start()
    {
        var seq=DOTween.Sequence()
        .Append(hand.DOScale(0.4f,1f))
        .Append(hand.DOMoveX(-2f,1f))
        .Append(hand.DOMoveX(0f,1f))
        .Append(hand.DOMoveX(2f,1f))
        .Append(hand.DOMoveX(0f,1f))
        .Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
