using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{

    int index = 0;
    int lPrzyciskow = 3;
    public Text[] TextBox = new Text[3];
    float yOffset;
    float spinSpeed;
    int legsDiff;
    bool onetime;
    public GameObject info;
    private Text textValue, limitsValues, optionInfo;
    private Animator leftArrow, rightArrow;
    public GameObject infoImage;
    private bool infoImageAccepted;


    void Start()
    {
        infoImage.SetActive(true);
        infoImageAccepted = false;
        onetime = true;
        yOffset = TextBox[1].transform.position.y - TextBox[0].transform.position.y;
        Cursor.visible = false;//Ukrycie pokazanie kursora myszy.
        //textValue = info.GetComponentInChildren
        textValue = GameObject.Find("OptionValue/TextValue").GetComponent<Text>();
        limitsValues = GameObject.Find("OptionValue/LimitsValues").GetComponent<Text>();
        optionInfo = GameObject.Find("OptionValue/OptionInfo").GetComponent<Text>();
        leftArrow = GameObject.Find("OptionValue/LeftArrow").GetComponent<Animator>();
        rightArrow = GameObject.Find("OptionValue/RightArrow").GetComponent<Animator>();
        spinSpeed = PersistentManagerScript.Instance.config["general"]["rotationSensitivity"].FloatValue * 2;
        legsDiff = PersistentManagerScript.Instance.config["general"]["legsDifferenceForRotation"].IntValue;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            infoImageAccepted = true;
            infoImage.SetActive(false);
            
        }
        KeyboardSteer();
    }
    
    void KeyboardSteer()
    {
        if(infoImageAccepted)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                if (index < lPrzyciskow - 1)
                {
                    index++; onetime = true;
                    yOffset = TextBox[index].transform.position.y - TextBox[index - 1].transform.position.y;
                    Vector2 position = transform.position;
                    position.y += yOffset;
                    transform.position = position;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                if (index > 0)
                {
                    index--; onetime = true;
                    yOffset = TextBox[index + 1].transform.position.y - TextBox[index].transform.position.y;
                    Vector2 position = transform.position;
                    position.y -= yOffset;
                    transform.position = position;
                }
            }


            if (index == 0)
            {
                if (onetime)
                {
                    limitsValues.text = "0.36\n0.1\n1";
                    optionInfo.text = "Parametr opisuje szybkosc obrotu postaci. Im wieksza wartosc, tym szybciej postac bedzie sie obracac dla takiego samego nacisku na platformy.";
                    info.SetActive(true);
                    onetime = false;
                }
                spinSpeed = (float)Math.Round((double)spinSpeed, 2);
                spinSpeed = Mathf.Clamp(spinSpeed, 0.1f, 1);
                textValue.text = spinSpeed.ToString();
                if (Input.GetKeyDown(KeyCode.LeftArrow)) { spinSpeed -= 0.02f; leftArrow.SetBool("play", true); }
                else if (Input.GetKeyDown(KeyCode.RightArrow)) { spinSpeed += 0.02f; rightArrow.SetBool("play", true); }
            }
            if (index == 1)
            {
                if (onetime)
                {
                    limitsValues.text = "30\n2\n80";
                    optionInfo.text = "Parametr opisuje minimalna roznice naciskow na platformy, przy ktorej postac zacznie sie obracac. Im wieksza wartosc tym wieksza roznica naciskow powoduje obrot.";
                    info.SetActive(true);
                    onetime = false;
                }
                legsDiff = Mathf.Clamp(legsDiff, 2, 80);
                textValue.text = legsDiff.ToString();
                if (Input.GetKeyDown(KeyCode.LeftArrow)) { legsDiff -= 2; leftArrow.SetBool("play", true); }
                else if (Input.GetKeyDown(KeyCode.RightArrow)) { legsDiff += 2; rightArrow.SetBool("play", true); }
            }
            if (index == 2)
            {
                info.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    PersistentManagerScript.Instance.config["general"]["rotationSensitivity"].FloatValue = (float)Math.Round((double)spinSpeed / 2, 2);
                    PersistentManagerScript.Instance.config["general"]["legsDifferenceForRotation"].IntValue = legsDiff;
                    PersistentManagerScript.Instance.config.SaveToFile(PersistentManagerScript.Instance.gameFolder + "/config.cfg");
                    SceneManager.LoadScene("MenuMedieval");
                }

            }
        }
        
    }

}
