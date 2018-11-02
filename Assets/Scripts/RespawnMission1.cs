using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnMission1 : MonoBehaviour {

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
                Offsets(10f, 15f, -17f, -15f);
            else
                Offsets(-15f, -10f, -17f, -15f);
        }
        else if (targetHitCount <= 8 && targetHitCount > 4)
        {
            if (targetHitCount % 2 == 0)
                Offsets(20f, 25f, 0f, 5f);
            else
                Offsets(-20f, -25f, 0f, 5f);
        }
        vector.z = vector.z + zOff;
        vector.x = vector.x + xOff;
        target.transform.position = vector;
        GameObject.Instantiate(target);
        target.name = "Target";
        Debug.Log(target.transform.position);
    }


    void Offsets(float xOffMin, float xOffMax, float zOffMin, float zOffMax)
    {
        xOff = Random.Range(xOffMin, xOffMax);
        zOff = Random.Range(zOffMin, zOffMax);
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
