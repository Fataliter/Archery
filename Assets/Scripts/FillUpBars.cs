using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillUpBars : MonoBehaviour {
    Image mission1, mission2, mission3, mission4;
    float time1, time2, time3, time4;
    float[] target1, target2, target3, target4;
    Text targetMission1, timeMission1;

	void Start ()
    {
        GameObject mission1Object = GameObject.Find("Mission1Bar");
        GameObject mission2Object = GameObject.Find("Mission2Bar");
        GameObject mission3Object = GameObject.Find("Mission3Bar");
        GameObject mission4Object = GameObject.Find("Mission4Bar");
        GetData();
        if (mission1Object != null)
        {
            SaveManager.Instance.state.timePlayedMission1 = 395f;
            SaveManager.Instance.state.targetsMission1 = "486";

            mission1 = mission1Object.GetComponent<Image>();
            mission1.fillAmount = 0f;

            GameObject targetMission1Object = GameObject.Find("TargetsMission1");
            targetMission1 = targetMission1Object.GetComponent<Text>();
            GameObject timeMission1Object = GameObject.Find("TimeMission1");
            timeMission1 = timeMission1Object.GetComponent<Text>();

            float timePlayed = SaveManager.Instance.state.timePlayedMission1;
            string[] targets = SaveManager.Instance.state.targetsMission1.Split(',');

            targetMission1.text = targets[0] + "/" + target1[0].ToString();
            timeMission1.text = timePlayed.ToString() + "/" + time1.ToString();

            if (SaveManager.Instance.state.targetsMission1 != "")
            {
                float progress = CheckProgress(timePlayed, time1, targets, target1);
                mission1.fillAmount = progress;
            }
        }
        if (mission2Object != null)
        {
            SaveManager.Instance.state.timePlayedMission2 = 20f;
            SaveManager.Instance.state.targetsMission2 = "23,1";

            mission2 = mission2Object.GetComponent<Image>();
            mission2.fillAmount = 0f;

            float timePlayed = SaveManager.Instance.state.timePlayedMission2;
            string[] targets = SaveManager.Instance.state.targetsMission2.Split(',');

            if (SaveManager.Instance.state.targetsMission2 != "")
            {
                float progress = CheckProgress(timePlayed, time2, targets, target2);
                mission2.fillAmount = progress;
            }
        }
        if (mission3Object != null)
        {
            mission3 = mission3Object.GetComponent<Image>();
            mission3.fillAmount = 0f;

            float timePlayed = SaveManager.Instance.state.timePlayedMission3;
            string[] targets = SaveManager.Instance.state.targetsMission3.Split(',');

            if (SaveManager.Instance.state.targetsMission3 != "")
            {
                float progress = CheckProgress(timePlayed, time3, targets, target3);
                mission3.fillAmount = progress;
            }
        }
        if (mission4Object != null)
        {
            mission4 = mission4Object.GetComponent<Image>();
            mission4.fillAmount = 0f;

            float timePlayed = SaveManager.Instance.state.timePlayedMission4;
            string[] targets = SaveManager.Instance.state.targetsMission4.Split(',');

            if (SaveManager.Instance.state.targetsMission4 != "")
            {
                float progress = CheckProgress(timePlayed, time4, targets, target4);
                mission4.fillAmount = progress;
            }
        }
    }

    void GetData()
    {
        if (PersistentManagerScript.Instance.medalsMenu.trophy1 == 1)
            GetDataMission1("trophy");
        else if (PersistentManagerScript.Instance.medalsMenu.medalg1 == 1)
            GetDataMission1("gold");
        else if (PersistentManagerScript.Instance.medalsMenu.medals1 == 1)
            GetDataMission1("silver");
        else if (PersistentManagerScript.Instance.medalsMenu.medalb1 == 1 || PersistentManagerScript.Instance.medalsMenu.medalb1 == 0)
            GetDataMission1("bronze");

        if (PersistentManagerScript.Instance.medalsMenu.medals1 == 1)
        {
            if (PersistentManagerScript.Instance.medalsMenu.trophy2 == 1)
                GetDataMission2("trophy");
            else if (PersistentManagerScript.Instance.medalsMenu.medalg2 == 1)
                GetDataMission2("gold");
            else if (PersistentManagerScript.Instance.medalsMenu.medals2 == 1)
                GetDataMission2("silver");
            else if (PersistentManagerScript.Instance.medalsMenu.medalb2 == 1 || PersistentManagerScript.Instance.medalsMenu.medalb2 == 0)
                GetDataMission2("bronze");

            if (PersistentManagerScript.Instance.medalsMenu.medals2 == 1)
            {
                if (PersistentManagerScript.Instance.medalsMenu.trophy3 == 1)
                    GetDataMission3("trophy");
                else if (PersistentManagerScript.Instance.medalsMenu.medalg3 == 1)
                    GetDataMission3("gold");
                else if (PersistentManagerScript.Instance.medalsMenu.medals3 == 1)
                    GetDataMission3("silver");
                else if (PersistentManagerScript.Instance.medalsMenu.medalb3 == 1 || PersistentManagerScript.Instance.medalsMenu.medalb3 == 0)
                    GetDataMission3("bronze");

                if (PersistentManagerScript.Instance.medalsMenu.medals3 == 1)
                {
                    if (PersistentManagerScript.Instance.medalsMenu.trophy4 == 1)
                        GetDataMission4("trophy");
                    else if (PersistentManagerScript.Instance.medalsMenu.medalg4 == 1)
                        GetDataMission4("gold");
                    else if (PersistentManagerScript.Instance.medalsMenu.medals4 == 1)
                        GetDataMission4("silver");
                    else if (PersistentManagerScript.Instance.medalsMenu.medalb4 == 1 || PersistentManagerScript.Instance.medalsMenu.medalb4 == 0)
                        GetDataMission4("bronze");
                }
            }
        }
    }

    float CheckProgress(float time, float timeMax, string[] targetsStr, float[] targetMax)
    {
        float[] targets = new float[targetMax.Length];
        float played = 0;

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = float.Parse(targetsStr[i]);
            if (targets[i] > targetMax[i])
                targets[i] = targetMax[i];
            played += targets[i];
        }

        if (time > timeMax)
            time = timeMax;
        played += time;

        float max = 0;
        for (int i = 0; i < targetMax.Length; i++)
            max += targetMax[i];
        max += timeMax;

        return played / max;
    }

    void GetDataMission1(string achiv)
    {
        string[] mission1 = (PersistentManagerScript.Instance.config["mission1"][achiv].StringValue).Split(',');
        time1 = float.Parse(mission1[0]);
        target1 = new float[mission1.Length - 1];
        for (int i = 0; i < mission1.Length - 1; i++)
            target1[i] = float.Parse(mission1[i + 1]);
    }

    void GetDataMission2(string achiv)
    {
        string[] mission2 = (PersistentManagerScript.Instance.config["mission2"][achiv].StringValue).Split(',');
        time2 = float.Parse(mission2[0]);
        target2 = new float[mission2.Length - 1];
        for (int i = 0; i < mission2.Length - 1; i++)
            target2[i] = float.Parse(mission2[i + 1]);
    }

    void GetDataMission3(string achiv)
    {
        string[] mission3 = (PersistentManagerScript.Instance.config["mission3"][achiv].StringValue).Split(',');
        time3 = float.Parse(mission3[0]);
        target3 = new float[mission3.Length - 1];
        for (int i = 0; i < mission3.Length - 1; i++)
            target3[i] = float.Parse(mission3[i + 1]);
    }

    void GetDataMission4(string achiv)
    {
        string[] mission4 = (PersistentManagerScript.Instance.config["mission4"][achiv].StringValue).Split(',');
        time4 = float.Parse(mission4[0]);
        target4 = new float[mission4.Length - 1];
        for (int i = 0; i < mission4.Length - 1; i++)
            target4[i] = float.Parse(mission4[i + 1]);
    }
}