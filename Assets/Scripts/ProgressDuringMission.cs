using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressDuringMission : MonoBehaviour {
    public Sprite target, bandit, goblin, dragon;

    Image image1, image2, image3;
    Text text1, text2, text3;
    Image time;

    string activeScene;
    public static bool hit = false;
    public static string targetName = "";

	void Start () {
        activeScene = SceneManager.GetActiveScene().name;

        GameObject timeObject = GameObject.Find("TimeLeft");
        time = timeObject.GetComponent<Image>();

        GameObject image1Object = GameObject.Find("Image1");
        image1 = image1Object.GetComponent<Image>();
        GameObject image2Object = GameObject.Find("Image2");
        image2 = image2Object.GetComponent<Image>();
        GameObject image3Object = GameObject.Find("Image3");
        image3 = image3Object.GetComponent<Image>();

        GameObject text1Object = GameObject.Find("Text1");
        text1 = text1Object.GetComponent<Text>();
        GameObject text2Object = GameObject.Find("Text2");
        text2 = text2Object.GetComponent<Text>();
        GameObject text3Object = GameObject.Find("Text3");
        text3 = text3Object.GetComponent<Text>();

        SetImages();
    }
	
	void Update () {
        time.fillAmount = (MissionManager.maxTime - MissionManager.timePlayed) / MissionManager.maxTime;
        if (hit)
        {
            ChangeValue(targetName);
        }
	}

    void ChangeValue(string targetName)
    {
        if (targetName == "Target")
            text1.text = (int.Parse(text1.text) + 1).ToString();
        if (targetName == "Enemy1")
            text2.text = (int.Parse(text2.text) + 1).ToString();
        if (targetName == "Enemy2")
            text3.text = (int.Parse(text3.text) + 1).ToString();
        if (targetName == "Enemy3")
            text2.text = (int.Parse(text2.text) + 1).ToString();
        hit = false;
    }

    void SetImages()
    {
        if (activeScene == "Mission1")
        {
            image1.sprite = target;
            image1.color = new Color(1f, 1f, 1f, 1f);
            text1.text = "0";
        }
        else if (activeScene == "Mission2")
        {
            image1.sprite = target;
            image1.color = new Color(1f, 1f, 1f, 1f);
            text1.text = "0";
            image2.sprite = bandit;
            image2.color = new Color(1f, 1f, 1f, 1f);
            text2.text = "0";
        }
        else if (activeScene == "Mission3")
        {
            image1.sprite = target;
            image1.color = new Color(1f, 1f, 1f, 1f);
            text1.text = "0";
            image2.sprite = bandit;
            image2.color = new Color(1f, 1f, 1f, 1f);
            text2.text = "0";
            image3.sprite = goblin;
            image3.color = new Color(1f, 1f, 1f, 1f);
            text3.text = "0";
        }
        else if (activeScene == "Mission4")
        {
            image1.sprite = target;
            image1.color = new Color(1f, 1f, 1f, 1f);
            text1.text = "0";
            image2.sprite = dragon;
            image2.color = new Color(1f, 1f, 1f, 1f);
            text2.text = "0";
            image3.sprite = goblin;
            image3.color = new Color(1f, 1f, 1f, 1f);
            text3.text = "0";
        }
        else if (activeScene == "Training")
        {
            image1.sprite = target;
            image1.color = new Color(1f, 1f, 1f, 1f);
            text1.text = "0";
        }
    }
}
