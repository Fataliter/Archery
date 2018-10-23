using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public static bool canRotateSlider = false;

    float Rotation_sensitivity = 0.01f/2; 

    float LeftLeg;
    float RightLeg;
    byte RightButton;


    void Update()
    {
        parameters();
        rotation();
    }

    void parameters() 
    {
        LeftLeg = PersistentManagerScript.Instance.mydata.LeftLeg;
        RightLeg = PersistentManagerScript.Instance.mydata.RightLeg;
        RightButton = (byte)PersistentManagerScript.Instance.mydata.RightButton;
    }

    void rotation() 
    {
        if (Mathf.Abs(LeftLeg - RightLeg) >= 20 && Arrowshoot.keypressed == false && canRotateSlider)    
            transform.Rotate(0, (RightLeg - LeftLeg) * Rotation_sensitivity, 0); 
        else
            transform.Rotate(0, 0, 0);
    }
}
