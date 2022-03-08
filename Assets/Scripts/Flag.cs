using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Flag : MonoBehaviour
{
    // public Transform flag;
    public Material yellowFlag;
    public Material redFlag;
    public SkinnedMeshRenderer meshFlag;
    public GameObject particle;
    public Animator anim;
    public bool isFly = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeColor()
    {
        meshFlag.material = yellowFlag;
        Debug.Log( meshFlag.material);
        Debug.Log(" okkkkkkkkkkkkkkmeshFlag.material");

    }

    void Fly()
    {
        ChangeColor();
        AudioController.instance.PlayAudio(AudioType.Flag, 1f);
        var main = particle.GetComponent<ParticleSystem>().main;
        main.simulationSpeed = 0.5f;
        particle.SetActive(true);
        this.GetComponent<BoxCollider>().enabled = false;
        isFly = true;
        anim.SetTrigger("Collided");
        // anim.Play("Flag");
        // GameController.Instance.UpdateFlagFly();
        // flag.DOMoveY(flag.position.y + 1f, 1f);
        // flag.GetComponent<MeshRenderer>().material.DOFade(0, 1f);
    }
    public void DoneAnim(){
        anim.SetTrigger("DoneAnim");
    }
    void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.tag == "Plank")
        // {
        // }
        Fly();
    }

    public void ResetFlag(){
        isFly=false;
        meshFlag.material=redFlag;
        particle.SetActive(false);
        this.GetComponent<BoxCollider>().enabled = true;
    }
}
