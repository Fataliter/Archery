using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation_keyboard : MonoBehaviour
{
    public static bool canRotateKeyboard = false;
    public float Rotation_sensitivity = 1f; //mnożnik czułości reakcji gry (obrotu postaci) na zmianę nacisku platform


    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && !Input.GetKey(KeyCode.Space) && canRotateKeyboard)
            transform.Rotate(0, Input.GetAxis("Horizontal") * Rotation_sensitivity, 0);
        else
            transform.Rotate(0, 0, 0);
    }
}
