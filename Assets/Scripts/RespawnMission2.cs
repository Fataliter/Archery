using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnMission2 : MonoBehaviour {
    

    public GameObject bandit;

    int targetHitCount;
    public Image targetLeft, targetRight;
    float xOff, zOff;
    public GameObject target;
    
    void Start () {
        targetHitCount = 1;
        xOff = 0f;
        zOff = 0f;
        RespawnArcherTarget();
    }
	
	void Update () {
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
        if (targetHitCount > 8)
            targetHitCount = 1;
        Vector3 vector = transform.position;
        if (targetHitCount <= 4)
        {
            if (targetHitCount % 2 == 0)
                Offsets(5f, 10f, -9f, -7f);
            else
                Offsets(-20f, -15f, -13f, -11f);
        }
        else if (targetHitCount == 5)
        {
            bandit.transform.position = new Vector3(205f, 30f, 20f);
            GameObject.Instantiate(bandit);
            return;
        }
        else if (targetHitCount <= 8 && targetHitCount > 4)
        {
            if (targetHitCount % 2 == 0)
                Offsets(10f, 15f, 28f, 31f);
            else
                Offsets(-40f, -35f, 15f, 18f);
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
        Vector3 targetLocation = new Vector3(0,0,0);
        if (targetHitCount == 5)
        {
            targetLocation = Camera.main.WorldToViewportPoint(bandit.transform.position);
        }
        else
        {
            targetLocation = Camera.main.WorldToViewportPoint(target.transform.position);
        }

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
