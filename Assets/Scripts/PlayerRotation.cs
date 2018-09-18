using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public static bool canRotateSlider = false;

    float Rotation_sensitivity = 0.01f; //mnożnik czułości reakcji gry (obrotu postaci) na zmianę nacisku platform

    float LeftLeg;
    float RightLeg;
    byte RightButton;


    void Update()
    {
        parameters();
        rotation();
    }

    void parameters() //parametry pobierane ze skryptu nasłuchującego aplikacje serwera zintegrowaną z interpreterem pionizatora
    {
        LeftLeg = PersistentManagerScript.Instance.mydata.LeftLeg;
        RightLeg = PersistentManagerScript.Instance.mydata.RightLeg;
        RightButton = (byte)PersistentManagerScript.Instance.mydata.RightButton;
    }

    void rotation() //metoda obrotu postaci
    {
        if (Mathf.Abs(LeftLeg - RightLeg) >= 10 && Arrowshoot.keypressed == false && canRotateSlider)    //warunek - różnica nacisku na platformy >=10 i przycisk od strzału nie jest wciśnięty
            transform.Rotate(0, (RightLeg - LeftLeg) * Rotation_sensitivity, 0); //funkcja rotacji o zadaną wartość
        else
            transform.Rotate(0, 0, 0);
    }
}
