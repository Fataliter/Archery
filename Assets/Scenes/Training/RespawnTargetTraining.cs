using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;


public class RespawnTargetTraining : MonoBehaviour {

    public static bool ifDestroy = false;
    public static float xOff;
    public static float zOff;
    public static float timer=0f;
    public GameObject targetPosition;
    GameObject target;
    Transform player;
    Transform looker;
    float rot1, rot2;
    public Image targetLeft, targetRight;

    float nextActionTime = 0.2f;
    float period = 0.2f;
    float timerPeriod = 0f;

    float targetLocation;

    void Start () {
        Pillows.legsDifference = 10;
        Pillows.pillowPress = 5;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        targetLocation = 0f;
        RespawnArcherTarget();
    }
	
	void Update ()
    {
        timerPeriod += Time.deltaTime;
        if (timerPeriod > nextActionTime)
        {
            nextActionTime += period;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            PersistentManagerScript.Instance.data.angle += player.transform.localEulerAngles.y.ToString() + ",";
            PersistentManagerScript.Instance.data.targetLocation += targetLocation + ",";
        }

        if (ifDestroy == true)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            ifDestroy = false;
            Destroy(target);
            rot2 = player.transform.localEulerAngles.y;
            //PersistentManagerScript.Instance.data.angle += (rot1 - rot2).ToString() + ",";
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            PersistentManagerScript.Instance.data.hitAngle += player.transform.localEulerAngles.y.ToString() + ",";
            PersistentManagerScript.Instance.data.timeToHit += timer.ToString() + ",";
            RespawnArcherTarget();
        }
        ShowArrow(IfTargetSeen());
        timer += Time.deltaTime;
    }

    public void RespawnArcherTarget()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        looker = GameObject.FindGameObjectWithTag("Looker").GetComponent<Transform>();
        timer = 0f;
        target = GameObject.Instantiate(targetPosition);
        rot1 = player.transform.localEulerAngles.y;
        target.name = "target";
        Vector3 vector = transform.position;
        xOff = UnityEngine.Random.Range(-20f, 40f);
        zOff = UnityEngine.Random.Range(0f, 5f);
        vector.z = vector.z + zOff;
        vector.x = vector.x + xOff;
        target.transform.position = vector;
        looker.LookAt(target.transform.position);
        targetLocation = looker.transform.localEulerAngles.y - 90;
        if (targetLocation > 180)
            targetLocation = targetLocation - 360;
    }

    string IfTargetSeen()
    {
        Vector3 targetLocation = Camera.main.WorldToViewportPoint(target.transform.position);
        if (!(targetLocation.x < 1 && targetLocation.x > 0 && targetLocation.z > 0))
            if (targetLocation.x >= 1)
                return "right";
            else 
                return "left";
        else
            return "visible";
    }

    void ShowArrow(string whichside)
    {
        if (whichside == "left")
            targetLeft.enabled = true;
        else
            targetLeft.enabled = false;

        if (whichside == "right")
            targetRight.enabled = true;
        else
            targetRight.enabled = false;
    }
}