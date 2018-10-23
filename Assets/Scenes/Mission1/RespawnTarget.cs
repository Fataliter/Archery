using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnTarget : MonoBehaviour {

    public static float xOff = 0;
    public static float zOff = 0;
    public static bool ifDestroy = false;
    public static float timer = 0f;
    public static int hitCounter;

    public GameObject targetPosition;
    public GameObject respawnBandit;
    public SaveMedals saveMedals;

    public Image targetLeft, targetRight;
    public GameObject endMissionParticles;

    GameObject target;
    Transform player;
    Transform looker;

    float endPoints = 0f;
    float targetLocation;
    float rot1, rot2;

    float nextActionTime = 0.2f;
    float period = 0.2f;
    float timerPeriod = 0f;

    void Start() {
        Pillows.legsDifference = 20;
        Pillows.pillowPress = 25;
        hitCounter = 0;
        saveMedals = new SaveMedals();
        targetLocation = 0f;
        RespawnArcherTarget();
    }

    void Update() {
        timerPeriod += Time.deltaTime;
        if (timerPeriod > nextActionTime)
        {
            nextActionTime += period;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            PersistentManagerScript.Instance.data.angle += (player.transform.eulerAngles.y - 360f).ToString() + ",";
            PersistentManagerScript.Instance.data.targetLocation += targetLocation + ",";
        }

        if (ifDestroy == true)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            ifDestroy = false;
            Destroy(target);
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            PersistentManagerScript.Instance.data.hitAngle += (player.transform.eulerAngles.y - 360f).ToString() + ",";
            PersistentManagerScript.Instance.data.timeToHit += timerPeriod.ToString() + ",";
            endPoints += (10f / timer) * PociskDetonacja.points;
            if (hitCounter < 30)
            {
                RespawnArcherTarget();
            }
            /*else if (hitCounter < 23)
            {
                RespawnBandit();
            }*/
            else
            {
                GiveRewards();
            }
        }
        if (hitCounter < 30)
        {
            ShowArrow(IfTargetSeen());
        }
        timer += Time.deltaTime;
    }

    void RespawnArcherTarget()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        looker = GameObject.FindGameObjectWithTag("Looker").GetComponent<Transform>();
        timer = 0f;
        target = GameObject.Instantiate(targetPosition);
        target.name = "target";
        Vector3 vector = transform.position;
        if (hitCounter < 15)
        {
            if (hitCounter % 2 == 0)
                Offsets(10f, 15f, -17f, -15f);
            else
                Offsets(-15f, -10f, -17f, -15f);
        }
        else if (hitCounter < 30)
        {
            if (hitCounter % 2 == 0)
                Offsets(20f, 25f, 0f, 5f);
            else
                Offsets(-20f, -25f, 0f, 5f);
        }
        vector.z = vector.z + zOff;
        vector.x = vector.x + xOff;
        target.transform.position = vector;
        looker.LookAt(target.transform.position);
        targetLocation = looker.transform.localEulerAngles.y - 90;
        if (targetLocation > 180)
            targetLocation = targetLocation - 360;
    }

    void RespawnBandit()
    {
        timer = 0f;
        if (hitCounter == 20)
            respawnBandit.transform.position = new Vector3(200, 50, 250);
        else if (hitCounter == 21)
            respawnBandit.transform.position = new Vector3(170, 50, 250);
        else if (hitCounter == 22)
            respawnBandit.transform.position = new Vector3(230, 50, 250);
        GameObject.Instantiate(respawnBandit);
    }

    void Offsets(float xOffMin, float xOffMax, float zOffMin, float zOffMax)
    {
        xOff = UnityEngine.Random.Range(xOffMin, xOffMax);
        zOff = UnityEngine.Random.Range(zOffMin, zOffMax);
    }

    void GiveRewards()
    {
        if (endPoints > 50)
            PersistentManagerScript.Instance.medalsMenu.medalb1 = 1;
        if (endPoints > 80)
            PersistentManagerScript.Instance.medalsMenu.medals1 = 1;
        if (endPoints > 300)
            PersistentManagerScript.Instance.medalsMenu.medalg1 = 1;
        if (endPoints > 350)
            PersistentManagerScript.Instance.medalsMenu.trophy1 = 1;
        saveMedals.Save();
        PersistentManagerScript.Instance.data.timeOfPlaying = BackToMenu.timePlay;
        PersistentManagerScript.Instance.data.missionName = "village";
        SendData.SaveDataFromMission();
        GameObject.Instantiate(endMissionParticles);
        Debug.Log("punkty: " + endPoints);
        Debug.Log("czas: " + timer);
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