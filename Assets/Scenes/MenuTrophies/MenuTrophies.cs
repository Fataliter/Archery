using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuTrophies : MonoBehaviour
{
    public Text actualMission;
    void Start()
    {
        Cursor.visible = false;//Ukrycie pokazanie kursora myszy. 
    }

    void Update()
    {
        if(PersistentManagerScript.Instance.mydata.LeftButton==0)
        {
            while (PersistentManagerScript.Instance.mydata.LeftButton == 0)
            {}
            SceneManager.LoadScene("MenuMedieval");
        }

        if (ControlMenuTrophies.index==0)
            actualMission.text = "Misja 1";
        else if (ControlMenuTrophies.index == 1)
            actualMission.text = "Misja 2";
        else if (ControlMenuTrophies.index == 2)
            actualMission.text = "Misja 3";
        else if (ControlMenuTrophies.index == 3)
            actualMission.text = "Misja 4";

        if (Input.GetKey(KeyCode.Space))
            SceneManager.LoadScene("MenuMedieval");
    }

}