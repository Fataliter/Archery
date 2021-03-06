﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class PlayerRotation : MonoBehaviour
{
    public static bool canRotateSlider = false;

    float rotationSens;
    int legsDiff;
    float leftLeg;
    float rightLeg;
    bool leftRange, rightRange;
    

    private void Awake()
    {
        if (PersistentManagerScript.Instance.config["general"]["keyboardSteerPlayer"].IntValue == 1)
            this.enabled = false;
        else
            this.enabled = true;

    }

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
        rightRange = transform.eulerAngles.y >= -10 && transform.eulerAngles.y <= 140;
        leftRange = transform.eulerAngles.y <= 370 && transform.eulerAngles.y >= 220;
    }

    void rotation()
    {
        if (Mathf.Abs(leftLeg - rightLeg) >= legsDiff && PlayerShoot.keypressed == false && canRotateSlider && (leftRange || rightRange))
            transform.Rotate(0, (rightLeg - leftLeg) * Time.deltaTime * rotationSens, 0);
        else
        {
            if (!rightRange && transform.eulerAngles.y <= 180)
                transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 140, 0));
            else if (!leftRange && transform.eulerAngles.y > 180)
                transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 220, 0));
            else
                transform.Rotate(0, 0, 0);
        }
    }

}