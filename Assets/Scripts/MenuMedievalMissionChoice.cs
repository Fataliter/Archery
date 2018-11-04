using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuMedievalMissionChoice : MonoBehaviour
{
    int index = 0;
    public int lPrzyciskow = 5;
    float yOffset;
    public Text textMission1, textMission2;
    bool pressed = false;

    void Start()
    {
        yOffset = textMission2.transform.position.y - textMission1.transform.position.y;
        Cursor.visible = false;//Ukrycie pokazanie kursora myszy. 
    }

    void Update()
    {
        if (pressed == false)
        {
            if (PersistentManagerScript.Instance.mydata.RightButton == 0)
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
                    position.y -= yOffset * 4f;
                    transform.position = position;
                }
            }
            if (PersistentManagerScript.Instance.mydata.LeftButton == 0)
            { //Jeżeli naciśnięto klawisz "LeftButton"
                if (index == 0)
                    SceneManager.LoadScene("Mission1");
                if (index == 1 && PersistentManagerScript.Instance.medalsMenu.medals1 == 1)
                    SceneManager.LoadScene("Mission2");
                if (index == 2) { }
                    //SceneManager.LoadScene("Mission3");
                if (index == 3) { }
                    //SceneManager.LoadScene("Mission4");
                if (index == 4)
                    SceneManager.LoadScene("MenuMedieval");
            }
        }

        if (PersistentManagerScript.Instance.mydata.RightButton == 0 || PersistentManagerScript.Instance.mydata.LeftButton == 0)
            pressed = true;
        else
            pressed = false;

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
                SceneManager.LoadScene("Mission1");
            if (index == 1 && PersistentManagerScript.Instance.medalsMenu.medals1 == 1)
                SceneManager.LoadScene("Mission2");
            if (index == 2) { }
            //SceneManager.LoadScene("Mission3");
            if (index == 3) { }
                //SceneManager.LoadScene("Mission4");
            if (index == 4)
                SceneManager.LoadScene("MenuMedieval");
        }
    }

}