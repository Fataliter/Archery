using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour {

    public GameObject EndMissionParticles;

    public static int shootTargetCount = 0;
    public static int enemy1Count = 0;
    public static int enemy2Count = 0;
    public static int enemy3Count = 0;
    int pillowsPressLevel = 0;
    float[] pillowsPressValues = new float[4];
    public static float pillowPress;
    public static float pillowsLegsDiff = 0f;
    
    public static int hitCounter=0;

    byte leftButton;

    string activeScene = string.Empty;

    bool keyPressed = false;

    public static float maxTime;
    public static float timePlayed;
    float keypressTime = 0f;
    public static float timeAlreadyPlayed;
    public static int[] targetsOnEnd;
    public static bool endOfMission;
    bool onetime;
    public static bool hit = false;
    public static bool fireworks;

    float time1, time2, time3;

    void Awake () {
        time1 = time2 = time3 = 0f;
        string pillowsPressFromCfg = PersistentManagerScript.Instance.config["general"]["pillowsLevels"].StringValue;
        pillowsPressValues = pillowsPressFromCfg.Split(',').Select(float.Parse).ToArray();
        for(int i=0; i<4; i++) { Debug.Log(pillowsPressValues[i]); }
        fireworks = false;
        timePlayed = 0f;
        endOfMission = false;
        keyPressed = false;
        activeScene = SceneManager.GetActiveScene().name;
        pillowsLegsDiff = PersistentManagerScript.Instance.config["general"]["legsDifferenceForRotation"].FloatValue;
        timeAlreadyPlayed = GetDataFromSaveState();
        pillowPress = pillowsPressValues[pillowsPressLevel];
        onetime = true;
        hit = false;
    }

    private void Update() {
        parameters();
        timePlayed += Time.deltaTime;
        PillowsTimePress();
        EndMission();
    }

    void parameters()
    {
        leftButton = (byte)PersistentManagerScript.Instance.mydata.LeftButton;
    }

    float GetDataFromSaveState() {
        string savedData=string.Empty;
        float missionTime = 0f;
        int[] data;
        if(activeScene == "Training")
        {
            maxTime = Mathf.Infinity;
            pillowsPressLevel = SaveManager.Instance.state.pillowsLevelTraining;
            savedData = SaveManager.Instance.state.targetsTraining;
            missionTime = SaveManager.Instance.state.timePlayedTraining;
            shootTargetCount = int.Parse(savedData);
        }
        if (activeScene == "Mission1")
        {
            maxTime = PersistentManagerScript.Instance.config["mission1"]["missionTime"].FloatValue;
            pillowsPressLevel = SaveManager.Instance.state.pillowsLevelMission1;
            savedData = SaveManager.Instance.state.targetsMission1;
            missionTime = SaveManager.Instance.state.timePlayedMission1;
            shootTargetCount = int.Parse(savedData);
        }
        if (activeScene == "Mission2")
        {
            maxTime = PersistentManagerScript.Instance.config["mission2"]["missionTime"].FloatValue;
            pillowsPressLevel = SaveManager.Instance.state.pillowsLevelMission2;
            savedData = SaveManager.Instance.state.targetsMission2;
            missionTime = SaveManager.Instance.state.timePlayedMission2;
            data = savedData.Split(',').Select(int.Parse).ToArray();
            shootTargetCount = data[0];
            enemy1Count = data[1];
        }
        if (activeScene == "Mission3")
        {
            maxTime = PersistentManagerScript.Instance.config["mission3"]["missionTime"].FloatValue;
            pillowsPressLevel = SaveManager.Instance.state.pillowsLevelMission3;
            savedData = SaveManager.Instance.state.targetsMission3;
            missionTime = SaveManager.Instance.state.timePlayedMission3;
            data = savedData.Split(',').Select(int.Parse).ToArray();
            shootTargetCount = data[0];
            enemy1Count = data[1];
            enemy2Count = data[2];
        }
        if (activeScene == "Mission4")
        {
            maxTime = PersistentManagerScript.Instance.config["mission4"]["missionTime"].FloatValue;
            pillowsPressLevel = SaveManager.Instance.state.pillowsLevelMission4;
            savedData = SaveManager.Instance.state.targetsMission4;
            missionTime = SaveManager.Instance.state.timePlayedMission4;
            data = savedData.Split(',').Select(int.Parse).ToArray();
            shootTargetCount = data[0];
            enemy2Count = data[1];
            enemy3Count = data[2];
        }
        
        return missionTime;
    }
	
	void SaveTargetsAndTime(int T, int E1, int E2, int E3) {
        if (activeScene == "Training")
        {
            SaveManager.Instance.state.timePlayedTraining = timeAlreadyPlayed;     
            SaveManager.Instance.state.targetsTraining = T.ToString();
            SaveManager.Instance.state.pillowsLevelTraining = 1;
            targetsOnEnd = new int[1];
            targetsOnEnd[0] = T;
        }   
        if (activeScene == "Mission1")
        {
            SaveManager.Instance.state.timePlayedMission1 = timeAlreadyPlayed;     
            SaveManager.Instance.state.targetsMission1 = T.ToString();
            SaveManager.Instance.state.pillowsLevelMission1 = pillowsPressLevel;
            targetsOnEnd = new int[1];
            targetsOnEnd[0] = T;
        } 
        if (activeScene == "Mission2")
        {
            SaveManager.Instance.state.timePlayedMission2 = timeAlreadyPlayed;  
            SaveManager.Instance.state.targetsMission2 = T.ToString() + "," + E1.ToString();
            SaveManager.Instance.state.pillowsLevelMission2 = pillowsPressLevel;
            targetsOnEnd = new int[2];
            targetsOnEnd[0] = T;
            targetsOnEnd[1] = E1;
        }
        if (activeScene == "Mission3")
        {
            SaveManager.Instance.state.timePlayedMission3 = timeAlreadyPlayed;  
            SaveManager.Instance.state.targetsMission3 = T.ToString() + "," + E1.ToString() + "," + E2.ToString();
            SaveManager.Instance.state.pillowsLevelMission3 = pillowsPressLevel;
            targetsOnEnd = new int[3];
            targetsOnEnd[0] = T;
            targetsOnEnd[1] = E1;
            targetsOnEnd[2] = E2;
        }
        if (activeScene == "Mission4")
        {
            SaveManager.Instance.state.timePlayedMission4 = timeAlreadyPlayed;  
            SaveManager.Instance.state.targetsMission4 = T.ToString() + "," + E2.ToString() + "," + E3.ToString();
            SaveManager.Instance.state.pillowsLevelMission4 = pillowsPressLevel;
            targetsOnEnd = new int[3];
            targetsOnEnd[0] = T;
            targetsOnEnd[1] = E2;
            targetsOnEnd[2] = E3;
        }
    }


    void EndMission()
    {
        
        if (Input.GetKey(KeyCode.Escape) || leftButton == 0) keypressTime += Time.deltaTime;
        else keypressTime = 0f;
        if (timePlayed >= maxTime)
        {
            if (onetime)
            {
                endOfMission = true;
                timeAlreadyPlayed += maxTime;
                SaveTargetsAndTime(shootTargetCount, enemy1Count, enemy2Count, enemy3Count);
                SaveManager.Instance.Save();
                onetime = false;
                GameObject.Instantiate(EndMissionParticles);
                fireworks = true;
            }
        }
        if (keypressTime > 3 && keyPressed == false)
        {
            endOfMission = true;
            keyPressed = true;
            timeAlreadyPlayed += timePlayed;
            SaveTargetsAndTime(shootTargetCount, enemy1Count, enemy2Count, enemy3Count);
            SaveManager.Instance.Save();
            SceneManager.LoadScene("MenuMedieval");
        }
    }
    

    void PillowsTimePress()
    {
        if (PillowsCanvas.pillowsPressed == 3) time3 += Time.deltaTime;
        if (PillowsCanvas.pillowsPressed == 2) time2 += Time.deltaTime;
        if (PillowsCanvas.pillowsPressed == 1) time1 += Time.deltaTime;
    }

    void SetPillowPressLevel()
    {
        if (time3 / timePlayed * 100 > 30) pillowsPressLevel = 3;
        else if (time2 / timePlayed * 100 > 30) pillowsPressLevel = 2;
        else if (time1 / timePlayed * 100 > 30) pillowsPressLevel = 1;
        else pillowsPressLevel = 0;
    }
    
}
