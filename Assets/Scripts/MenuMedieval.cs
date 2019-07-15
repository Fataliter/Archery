using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuMedieval : MonoBehaviour
{
    int index = 0;
    int lPrzyciskow = 5;
    public Text textMission, textTraining, textUserID;
    float yOffset;

    void Start()
    {
        PersistentManagerScript.Instance.finishedLoad = false;
        yOffset = textMission.transform.position.y - textTraining.transform.position.y;
        Cursor.visible = false;//Ukrycie pokazanie kursora myszy.
        textUserID.text = "User: " + PersistentManagerScript.Instance.userID;

    }

    void Update()
    {
        JanekStepSteer();
        KeyboardSteer();
        //if(PersistentManagerScript.gotUserID) textUserID.text = PersistentManagerScript.Instance.msg;
        
    }
    void KeyboardSteer()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        { //Jeżeli naciśnięto klawisz "RightArrow"

            if (index < lPrzyciskow - 1)
            {
                index++;
                Vector2 position = transform.position;
                position.y += yOffset;
                transform.position = position;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        { //Jeżeli naciśnięto klawisz "LeftArrow"

            if (index > 0)
            {
                index--;
                Vector2 position = transform.position;
                position.y -= yOffset;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (index == 0)
            {
                SceneManager.LoadScene("Training");
            }
            if (index == 1)
            {
                SceneManager.LoadScene("MenuMedievalMissionChoice");
            }
            if (index == 2)
            {
                SceneManager.LoadScene("MenuTrophies");
            }
            if (index == 3)
            {
                SceneManager.LoadScene("MenuOptions");
            }
            if (index == 4)
            {
                PersistentManagerScript.Instance.WebSocketClose();
                PersistentManagerScript.Instance.HideFolder();
                Application.Quit();
            }
        }
    }

    void JanekStepSteer()
    {
        if (ClickedButton.rightButtonDown)
        { //Jeżeli naciśnięto klawisz "RightButton"
            if (index < lPrzyciskow - 1)
            {
                index++;
                Vector2 position = transform.position;
                position.y += yOffset;
                transform.position = position;
            }
            else
            {
                index = 0;
                Vector2 position = transform.position;
                position.y -= yOffset * 3f;
                transform.position = position;
            }

        }
        if (ClickedButton.leftButtonDown)
        { //Jeżeli naciśnięto klawisz "LeftButton"
            if (index == 0)
            {
                SceneManager.LoadScene("Training");
            }
            if (index == 1)
            {
                SceneManager.LoadScene("MenuMedievalMissionChoice");
            }
            if (index == 2)
            {
                SceneManager.LoadScene("MenuTrophies");
            }
            if (index == 3)
            {
                SceneManager.LoadScene("MenuOptions");
            }
            if (index == 4)
            {
                PersistentManagerScript.Instance.WebSocketClose();
                PersistentManagerScript.Instance.HideFolder();
                //Application.Quit();
            }
        }

    }

}