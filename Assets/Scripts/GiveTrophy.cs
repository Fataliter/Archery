using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GiveTrophy : MonoBehaviour {

    public Sprite[] trophies = new Sprite[4];
    Canvas missionEnd, arrows;
    Text medalText;
    Image medal;

    int[] trophyOnStart = new int[4];

    int[] trophyOnEnd = new int[4];

    string activeScene = "";
    string[] achievmentName = { "bronze", "silver", "gold", "trophy" };
    public SaveMedals saveMedals;

    float scoredTime = 0f;
    int[] scoredTargets;

    int timeToScore = 0;
    int[] targetsToScore;

    public static bool gave;

    bool oneTime;

    void Start () {
        oneTime = true;
        gave = false;
        activeScene = SceneManager.GetActiveScene().name;
        MedalsOnStart();
        saveMedals = new SaveMedals();

        GameObject missionEndObject = GameObject.Find("EndOfMission");
        missionEnd = missionEndObject.GetComponent<Canvas>();
        missionEnd.enabled = false;
    }
	
	void Update () {
        if (MissionManager.endOfMission == true && MissionManager.keyPressed == false)
        {
            scoredTime = MissionManager.timeAlreadyPlayed;
            scoredTargets = MissionManager.targetsOnEnd;
            if (oneTime)
            {
                ScoredAchievment();
                oneTime = false;
            }
            gave = true;
            MedalsOnEnd();
            MissionManager.endOfMission = false;
        }
	}

    void ScoredAchievment(int iterator=0)
    {
        int countTargets = 0;
        string[] mission = (PersistentManagerScript.Instance.config[activeScene.ToLower()][achievmentName[iterator]].StringValue).Split(',');
        timeToScore = int.Parse(mission[0])*60;
        targetsToScore = new int[mission.Length - 1];
        for (int i = 0; i < mission.Length - 1; i++)
        {
            targetsToScore[i] = int.Parse(mission[i + 1]);
        }
        for (int i = 0; i < targetsToScore.Length; i++)
        {
            if(scoredTargets[i] >= targetsToScore[i])
                countTargets++;
        }
        if(countTargets==targetsToScore.Length && scoredTime >= timeToScore && iterator < achievmentName.Length)
        {
            GiveMedals(iterator);
            saveMedals.Save();
            iterator++;
            ScoredAchievment(iterator);
        }
    }

    void GiveMedals(int k)
    {
        if (activeScene == "Training")
        {
            if (k >= 0) PersistentManagerScript.Instance.medalsMenu.training1 = 1;
            if (k >= 1) PersistentManagerScript.Instance.medalsMenu.training2 = 1;
            if (k >= 2) PersistentManagerScript.Instance.medalsMenu.training3 = 1;
            if (k >= 3) PersistentManagerScript.Instance.medalsMenu.training4 = 1;
        }
        if (activeScene == "Mission1")
        {
            if (k >= 0) PersistentManagerScript.Instance.medalsMenu.medalb1 = 1;
            if (k >= 1) PersistentManagerScript.Instance.medalsMenu.medals1 = 1;
            if (k >= 2) PersistentManagerScript.Instance.medalsMenu.medalg1 = 1;
            if (k >= 3) PersistentManagerScript.Instance.medalsMenu.trophy1 = 1;
        }
        if (activeScene == "Mission2")
        {
            if (k >= 0) PersistentManagerScript.Instance.medalsMenu.medalb2 = 1;
            if (k >= 1) PersistentManagerScript.Instance.medalsMenu.medals2 = 1;
            if (k >= 2) PersistentManagerScript.Instance.medalsMenu.medalg2 = 1;
            if (k >= 3) PersistentManagerScript.Instance.medalsMenu.trophy2 = 1;
        }
        if (activeScene == "Mission3")
        {
            if (k >= 0) PersistentManagerScript.Instance.medalsMenu.medalb3 = 1;
            if (k >= 1) PersistentManagerScript.Instance.medalsMenu.medals3 = 1;
            if (k >= 2) PersistentManagerScript.Instance.medalsMenu.medalg3 = 1;
            if (k >= 3) PersistentManagerScript.Instance.medalsMenu.trophy3 = 1;
        }
        if (activeScene == "Mission4")
        {
            if (k >= 0) PersistentManagerScript.Instance.medalsMenu.medalb4 = 1;
            if (k >= 1) PersistentManagerScript.Instance.medalsMenu.medals4 = 1;
            if (k >= 2) PersistentManagerScript.Instance.medalsMenu.medalg4 = 1;
            if (k >= 3) PersistentManagerScript.Instance.medalsMenu.trophy4 = 1;
        }
    }

    void MedalsOnStart()
    {
        if (activeScene == "Mission1")
        {
            trophyOnStart[0] = PersistentManagerScript.Instance.medalsMenu.medalb1;
            trophyOnStart[1] = PersistentManagerScript.Instance.medalsMenu.medals1;
            trophyOnStart[2] = PersistentManagerScript.Instance.medalsMenu.medalg1;
            trophyOnStart[3] = PersistentManagerScript.Instance.medalsMenu.trophy1;
        }
        if (activeScene == "Mission2")
        {
            trophyOnStart[0] = PersistentManagerScript.Instance.medalsMenu.medalb2;
            trophyOnStart[1] = PersistentManagerScript.Instance.medalsMenu.medals2;
            trophyOnStart[2] = PersistentManagerScript.Instance.medalsMenu.medalg2;
            trophyOnStart[3] = PersistentManagerScript.Instance.medalsMenu.trophy2;
        }
        if (activeScene == "Mission3")
        {
            trophyOnStart[0] = PersistentManagerScript.Instance.medalsMenu.medalb3;
            trophyOnStart[1] = PersistentManagerScript.Instance.medalsMenu.medals3;
            trophyOnStart[2] = PersistentManagerScript.Instance.medalsMenu.medalg3;
            trophyOnStart[3] = PersistentManagerScript.Instance.medalsMenu.trophy3;
        }
        if (activeScene == "Mission4")
        {
            trophyOnStart[0] = PersistentManagerScript.Instance.medalsMenu.medalb4;
            trophyOnStart[1] = PersistentManagerScript.Instance.medalsMenu.medals4;
            trophyOnStart[2] = PersistentManagerScript.Instance.medalsMenu.medalg4;
            trophyOnStart[3] = PersistentManagerScript.Instance.medalsMenu.trophy4;
        }
    }

    void MedalsOnEnd()
    {
        if (gave)
        {
            GameObject arrowsObject = GameObject.Find("PowerAndArrows");
            arrows = arrowsObject.GetComponent<Canvas>();
            arrows.enabled = false;

            missionEnd.enabled = true;

            GameObject medalTextObject = GameObject.Find("MedalText");
            medalText = medalTextObject.GetComponent<Text>();
            medalText.enabled = false;

            GameObject medalObject = GameObject.Find("Medal");
            medal = medalObject.GetComponent<Image>();
            medal.enabled = false;

            if (activeScene == "Mission1")
            {
                trophyOnEnd[0] = PersistentManagerScript.Instance.medalsMenu.medalb1;
                trophyOnEnd[1] = PersistentManagerScript.Instance.medalsMenu.medals1;
                trophyOnEnd[2] = PersistentManagerScript.Instance.medalsMenu.medalg1;
                trophyOnEnd[3] = PersistentManagerScript.Instance.medalsMenu.trophy1;
            }
            if (activeScene == "Mission2")
            {
                trophyOnEnd[0] = PersistentManagerScript.Instance.medalsMenu.medalb2;
                trophyOnEnd[1] = PersistentManagerScript.Instance.medalsMenu.medals2;
                trophyOnEnd[2] = PersistentManagerScript.Instance.medalsMenu.medalg2;
                trophyOnEnd[3] = PersistentManagerScript.Instance.medalsMenu.trophy2;
            }
            if (activeScene == "Mission3")
            {
                trophyOnEnd[0] = PersistentManagerScript.Instance.medalsMenu.medalb3;
                trophyOnEnd[1] = PersistentManagerScript.Instance.medalsMenu.medals3;
                trophyOnEnd[2] = PersistentManagerScript.Instance.medalsMenu.medalg3;
                trophyOnEnd[3] = PersistentManagerScript.Instance.medalsMenu.trophy3;
            }
            if (activeScene == "Mission4")
            {
                trophyOnEnd[0] = PersistentManagerScript.Instance.medalsMenu.medalb4;
                trophyOnEnd[1] = PersistentManagerScript.Instance.medalsMenu.medals4;
                trophyOnEnd[2] = PersistentManagerScript.Instance.medalsMenu.medalg4;
                trophyOnEnd[3] = PersistentManagerScript.Instance.medalsMenu.trophy4;
            }
            for (int i = 0; i < 4; i++)
            {
                if (trophyOnStart[i] != trophyOnEnd[i])
                {
                    medalText.enabled = true;
                    medal.enabled = true;
                    medal.sprite = trophies[i];
                }
            }
        }
    }
}
