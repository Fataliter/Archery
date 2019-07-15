using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillUpBars : MonoBehaviour {
    public Sprite bronze, silver, gold, trophy, win;

    Image mission1, mission2, mission3, mission4;
    float time1, time2, time3, time4;
    float[] target1, target2, target3, target4;
    Text dragon, goblin, bandit, target, time;
    Image achiev1, achiev2, achiev3, achiev4;

	void Start ()
    {
        GameObject mission1Object = GameObject.Find("Mission1Bar");
        GameObject mission2Object = GameObject.Find("Mission2Bar");
        GameObject mission3Object = GameObject.Find("Mission3Bar");
        GameObject mission4Object = GameObject.Find("Mission4Bar");
        GetData();
        if (mission1Object != null)
        {
            mission1 = mission1Object.GetComponent<Image>();
            mission1.fillAmount = 0f;

            GameObject targetObject = GameObject.Find("TargetsMission1");
            target = targetObject.GetComponent<Text>();
            GameObject timeObject = GameObject.Find("TimeMission1");
            time = timeObject.GetComponent<Text>();

            float timePlayed = SaveManager.Instance.state.timePlayedMission1;
            string[] targets = SaveManager.Instance.state.targetsMission1.Split(',');

            float timeFloat = Convert.ToSingle(Math.Floor(Convert.ToDouble(timePlayed / 60f)));

            target.text = targets[0] + "/" + target1[0].ToString();
            time.text = timeFloat.ToString() + "/" + time1.ToString();

            if(timePlayed!=0)
            {
                float progress = CheckProgress(timeFloat, time1, targets, target1);
                mission1.fillAmount = progress;
            }
            
        }
        if (mission2Object != null)
        {
            mission2 = mission2Object.GetComponent<Image>();
            mission2.fillAmount = 0f;

            GameObject targetObject = GameObject.Find("TargetsMission2");
            target = targetObject.GetComponent<Text>();
            GameObject timeObject = GameObject.Find("TimeMission2");
            time = timeObject.GetComponent<Text>();
            GameObject banditObject = GameObject.Find("BanditMission2");
            bandit = banditObject.GetComponent<Text>();

            float timePlayed = SaveManager.Instance.state.timePlayedMission2;
            string[] targets = SaveManager.Instance.state.targetsMission2.Split(',');

            float timeFloat = Convert.ToSingle(Math.Floor(Convert.ToDouble(timePlayed / 60f)));

            target.text = targets[0] + "/" + target2[0].ToString();
            time.text = timeFloat.ToString() + "/" + time2.ToString();
            bandit.text = targets[1] + "/" + target2[1].ToString();
           
            if(timePlayed!=0)
            {
                float progress = CheckProgress(timeFloat, time2, targets, target2);
                mission2.fillAmount = progress;
            }
        }
        if (mission3Object != null)
        {
            mission3 = mission3Object.GetComponent<Image>();
            mission3.fillAmount = 0f;

            GameObject targetObject = GameObject.Find("TargetsMission3");
            target = targetObject.GetComponent<Text>();
            GameObject timeObject = GameObject.Find("TimeMission3");
            time = timeObject.GetComponent<Text>();
            GameObject banditObject = GameObject.Find("BanditMission3");
            bandit = banditObject.GetComponent<Text>();
            GameObject goblinObject = GameObject.Find("GoblinMission3");
            goblin = goblinObject.GetComponent<Text>();

            float timePlayed = SaveManager.Instance.state.timePlayedMission3;
            string[] targets = SaveManager.Instance.state.targetsMission3.Split(',');

            float timeFloat = Convert.ToSingle(Math.Floor(Convert.ToDouble(timePlayed / 60f)));
            
            target.text = targets[0] + "/" + target3[0].ToString();
            time.text = timeFloat.ToString() + "/" + time3.ToString();
            bandit.text = targets[1] + "/" + target3[1].ToString();
            goblin.text = targets[2] + "/" + target3[2].ToString();
          
            if(timePlayed!=0)
            {
                float progress = CheckProgress(timeFloat, time3, targets, target3);
                mission3.fillAmount = progress;
            }
        }
        if (mission4Object != null)
        {
            mission4 = mission4Object.GetComponent<Image>();
            mission4.fillAmount = 0f;

            GameObject targetObject = GameObject.Find("TargetsMission4");
            target = targetObject.GetComponent<Text>();
            GameObject timeObject = GameObject.Find("TimeMission4");
            time = timeObject.GetComponent<Text>();
            GameObject goblinObject = GameObject.Find("GoblinMission4");
            goblin = goblinObject.GetComponent<Text>();
            GameObject dragonObject = GameObject.Find("DragonMission4");
            dragon = dragonObject.GetComponent<Text>();

            float timePlayed = SaveManager.Instance.state.timePlayedMission4;
            string[] targets = SaveManager.Instance.state.targetsMission4.Split(',');

            float timeFloat = Convert.ToSingle(Math.Floor(Convert.ToDouble(timePlayed / 60f)));

            target.text = targets[0] + "/" + target4[0].ToString();
            time.text = timeFloat.ToString() + "/" + time4.ToString();
            goblin.text = targets[1] + "/" + target4[1].ToString();
            dragon.text = targets[2] + "/" + target4[2].ToString();
           
            if(timePlayed!=0)
            {
                float progress = CheckProgress(timeFloat, time4, targets, target4);
                mission4.fillAmount = progress;
            }
            
        }
    }

    void GetData()
    {
        GameObject achievObject = GameObject.Find("AchievImageMission1");
        achiev1 = achievObject.GetComponent<Image>();
        achievObject = GameObject.Find("AchievImageMission2");
        achiev2 = achievObject.GetComponent<Image>();
        achievObject = GameObject.Find("AchievImageMission3");
        achiev3 = achievObject.GetComponent<Image>();
        achievObject = GameObject.Find("AchievImageMission4");
        achiev4 = achievObject.GetComponent<Image>();
        NextMedalMission1();
        NextMedalMission2();
        NextMedalMission3();
        NextMedalMission4();


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

    void NextMedalMission1()
    {
        if (PersistentManagerScript.Instance.medalsMenu.trophy1 == 1)
        {
            GetDataMission1("trophy");
            achiev1.sprite = win;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medalg1 == 1)
        {
            GetDataMission1("trophy");
            achiev1.sprite = trophy;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medals1 == 1)
        {
            GetDataMission1("gold");
            achiev1.sprite = gold;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medalb1 == 1)
        {
            GetDataMission1("silver");
            achiev1.sprite = silver;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medalb1 == 0)
        {
            GetDataMission1("bronze");
            achiev1.sprite = bronze;
        }
    }

    void NextMedalMission2()
    {
        if (PersistentManagerScript.Instance.medalsMenu.trophy2 == 1)
        {
            GetDataMission2("trophy");
            achiev2.sprite = win;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medalg2 == 1)
        {
            GetDataMission2("trophy");
            achiev2.sprite = trophy;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medals2 == 1)
        {
            GetDataMission2("gold");
            achiev2.sprite = gold;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medalb2 == 1)
        {
            GetDataMission2("silver");
            achiev2.sprite = silver;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medalb2 == 0)
        {
            GetDataMission2("bronze");
            achiev2.sprite = bronze;
        }
    }

    void NextMedalMission3()
    {
        if (PersistentManagerScript.Instance.medalsMenu.trophy3 == 1)
        {
            GetDataMission3("trophy");
            achiev3.sprite = win;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medalg3 == 1)
        {
            GetDataMission3("trophy");
            achiev3.sprite = trophy;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medals3 == 1)
        {
            GetDataMission3("gold");
            achiev3.sprite = gold;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medalb3 == 1)
        {
            GetDataMission3("silver");
            achiev3.sprite = silver;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medalb3 == 0)
        {
            GetDataMission3("bronze");
            achiev3.sprite = bronze;
        }
    }

    void NextMedalMission4()
    {
        if (PersistentManagerScript.Instance.medalsMenu.trophy4 == 1)
        {
            GetDataMission4("trophy");
            achiev4.sprite = win;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medalg4 == 1)
        {
            GetDataMission4("trophy");
            achiev4.sprite = trophy;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medals4 == 1)
        {
            GetDataMission4("gold");
            achiev4.sprite = gold;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medalb4 == 1)
        {
            GetDataMission4("silver");
            achiev4.sprite = silver;
        }
        else if (PersistentManagerScript.Instance.medalsMenu.medalb4 == 0)
        {
            GetDataMission4("bronze");
            achiev4.sprite = bronze;
        }
    }
}