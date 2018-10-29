using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public static bool canRotateSlider = false;

    float RotationSens;
    int LegsDiff;
    float LeftLeg;
    float RightLeg;

    private void Start()
    {
        RotationSens = PersistentManagerScript.Instance.config["general"]["rotationSensitivity"].FloatValue;
        LegsDiff = PersistentManagerScript.Instance.config["general"]["LegsDifferenceForRotation"].IntValue;
    }

    void Update()
    {
        parameters();
        rotation();
    }

    void parameters() 
    {
        LeftLeg = PersistentManagerScript.Instance.mydata.LeftLeg;
        RightLeg = PersistentManagerScript.Instance.mydata.RightLeg;
    }

    void rotation() 
    {
        if (Mathf.Abs(LeftLeg - RightLeg) >= LegsDiff && PlayerShoot.keypressed == false && canRotateSlider)    
            transform.Rotate(0, (RightLeg - LeftLeg) * RotationSens, 0); 
        else
            transform.Rotate(0, 0, 0);
    }
}
