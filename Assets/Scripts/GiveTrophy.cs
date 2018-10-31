using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GiveTrophy : MonoBehaviour {

    public Sprite bronze, silver, gold, trophy;

    string activeScene = "";
    string[] achievmentName = { "bronze", "silver", "gold", "trophy" };
    public SaveMedals saveMedals;

    float scoredTime = 0f;
    int[] scoredTargets;

    int timeToScore = 0;
    int[] targetsToScore;

    void Start () {
        activeScene = SceneManager.GetActiveScene().name;
        saveMedals = new SaveMedals();
    }
	
	void Update () {
        if (MissionManager.endOfMission == true)
        {
            scoredTime = MissionManager.timeAlreadyPlayed;
            scoredTargets = MissionManager.targetsOnEnd;
            ScoredAchievment();
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
}
