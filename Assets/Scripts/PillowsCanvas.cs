using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillowsCanvas : MonoBehaviour {
    Image background, left, right, rear;

    float LeftLeg;
    float RightLeg;
    float LeftPillow;
    float RightPillow;
    float RearPillow;

    float pillowPress;
    float legsDifference;

    void Start () {
        pillowPress = MissionManager.pillowsPress;
        legsDifference = MissionManager.pillowsLegsDiff;

        GameObject backgroundObject = GameObject.Find("BlackScreen");
        background = backgroundObject.GetComponent<Image>();

        GameObject leftObject = GameObject.Find("PillowLeft");
        left = leftObject.GetComponent<Image>();
        GameObject rightObject = GameObject.Find("PillowRight");
        right = rightObject.GetComponent<Image>();
        GameObject rearObject = GameObject.Find("PillowRear");
        rear = rearObject.GetComponent<Image>();
    }
	
	void Update () {
        Parameters();
        PillowsPressure();
    }

    void PillowsPressure()
    {
        if ((LeftLeg - RightLeg) > legsDifference && RightPillow > pillowPress)
            SetColor(RightPillow);
        else if ((RightLeg - LeftLeg) > legsDifference && LeftPillow > pillowPress)
            SetColor(LeftPillow);
        else if (RearPillow > pillowPress)
            SetColor(RearPillow);
        else
        {
            background.color = new Color(0f, 0f, 0f, 0f);
            left.color = new Color(0f, 0f, 0f, 0f);
            right.color = new Color(0f, 0f, 0f, 0f);
            rear.color = new Color(0f, 0f, 0f, 0f);
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

    void SetColor(float pressure)
    {
        float intensity = (pressure / 100f) + 0.4f;
        background.color = new Color(0f, 0f, 0f, intensity);

        if (LeftPillow >= pillowPress)
            left.color = new Color(0f, 0f, 220f, (LeftPillow / 100f) + 0.4f);
        if (RightPillow >= pillowPress)
            right.color = new Color(0f, 0f, 220f, (RightPillow / 100f) + 0.4f);
        if (RearPillow >= pillowPress)
            rear.color = new Color(0f, 0f, 220f, (RearPillow / 100f) + 0.4f);
    }
}
