﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation_keyboard : MonoBehaviour
{
    public static bool canRotateKeyboard = false;
    public float rotation_sensitivity = 1f; //mnożnik czułości reakcji gry (obrotu postaci) na zmianę nacisku platform
    bool leftRange, rightRange;


    private void Awake()
    {
        if (PersistentManagerScript.Instance.config["general"]["keyboardSteerPlayer"].IntValue == 1)
            this.enabled = true;
        else
            this.enabled = false;
    }

    void Update()
    {
        rightRange = transform.eulerAngles.y >= -10 && transform.eulerAngles.y <= 140;
        leftRange = transform.eulerAngles.y <= 370 && transform.eulerAngles.y >= 220;
        
        if (Input.GetAxis("Horizontal") != 0 && !Input.GetKey(KeyCode.Space) && canRotateKeyboard && (leftRange || rightRange))
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotation_sensitivity, 0);
        }
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
