using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PillowsCanvas : MonoBehaviour {
    Image background, left, right, rear;

    float LeftLeg;
    float RightLeg;
    float LeftPillow;
    float RightPillow;
    float RearPillow;
    public static bool pillowsPressed;
    float pillowPressMinValue;

    float pillowPress;
    float legsDifference;

    float fadeTime;
    float fadeTimeFromCfg;

    void Start () {
        fadeTimeFromCfg = PersistentManagerScript.Instance.config["general"]["fadeTime"].FloatValue;
        fadeTime = 0f;
        pillowsPressed = false;
        pillowPress = MissionManager.pillowPress; 
        legsDifference = MissionManager.pillowsLegsDiff;
        pillowPressMinValue = PersistentManagerScript.Instance.config["general"]["pillowsPressTimeCount"].FloatValue;

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
        if (GameObject.FindGameObjectWithTag("Finish") == null)
        {
            Parameters();
            KeyboardParam();
            HasPillowBeenPressed();
            PillowsPressure();
        }
        else
        {
            fadeTime = 0f;
            background.color = new Color(0f, 0f, 0f, 0f);
            left.color = new Color(0f, 0f, 0f, 0f);
            right.color = new Color(0f, 0f, 0f, 0f);
            rear.color = new Color(0f, 0f, 0f, 0f);
        }
    }

    void PillowsPressure()
    {
        if ((LeftLeg - RightLeg) > legsDifference && RightPillow > pillowPress)
            SetColor();
        if ((RightLeg - LeftLeg) > legsDifference && LeftPillow > pillowPress)
            SetColor();
        if (RearPillow > pillowPress)
            SetColor();
        if(RearPillow < pillowPress && RightPillow < pillowPress && LeftPillow <= pillowPress)
        {
            fadeTime = 0f;
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

    void SetColor()
    {
        fadeTime += Time.deltaTime;
        float intensity = Mathf.Clamp(fadeTime / fadeTimeFromCfg, 0f, 1f);

        background.color = new Color(0f, 0f, 0f, intensity);

        if (intensity >= 0.2f)
        {
            if (LeftPillow >= pillowPress)
                left.color = new Color(0f, 0f, 1f, intensity);
            else
                left.color = new Color(0f, 0f, 0f, 0f);
            if (RightPillow >= pillowPress)
                right.color = new Color(0f, 0f, 1f, intensity);
            else
                right.color = new Color(0f, 0f, 0f, 0f);
            if (RearPillow >= pillowPress)
                rear.color = new Color(0f, 0f, 1f, intensity);
            else
                rear.color = new Color(0f, 0f, 0f, 0f);
        }

    }

    void KeyboardParam()
    {
        LeftLeg = 80f;
        RightLeg = 20f;
        if (Input.GetKey(KeyCode.J)) LeftPillow = 90f;
        else LeftPillow = 0f;
        if (Input.GetKey(KeyCode.K)) RearPillow = 90f;
        else RearPillow = 0f;
        if (Input.GetKey(KeyCode.L)) RightPillow = 90f;
        else RightPillow = 0f;
    }

    void HasPillowBeenPressed()
    {
        if (LeftPillow > pillowPressMinValue || RightPillow > pillowPressMinValue || RearPillow > pillowPressMinValue) pillowsPressed = true;
        else pillowsPressed = false;
    }
}
