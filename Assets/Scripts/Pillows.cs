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
    

	void Update () {
        Parameters();
        //WindForceCalc();
        WindForceCalcKeyboard();
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
        Debug.Log("wiatr= " + WindForce);
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
