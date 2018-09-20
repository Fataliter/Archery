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

    void Start()
    {
        yOffset = textMission2.transform.position.y - textMission1.transform.position.y;
        Cursor.visible = false;//Ukrycie pokazanie kursora myszy. 
    }

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
                position.y -= yOffset * 4f;
                transform.position = position;
            }
        }
        if (PersistentManagerScript.Instance.mydata.LeftButton == 0)
        { //Jeżeli naciśnięto klawisz "LeftButton"
            while (PersistentManagerScript.Instance.mydata.LeftButton == 0)
            { }
            if (index == 0)
                SceneManager.LoadScene("Mission1");
            if (index == 1) { }
            //SceneManager.LoadScene("Mission2");
            if (index == 2) { }
            //SceneManager.LoadScene("Mission3");
            if (index == 3) { }
                //SceneManager.LoadScene("Mission4");
            if (index == 4)
                SceneManager.LoadScene("MenuMedieval");
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
                SceneManager.LoadScene("Mission1");
            if (index == 1) { }
            //SceneManager.LoadScene("Mission2");
            if (index == 2) { }
            //SceneManager.LoadScene("Mission3");
            if (index == 3) { }
                //SceneManager.LoadScene("Mission4");
            if (index == 4)
                SceneManager.LoadScene("MenuMedieval");
        }
    }

}