using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBrowser : MonoBehaviour {

    float LeftLeg;
    float RightLeg;
    float LeftPillow;
    float RightPillow;
    float RearPillow;
    Transform playerTransform;
    Transform lookerTransform;

    float nextActionTime = 0.2f;
    float period = 0.2f;
    float timer = 0f;

    void Start () {
        timer = 0f;
        nextActionTime = 0.2f;
        lookerTransform = GameObject.FindGameObjectWithTag("Looker").GetComponent<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PersistentManagerScript.Instance.data.missionName = SceneManager.GetActiveScene().name;
    }

    void Update() {
        Parameters();
        timer += Time.deltaTime;
        if (timer > nextActionTime)
        {
            LowerDiagram();
            nextActionTime += period;
        }
        if(MissionManager.endOfMission==true)
        {
            PersistentManagerScript.Instance.data.timeOfPlaying = MissionManager.timePlayed;
            SendData.SaveDataFromMission();
        }
    }
    void Parameters()
    {
        LeftLeg = PersistentManagerScript.Instance.mydata.LeftLeg;
        RightLeg = PersistentManagerScript.Instance.mydata.RightLeg;
        LeftPillow = PersistentManagerScript.Instance.mydata.LeftPillow;
        RightPillow = PersistentManagerScript.Instance.mydata.RightPillow;
        RearPillow = PersistentManagerScript.Instance.mydata.RearPillow;
    }

    void LowerDiagram()
    {
        PersistentManagerScript.Instance.data.pressOnLeftLeg += LeftLeg.ToString() + ",";
        PersistentManagerScript.Instance.data.pressOnRightLeg += RightLeg.ToString() + ",";
        PersistentManagerScript.Instance.data.pressOnLeft += LeftPillow.ToString() + ",";
        PersistentManagerScript.Instance.data.pressOnRight += RightPillow.ToString() + ",";
        PersistentManagerScript.Instance.data.pressOnRear += RearPillow.ToString() + ",";
    }

    void UpperDiagram()
    {
        PersistentManagerScript.Instance.data.angle += (playerTransform.transform.eulerAngles.y + 90f).ToString() + ",";

    }

}
