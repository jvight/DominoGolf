using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Golf : MonoBehaviour
{
    public Character character;
    public DrawTrajectory drawTrajectory;
    public GameObject trailFx;
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;
    private Rigidbody rb;

    private bool isShoot = false;
    Vector3 oldPos;
    Vector3 oldAngle;
    bool isTouch = false;
    bool touchUI = false;
    bool canTouch = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldPos = transform.position;
        // oldAngle = transform.eulerAngles;
    }

    public void ReBack()
    {
        trailFx.SetActive(false);
        character.SetTimeAnim(1);
        Debug.Log(oldPos);
        transform.position = oldPos;
        // transform.eulerAngles = oldAngle;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        gameObject.SetActive(true);

        StartCoroutine(DelayFunc(() =>
        {
            trailFx.SetActive(true);

        }, 0.5f));
        isShoot = false;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        if (results.Count > 0)
        {
            if (!results[0].gameObject.GetComponent<Button>())
            {
                return false;
            }
        }
        return results.Count > 0;
    }

    void Update()
    {
        if (IsPointerOverUIObject() && canTouch)
        {
            canTouch = false;
            StartCoroutine(DelayFunc(() =>
            {
                canTouch = true;
            }, 0.3f));
            return;
        }
        if (isShoot || GameController.Instance.AmountBall <= 0 || GameController.Instance.GameDone || !canTouch || !StaticData.game_start) { return; }
        if (Input.GetMouseButtonDown(0) && canTouch)
        {
            mousePressDownPos = Input.mousePosition;
            isTouch = true;
            Debug.Log("tocuh start");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("touch end");
            if (!isTouch) { return; }
            isTouch = false;
            drawTrajectory.HideLine();
            mouseReleasePos = Input.mousePosition;
            var f = Vector3.ClampMagnitude(mousePressDownPos - mouseReleasePos, 800);
            Shoot(f);
        }
        if (isTouch)
        {
            // if (touchUI) { return; }
            // Debug.Log("touch move");
            mouseReleasePos = Input.mousePosition;
            Vector3 force = mousePressDownPos - mouseReleasePos;
            force = Vector3.ClampMagnitude(force, 800);
            Vector3 forceV = new Vector3(force.x, Math.Abs(force.y + 200), Math.Abs(force.y + 100)) * forceMultiplier;
            // Debug.Log(force.magnitude);
            drawTrajectory.UpdateTrajectory(forceV, rb, transform.position);
        }
        // if (Input.touchCount > 0)
        // {
        //     Touch touch = Input.GetTouch(0);
        //     if (touch.phase == TouchPhase.Began)
        //     {
        //         mousePressDownPos = touch.position;
        //     }
        //     else if (touch.phase == TouchPhase.Moved)
        //     {
        //         mouseReleasePos = touch.position;
        //         Vector3 force = mousePressDownPos - mouseReleasePos;
        //         Vector3 forceV = new Vector3(force.x, Math.Abs(force.y + 200), Math.Abs(force.y + 100)) * forceMultiplier;
        //         drawTrajectory.UpdateTrajectory(forceV, rb, transform.position);
        //     }
        //     else if (touch.phase == TouchPhase.Ended)
        //     {
        //         drawTrajectory.HideLine();
        //         mouseReleasePos = touch.position;
        //         Shoot(mousePressDownPos - mouseReleasePos);
        //     }
        // }
    }


    private float forceMultiplier = 0.5f;
    void Shoot(Vector3 Force)
    {
        if (isShoot)
            return;
        character.SetTimeAnim(0.5f);
        // Time.timeScale = 2;
        GameController.Instance.ChangeTime(2);
        character.Hit();
        StartCoroutine(DelayFunc(() =>
        {
            AudioController.instance.PlayAudio(AudioType.HitGolf, 1f);
            GameController.Instance.PlayGolf();
            rb.AddForce(new Vector3(Force.x, Math.Abs(Force.y + 200), Math.Abs(Force.y + 100)) * forceMultiplier);
        }, 1.17f * Time.timeScale));
        isShoot = true;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Plank")
        {
            AudioController.instance.PlayAudio(AudioType.GolfHitOther, 0.5f);
            StartCoroutine(DelayFunc(() =>
            {
                gameObject.SetActive(false);
            }, 0.03f));
            // Time.timeScale = 2.5f;
            GameController.Instance.ChangeTime(2.5f);
            // character.SetTimeAnim(0.5f);
        }
        else if (collisionInfo.gameObject.tag == "Border" || collisionInfo.gameObject.tag == "Object")
        {
            AudioController.instance.PlayAudio(AudioType.GolfHitOther, 0.5f);
        }
        Debug.Log(collisionInfo.gameObject.tag);
        if (isShoot)
        {
            GameController.Instance.PourDone();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (isShoot)
        {
            GameController.Instance.PourDone();
        }
    }
    IEnumerator DelayFunc(Action call, float time)
    {
        yield return new WaitForSeconds(time);
        call();
    }
}
