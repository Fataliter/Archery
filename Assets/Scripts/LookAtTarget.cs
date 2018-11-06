using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {

    
    string[] tags = { "Target", "Enemy1", "Enemy2", "Enemy3" };
    GameObject target, enemy1, enemy2, enemy3;
    Transform targetLeftSide, targetRightSide;
    GameObject lookAtLeft, lookAtRight, lookAtCentre;

    private void Start()
    {
        lookAtCentre = GameObject.Find("LookAtTarget/LookAtCentre");
        lookAtRight = GameObject.Find("LookAtTarget/LookAtRight");
        lookAtLeft = GameObject.Find("LookAtTarget/LookAtLeft");
    }
    void Update () {
        FindTargets();
        LookAtTargets();
    }

    void FindTargets()
    {
        target = GameObject.FindGameObjectWithTag(tags[0]);
        enemy1 = GameObject.FindGameObjectWithTag(tags[1]);
        enemy2 = GameObject.FindGameObjectWithTag(tags[2]);
        enemy3 = GameObject.FindGameObjectWithTag(tags[3]);
        if(target!=null)
        {
            targetLeftSide = target.transform.Find("LeftSideTarget");
            targetRightSide = target.transform.Find("RightSideTarget");
        }
    }

    void LookAtTargets()
    {
        if (target != null)
        {
            lookAtCentre.transform.LookAt(target.transform.position);
            lookAtLeft.transform.LookAt(targetLeftSide.position);
            lookAtRight.transform.LookAt(targetRightSide.position);
        }
        if (enemy1 != null)
        {
            lookAtCentre.transform.LookAt(enemy1.transform.position);
            lookAtLeft.transform.rotation = Quaternion.identity;
            lookAtRight.transform.rotation = Quaternion.identity;
        }
        if (enemy2 != null)
        {
            lookAtCentre.transform.LookAt(enemy2.transform.position);
            lookAtLeft.transform.rotation = Quaternion.identity;
            lookAtRight.transform.rotation = Quaternion.identity;
        }
        if (enemy3 != null)
        {
            lookAtCentre.transform.LookAt(enemy3.transform.position);
            lookAtLeft.transform.rotation = Quaternion.identity;
            lookAtRight.transform.rotation = Quaternion.identity;
        }
    }
    
}
