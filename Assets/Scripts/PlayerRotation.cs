using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public static bool canRotateSlider = false;

    float rotationSens;
    int legsDiff;
    float leftLeg;
    float rightLeg;

    private void Start()
    {
        rotationSens = PersistentManagerScript.Instance.config["general"]["rotationSensitivity"].FloatValue;
        legsDiff = PersistentManagerScript.Instance.config["general"]["LegsDifferenceForRotation"].IntValue;
    }

    void Update()
    {
        parameters();
        rotation();
    }

    void parameters() 
    {
        leftLeg = PersistentManagerScript.Instance.mydata.LeftLeg;
        rightLeg = PersistentManagerScript.Instance.mydata.RightLeg;
    }

    void rotation() 
    {
        if (Mathf.Abs(leftLeg - rightLeg) >= legsDiff && PlayerShoot.keypressed == false && canRotateSlider)    
            transform.Rotate(0, (rightLeg - leftLeg) * rotationSens, 0); 
        else
            transform.Rotate(0, 0, 0);
    }
}
