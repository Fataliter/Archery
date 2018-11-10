using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnTraining : MonoBehaviour {

    int targetHitCount;
    public Image targetLeft, targetRight;
    float xOff, zOff;
    public GameObject target;


    void Start () {
        xOff = 0f;
        zOff = 0f;
        RespawnArcherTarget();
    }
	
	void Update () {
        ShowArrow(IfTargetSeen());
        if (MissionManager.hit)
        {
            RespawnArcherTarget();
            MissionManager.hit = false;
        }
    }

    void RespawnArcherTarget()
    {
        Vector3 vector = transform.position;
        Offsets(-25f, 25f, -15f, 5f);
        vector += new Vector3 (xOff, 0, zOff);
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
        Vector3 targetLocation = Camera.main.WorldToViewportPoint(target.transform.position);
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
