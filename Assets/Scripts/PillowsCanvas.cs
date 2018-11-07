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
    public static int pillowsPressed;

    float pillowPress;
    float legsDifference;

    void Start () {
        pillowsPressed = 0;
        pillowPress = MissionManager.pillowPress; 
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
        //Parameters();
        KeyboardParam();
        HasPillowBeenPressed();
        PillowsPressure();
    }

    void PillowsPressure()
    {
        if ((LeftLeg - RightLeg) > legsDifference && RightPillow > pillowPress)
            SetColor(RightPillow);
        if ((RightLeg - LeftLeg) > legsDifference && LeftPillow > pillowPress)
            SetColor(LeftPillow);
        if (RearPillow > pillowPress)
            SetColor(RearPillow);
        if(RearPillow < pillowPress && RightPillow < pillowPress && LeftPillow <= pillowPress)
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
        else
            left.color = new Color(0f, 0f, 0f, 0f);
        if (RightPillow >= pillowPress)
            right.color = new Color(0f, 0f, 220f, (RightPillow / 100f) + 0.4f);
        else
            right.color = new Color(0f, 0f, 0f, 0f);
        if (RearPillow >= pillowPress)
            rear.color = new Color(0f, 0f, 220f, (RearPillow / 100f) + 0.4f);
        else
            rear.color = new Color(0f, 0f, 0f, 0f);

    }

    void KeyboardParam()
    {
        LeftLeg = 80f;
        RightLeg = 20f;
        if (Input.GetKey(KeyCode.J)) LeftPillow = 60f;
        else LeftPillow = 0f;
        if (Input.GetKey(KeyCode.K)) RearPillow = 60f;
        else RearPillow = 0f;
        if (Input.GetKey(KeyCode.L)) RightPillow = 60f;
        else RightPillow = 0f;
    }

    void HasPillowBeenPressed()
    {
        if (LeftPillow > 80 || RightPillow > 80 || RearPillow > 80)  pillowsPressed = 3; 
        else if (LeftPillow > 55 || RightPillow > 55 || RearPillow > 55) pillowsPressed = 2;
        else if (LeftPillow > 30 || RightPillow > 30 || RearPillow > 30) pillowsPressed = 1;
        else pillowsPressed = 0;
    }
}
