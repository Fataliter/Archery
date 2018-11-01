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
    public static float pillowsPress = 0f;
    public static float pillowsLegsDiff = 0f;


    public static int hitCounter=0;

    byte leftButton;

    string activeScene = string.Empty;

    bool keyPressed = false;

    public static float maxTime = 0f;
    public static float timePlayed = 0f;
    float keypressTime = 0f;
    public static float timeAlreadyPlayed = 0f;
    public static int[] targetsOnEnd;
    public static bool endOfMission = false;

    void Awake () {
        endOfMission = false;
        keyPressed = false;
        activeScene = SceneManager.GetActiveScene().name;
        timeAlreadyPlayed = GetDataFromSaveState();
    }

    private void Update() {
        parameters();
        timePlayed += Time.deltaTime;
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
            pillowsLegsDiff = PersistentManagerScript.Instance.config["training"]["legsDifferenceForPillows"].FloatValue;
            pillowsPress = PersistentManagerScript.Instance.config["training"]["pillowsPress"].FloatValue;
            savedData = SaveManager.Instance.state.targetsTraining;
            missionTime = SaveManager.Instance.state.timePlayedTraining;
            shootTargetCount = int.Parse(savedData);
        }
        if (activeScene == "Mission1")
        {
            maxTime = PersistentManagerScript.Instance.config["mission1"]["missionTime"].FloatValue;
            pillowsLegsDiff = PersistentManagerScript.Instance.config["mission1"]["legsDifferenceForPillows"].FloatValue;
            pillowsPress = PersistentManagerScript.Instance.config["mission1"]["pillowsPress"].FloatValue;
            savedData = SaveManager.Instance.state.targetsMission1;
            missionTime = SaveManager.Instance.state.timePlayedMission1;
            shootTargetCount = int.Parse(savedData);
        }
        if (activeScene == "Mission2")
        {
            maxTime = PersistentManagerScript.Instance.config["mission2"]["missionTime"].FloatValue;
            pillowsLegsDiff = PersistentManagerScript.Instance.config["mission2"]["legsDifferenceForPillows"].FloatValue;
            pillowsPress = PersistentManagerScript.Instance.config["mission2"]["pillowsPress"].FloatValue;
            savedData = SaveManager.Instance.state.targetsMission2;
            missionTime = SaveManager.Instance.state.timePlayedMission2;
            data = savedData.Split(',').Select(int.Parse).ToArray();
            shootTargetCount = data[0];
            enemy1Count = data[1];
        }
        if (activeScene == "Mission3")
        {
            maxTime = PersistentManagerScript.Instance.config["mission3"]["missionTime"].FloatValue;
            pillowsLegsDiff = PersistentManagerScript.Instance.config["mission3"]["legsDifferenceForPillows"].FloatValue;
            pillowsPress = PersistentManagerScript.Instance.config["mission3"]["pillowsPress"].FloatValue;
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
            pillowsLegsDiff = PersistentManagerScript.Instance.config["mission4"]["legsDifferenceForPillows"].FloatValue;
            pillowsPress = PersistentManagerScript.Instance.config["mission4"]["pillowsPress"].FloatValue;
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
            targetsOnEnd = new int[1];
            targetsOnEnd[0] = T;
        }   
        if (activeScene == "Mission1")
        {
            SaveManager.Instance.state.timePlayedMission1 = timeAlreadyPlayed;     
            SaveManager.Instance.state.targetsMission1 = T.ToString();
            targetsOnEnd = new int[1];
            targetsOnEnd[0] = T;
        } 
        if (activeScene == "Mission2")
        {
            SaveManager.Instance.state.timePlayedMission2 = timeAlreadyPlayed;  
            SaveManager.Instance.state.targetsMission2 = T.ToString() + "," + E1.ToString();
            targetsOnEnd = new int[2];
            targetsOnEnd[0] = T;
            targetsOnEnd[1] = E1;
        }
        if (activeScene == "Mission3")
        {
            SaveManager.Instance.state.timePlayedMission3 = timeAlreadyPlayed;  
            SaveManager.Instance.state.targetsMission3 = T.ToString() + "," + E1.ToString() + "," + E2.ToString();
            targetsOnEnd = new int[3];
            targetsOnEnd[0] = T;
            targetsOnEnd[1] = E1;
            targetsOnEnd[2] = E2;
        }
        if (activeScene == "Mission4")
        {
            SaveManager.Instance.state.timePlayedMission4 = timeAlreadyPlayed;  
            SaveManager.Instance.state.targetsMission4 = T.ToString() + "," + E2.ToString() + "," + E3.ToString();
            targetsOnEnd = new int[3];
            targetsOnEnd[0] = T;
            targetsOnEnd[1] = E2;
            targetsOnEnd[2] = E3;
        }
    }


    void EndMission()
    {
        bool onetime = true;
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
            }
            GameObject.Instantiate(EndMissionParticles);
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
    
}
