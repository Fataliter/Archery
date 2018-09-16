﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class RespawnTarget : MonoBehaviour {

    public static float xOff = 0;
    public static float zOff = 0;
    public static bool ifDestroy = false;
    public static float timer = 0f;
    public static int hitCounter = 19;
    float endPoints = 0f;
    public GameObject targetPosition;
    public GameObject respawnBandit;
    GameObject target;
    Transform player;
    float rot1, rot2;
    public Image targetLeft, targetRight;

    void Start () {
        RespawnArcherTarget();
    }
	
	void Update () {
        if (ifDestroy == true)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Destroy(target);
            ifDestroy = false;
            rot2 = player.transform.localEulerAngles.y;
            PersistentManagerScript.Instance.data.angle += (rot1 - rot2).ToString() + ",";
            PersistentManagerScript.Instance.data.timeToHit += timer.ToString() + ",";
            endPoints += (10f / timer) * PociskDetonacja.points;
            if (hitCounter < 20)
            {
                RespawnArcherTarget();
            }
            if (hitCounter == 20)
            {
                RespawnBandit();
                //GiveRewards();
            }
        }
        if (hitCounter < 20)
            ShowArrow(IfTargetSeen());
        timer += Time.deltaTime;
    }

    void RespawnArcherTarget()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = 0f;
        target = GameObject.Instantiate(targetPosition);
        rot1 = player.transform.localEulerAngles.y;
        target.name = "target";
        Vector3 vector = transform.position;
        if (hitCounter < 10)
        {
            if (hitCounter % 2 == 0)
                Offsets(10f, 15f, -17f, -15f);
            else
                Offsets(-15f, -10f, -17f, -15f);
        }
        else if (hitCounter < 20)
        {
            if (hitCounter % 2 == 0)
                Offsets(20f, 25f, 0f, 5f);
            else
                Offsets(-20f, -25f, 0f, 5f);
        }
        vector.z = vector.z + zOff;
        vector.x = vector.x + xOff;
        target.transform.position = vector;
    }

    void RespawnBandit()
    {
        target = GameObject.Instantiate(respawnBandit);
    }

    void Offsets(float xOffMin, float xOffMax, float zOffMin, float zOffMax)
    {
        xOff = UnityEngine.Random.Range(xOffMin, xOffMax);
        zOff = UnityEngine.Random.Range(zOffMin, zOffMax);
    }

    void Save()
    {
        FileStream file = File.Create(Application.persistentDataPath + "/trophies.data");
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, PersistentManagerScript.Instance.medalsMenu);
        file.Close();
    }

    void GiveRewards()
    {
        if (endPoints > 20)
            PersistentManagerScript.Instance.medalsMenu.medalb1 = 1;
        if (endPoints > 50)
            PersistentManagerScript.Instance.medalsMenu.medals1 = 1;
        if (endPoints > 80)
            PersistentManagerScript.Instance.medalsMenu.medalg1 = 1;
        if (endPoints > 150)
            PersistentManagerScript.Instance.medalsMenu.trophy1 = 1;
        Save();
        PersistentManagerScript.Instance.data.timeOfPlaying = BackToMenu.timePlay;
        PersistentManagerScript.Instance.data.missionName = "mission1";
        SendData.SaveDataFromMission();
        SceneManager.LoadScene("MenuMedievalMissionChoice");
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