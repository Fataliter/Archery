using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuTrophies : MonoBehaviour
{
    public Sprite mission1, mission2, mission3, mission4;
    Image background;
    public Text actualMission;
    void Start()
    {
        Cursor.visible = false;//Ukrycie pokazanie kursora myszy.
        GameObject backgroundObject = GameObject.Find("Back");
        background = backgroundObject.GetComponent<Image>();
    }

    void Update()
    {
        if(ClickedButton.leftButtonDown || Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("MenuMedieval");
        }

        if (ControlMenuTrophies.index == 0)
        {
            actualMission.text = "Wioska";
            background.sprite = mission1;
        }
        else if (ControlMenuTrophies.index == 1)
        {
            actualMission.text = "Jezioro";
            background.sprite = mission2;
        }
        else if (ControlMenuTrophies.index == 2)
        {
            actualMission.text = "";
            background.sprite = mission3;
        }
        else if (ControlMenuTrophies.index == 3)
        {
            actualMission.text = "";
            background.sprite = mission4;
        }
        
    }

}