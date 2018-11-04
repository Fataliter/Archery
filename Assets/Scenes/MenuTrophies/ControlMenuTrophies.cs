using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenuTrophies : MonoBehaviour {

    public static int index = 0;
    public int lMisji = 4;
    public RespawnMedals resp;
	
	void Update () {
        if (PersistentManagerScript.Instance.mydata.RightButton == 0)
        { //Jeżeli naciśnięto klawisz "RightArrow"
            while (PersistentManagerScript.Instance.mydata.RightButton == 0)
            { }
            if (index < lMisji - 1)
            {
                index++;
                resp.Respawn();
            }
            else
            {
                index = 0;
                resp.Respawn();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        { //Jeżeli naciśnięto klawisz "RightArrow"

            if (index < lMisji - 1)
            {
                index++;
                resp.Respawn();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        { //Jeżeli naciśnięto klawisz "LeftArrow"

            if (index > 0)
            {
                index--;
                resp.Respawn();
            }
        }
    }
}
