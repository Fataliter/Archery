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
    Transform lookerTransform;
    public static bool hit = false;

    float nextActionTime = 0.2f;
    float period = 0.2f;
    float timer = 0f;

    void Start () {
        hit = false;
        timer = 0f;
        nextActionTime = 0.2f;
        lookerTransform = GameObject.FindGameObjectWithTag("Looker").GetComponent<Transform>();
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
        if(MissionManager.endOfMission==true)
        {
            PersistentManagerScript.Instance.data.timeOfPlaying = MissionManager.timePlayed;
            SendData.SaveDataFromMission();
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
        
        if (lookerTransform.eulerAngles.y > 180)
            PersistentManagerScript.Instance.data.targetLocation += (lookerTransform.eulerAngles.y - 360f).ToString() + ",";
        else
            PersistentManagerScript.Instance.data.targetLocation += lookerTransform.eulerAngles.y.ToString() + ",";
        if (playerTransform.eulerAngles.y > 180)
            PersistentManagerScript.Instance.data.angle += (playerTransform.eulerAngles.y - 360f).ToString() + ",";
        else
            PersistentManagerScript.Instance.data.angle += (playerTransform.eulerAngles.y).ToString() + ",";
    }

}
