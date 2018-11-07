using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBrowser : MonoBehaviour {

    float leftLeg;
    float rightLeg;
    float leftPillow;
    float rightPillow;
    float rearPillow;
    Transform playerTransform;
    Transform lookerCentre, lookerRight, lookerLeft;
    public static bool hit = false;

    bool oneTime;

    float nextActionTime = 0.2f;
    float period = 0.2f;
    float timer = 0f;

    void Start () {
        oneTime = true;
        hit = false;
        timer = 0f;
        nextActionTime = 0.2f;
        lookerCentre = GameObject.Find("LookAtTarget/LookAtCentre").GetComponent<Transform>();
        lookerRight = GameObject.Find("LookAtTarget/LookAtRight").GetComponent<Transform>();
        lookerLeft = GameObject.Find("LookAtTarget/LookAtLeft").GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PersistentManagerScript.Instance.data.missionName = SceneManager.GetActiveScene().name.ToLower();
    }

    void Update() {
        Parameters();
        timer += Time.deltaTime;
        if (timer > nextActionTime)
        {
            LowerDiagram();
            UpperDiagram();
            nextActionTime += period;
        }
        if(hit)
        {
            if (playerTransform.eulerAngles.y > 180)
                PersistentManagerScript.Instance.data.hitAngle += (playerTransform.eulerAngles.y - 360f).ToString() + ",";
            else
                PersistentManagerScript.Instance.data.hitAngle += (playerTransform.eulerAngles.y).ToString() + ",";
            PersistentManagerScript.Instance.data.timeToHit += timer.ToString() + ",";
            hit = false;
        }
        if(MissionManager.endOfMission==true && oneTime)
        {
            PersistentManagerScript.Instance.data.timeOfPlaying = MissionManager.timePlayed;
            PersistentManagerScript.Instance.data.pillowsLevel = MissionManager.pillowPress.ToString();
            SendData.SaveDataFromMission();
            oneTime = false;
        }
    }
    void Parameters()
    {
        leftLeg = PersistentManagerScript.Instance.mydata.LeftLeg;
        rightLeg = PersistentManagerScript.Instance.mydata.RightLeg;
        leftPillow = PersistentManagerScript.Instance.mydata.LeftPillow;
        rightPillow = PersistentManagerScript.Instance.mydata.RightPillow;
        rearPillow = PersistentManagerScript.Instance.mydata.RearPillow;
    }

    void LowerDiagram()
    {
        PersistentManagerScript.Instance.data.pressOnLeftLeg += leftLeg.ToString() + ",";
        PersistentManagerScript.Instance.data.pressOnRightLeg += rightLeg.ToString() + ",";
        PersistentManagerScript.Instance.data.pressOnLeft += leftPillow.ToString() + ",";
        PersistentManagerScript.Instance.data.pressOnRight += rightPillow.ToString() + ",";
        PersistentManagerScript.Instance.data.pressOnRear += rearPillow.ToString() + ",";
    }

    void UpperDiagram()
    {

        if (lookerCentre.eulerAngles.y > 180)
        {
            PersistentManagerScript.Instance.data.targetLocation += (lookerCentre.eulerAngles.y - 360f).ToString() + ",";
            PersistentManagerScript.Instance.data.targetAngleLeft += (lookerLeft.eulerAngles.y - 360f).ToString() + ",";
            PersistentManagerScript.Instance.data.targetAngleRight += (lookerRight.eulerAngles.y - 360f).ToString() + ",";
        }
        else
        {
            PersistentManagerScript.Instance.data.targetLocation += lookerCentre.eulerAngles.y.ToString() + ",";
            PersistentManagerScript.Instance.data.targetAngleLeft += lookerLeft.eulerAngles.y.ToString() + ",";
            PersistentManagerScript.Instance.data.targetAngleRight += lookerRight.eulerAngles.y.ToString() + ",";
        }
        if (playerTransform.eulerAngles.y > 180)
            PersistentManagerScript.Instance.data.angle += (playerTransform.eulerAngles.y - 360f).ToString() + ",";
        else
            PersistentManagerScript.Instance.data.angle += (playerTransform.eulerAngles.y).ToString() + ",";
    }

}
