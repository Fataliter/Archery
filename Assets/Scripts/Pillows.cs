using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillows : MonoBehaviour {
    public static int legsDifference, pillowPress;

    public static float WindForce;
    float LeftLeg;
    float RightLeg;
    float LeftPillow;
    float RightPillow;
    float RearPillow;

    float nextActionTime = 0.2f;
    float period = 0.2f;
    float timer = 0f;

    void Update () {
        Parameters();
        WindForceCalc();
        WindForceCalcKeyboard();
        timer += Time.deltaTime;
        if (timer > nextActionTime)
        {
            nextActionTime += period;
            PersistentManagerScript.Instance.data.pressOnLeftLeg += LeftLeg.ToString() + ",";
            PersistentManagerScript.Instance.data.pressOnRightLeg += RightLeg.ToString() + ",";
            PersistentManagerScript.Instance.data.pressOnLeft += LeftPillow.ToString() + ",";
            PersistentManagerScript.Instance.data.pressOnRight += RightPillow.ToString() + ",";
            PersistentManagerScript.Instance.data.pressOnRear += RearPillow.ToString() + ",";
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

    void WindForceCalc()
    {
        if ((LeftLeg - RightLeg) > legsDifference && RightPillow > pillowPress)
            WindForce = RightPillow;
        else if ((RightLeg - LeftLeg) > legsDifference && LeftPillow > pillowPress)
            WindForce = -LeftPillow;
        else
            WindForce = 0f;
        //Debug.Log("wiatr= " + WindForce);
    }

    void WindForceCalcKeyboard()
    {
        if (Input.GetKey(KeyCode.J))
            WindForce = 50f;
        else if (Input.GetKey(KeyCode.K))
            WindForce = -50f;
        else
            WindForce = 0f;
    }
}
