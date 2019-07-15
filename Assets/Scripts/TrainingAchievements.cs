using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingAchievements : MonoBehaviour {
    Image training;
    float time, targets;
    Text timeText, targetsText;

    void Start() {
        GameObject trainingObject = GameObject.Find("TrainingBar");
        GetData();
        if (trainingObject != null)
        {
            training = trainingObject.GetComponent<Image>();
            training.fillAmount = 0f;

            GameObject targetObject = GameObject.Find("TargetsTraining");
            targetsText = targetObject.GetComponent<Text>();
            GameObject timeObject = GameObject.Find("TimeTraining");
            timeText = timeObject.GetComponent<Text>();

            float timePlayed = SaveManager.Instance.state.timePlayedTraining;
            string targetsScored = SaveManager.Instance.state.targetsTraining;

            float timeFloat = Convert.ToSingle(Math.Floor(Convert.ToDouble(timePlayed / 60f)));

            targetsText.text = targetsScored + "/" + targets.ToString();
            timeText.text = timeFloat.ToString() + "/" + time.ToString();

            if (timePlayed != 0)
            {
                float progress = CheckProgress(timeFloat, time, targetsScored, targets);
                training.fillAmount = progress;
            }
            
        }
    }

    void GetData()
    {
        if (PersistentManagerScript.Instance.medalsMenu.training4 == 1)
            GetDataTraining("trophy");
        else if (PersistentManagerScript.Instance.medalsMenu.training3 == 1)
            GetDataTraining("trophy");
        else if (PersistentManagerScript.Instance.medalsMenu.training2 == 1)
            GetDataTraining("gold");
        else if (PersistentManagerScript.Instance.medalsMenu.training1 == 1)
            GetDataTraining("silver");
        else if (PersistentManagerScript.Instance.medalsMenu.training1 == 0)
            GetDataTraining("bronze");
    }

    void GetDataTraining(string achiv)
    {
        string[] training = (PersistentManagerScript.Instance.config["training"][achiv].StringValue).Split(',');
        time = float.Parse(training[0]);
        targets = float.Parse(training[1]);
    }

    float CheckProgress(float timePlayed, float time, string targetsScored, float targets)
    {
        float targetsScoredFloat = float.Parse(targetsScored);
        float played = 0;

        if (timePlayed > time)
            timePlayed = time;
        played += timePlayed;

        if (targetsScoredFloat > targets)
            targetsScoredFloat = targets;
        played += targetsScoredFloat;

        float max = 0;
        max = time + targets;

        return played / max;
    }
}
