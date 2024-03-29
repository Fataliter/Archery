﻿using UnityEngine;
using UnityEngine.UI;

public class RespawnMission4 : MonoBehaviour {

    int targetHitCount;
    public Image targetLeft, targetRight;
    float xOff, zOff;
    public GameObject target, goblin, dragon;

    void Start()
    {
        targetHitCount = 1;
        xOff = 0f;
        zOff = 0f;
        RespawnArcherTarget();
    }

    void Update()
    {
        ShowArrow(IfTargetSeen());
        if (MissionManager.hit)
        {
            targetHitCount++;
            RespawnArcherTarget();
            MissionManager.hit = false;
        }
    }

    void RespawnArcherTarget()
    {
        if (targetHitCount > 10)
            targetHitCount = 1;
        Vector3 vector = transform.position;
        if (targetHitCount <= 4)
        {
            if (targetHitCount % 2 == 0)
                Offsets(11f, 11.5f, -17.1f, -16.4f);
            else
                Offsets(-11.5f, -11f, -17.1f, -16.4f);
        }
        else if (targetHitCount <= 8 && targetHitCount > 4)
        {
            if (targetHitCount % 2 == 0)
                Offsets(25f, 26f, 0f, 1f);
            else
                Offsets(-26f, -25f, 0f, 1f);
        }
        else if (targetHitCount == 9)
        {
            goblin.transform.position = new Vector3(200f, 20f, 190f);
            GameObject.Instantiate(goblin);
            return;
        }
        else if (targetHitCount == 10)
        {
            GameObject.Instantiate(dragon);
            return;
        }
        vector.z = vector.z + zOff;
        vector.x = vector.x + xOff;
        target.transform.position = vector;
        GameObject.Instantiate(target);
        target.name = "Target";
    }


    void Offsets(float xOffMin, float xOffMax, float zOffMin, float zOffMax)
    {
        xOff = Random.Range(xOffMin, xOffMax);
        zOff = Random.Range(zOffMin, zOffMax);
    }

    string IfTargetSeen()
    {
        Vector3 targetLocation = new Vector3(0, 0, 0);
        if (targetHitCount == 10)
            targetLocation = Camera.main.WorldToViewportPoint(dragon.transform.position);
        else if (targetHitCount == 9)
            targetLocation = Camera.main.WorldToViewportPoint(goblin.transform.position);
        else
            targetLocation = Camera.main.WorldToViewportPoint(target.transform.position);
        if ((targetLocation.x >= 1 && targetLocation.z > 0) || (targetLocation.x <= 0.6f && targetLocation.z < 0))
            return "right";
        else if ((targetLocation.x <= 0 && targetLocation.z > 0) || (targetLocation.x >= 0.6f && targetLocation.z < 0))
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
