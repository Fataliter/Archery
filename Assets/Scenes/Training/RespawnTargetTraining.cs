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
    float rot1, rot2;
    public Image targetLeft, targetRight;

    void Start () {
        Pillows.legsDifference = 10;
        Pillows.pillowPress = 5;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        RespawnArcherTarget();
    }
	
	void Update ()
    {
        if (ifDestroy == true)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            ifDestroy = false;
            Destroy(target);
            rot2 = player.transform.localEulerAngles.y;
            PersistentManagerScript.Instance.data.angle += (rot1 - rot2).ToString() + ",";
            PersistentManagerScript.Instance.data.timeToHit += timer.ToString() + ",";
            PillowsData();
            RespawnArcherTarget();
        }
        ShowArrow(IfTargetSeen());
        timer += Time.deltaTime;
    }

    public void RespawnArcherTarget()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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

    void PillowsData()
    {
        PersistentManagerScript.Instance.data.timeOnLeftPillow += Pillows.leftPillowTimer.ToString() + ",";
        PersistentManagerScript.Instance.data.timeOnRightPillow += Pillows.rightPillowTimer.ToString() + ",";
        PersistentManagerScript.Instance.data.timeOnRearPillow += Pillows.rearPillowTimer.ToString() + ",";
        Pillows.leftPillowTimer = 0f;
        Pillows.rightPillowTimer = 0f;
        Pillows.rearPillowTimer = 0f;
    }
}