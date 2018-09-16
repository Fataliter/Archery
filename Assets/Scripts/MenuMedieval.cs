using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuMedieval : MonoBehaviour
{
    int index = 0;
    public int lPrzyciskow = 4;
    public Text textMission, textTraining;
    float yOffset;

    void Start()
    {
        yOffset = textTraining.transform.position.y - textMission.transform.position.y;
        Cursor.visible = false;//Ukrycie pokazanie kursora myszy.
    }

    // Update is called once per frame
    void Update()
    {
        if (PersistentManagerScript.Instance.mydata.RightButton == 0)
        { //Jeżeli naciśnięto klawisz "RightButton"
            while (PersistentManagerScript.Instance.mydata.RightButton == 0)
            { }
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
        if (PersistentManagerScript.Instance.mydata.LeftButton == 0)
        { //Jeżeli naciśnięto klawisz "LeftButton"
            while (PersistentManagerScript.Instance.mydata.LeftButton == 0)
            { }
            if (index == 0)
            {
                SceneManager.LoadScene("MenuMedievalMissionChoice");
            }
            if (index == 1)
            {
                SceneManager.LoadScene("Training");
            }
            if (index == 2)
            {
                SceneManager.LoadScene("MenuTrophies");
            }
            if (index == 3)
            {
                PersistentManagerScript.client.Close();
                Application.Quit();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        { //Jeżeli naciśnięto klawisz "RightArrow"

            if (index < lPrzyciskow - 1)
            {
                index++;
                Vector2 position = transform.position;
                position.y += yOffset;
                transform.position = position;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
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
                SceneManager.LoadScene("MenuMedievalMissionChoice");
            }
            if (index == 1)
            {
                SceneManager.LoadScene("Training");
            }
            if (index == 2)
            {
                SceneManager.LoadScene("MenuTrophies");
            }
            if (index == 3)
            {
                PersistentManagerScript.client.Close();
                Application.Quit();
            }
        }
    }

}