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

    int maxTime = 302;
    float timePlayed = 0f;
    float keypressTime = 0f;
    public static float timeAlreadyPlayed = 0f;

    void Awake () {
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
            pillowsLegsDiff = PersistentManagerScript.Instance.config["training"]["legsDifferenceForPillows"].FloatValue;
            pillowsPress = PersistentManagerScript.Instance.config["training"]["pillowsPress"].FloatValue;
            savedData = SaveManager.Instance.state.targetsTraining;
            missionTime = SaveManager.Instance.state.timePlayedTraining;
            int.TryParse(savedData, out shootTargetCount);
        }
        if (activeScene == "Mission1")
        {
            pillowsLegsDiff = PersistentManagerScript.Instance.config["mission1"]["legsDifferenceForPillows"].FloatValue;
            pillowsPress = PersistentManagerScript.Instance.config["mission1"]["pillowsPress"].FloatValue;
            savedData = SaveManager.Instance.state.targetsMission1;
            missionTime = SaveManager.Instance.state.timePlayedMission1;
            int.TryParse(savedData, out shootTargetCount);
        }
        if (activeScene == "Mission2")
        {
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
            pillowsLegsDiff = PersistentManagerScript.Instance.config["mission4"]["legsDifferenceForPillows"].FloatValue;
            pillowsPress = PersistentManagerScript.Instance.config["mission4"]["pillowsPress"].FloatValue;
            savedData = SaveManager.Instance.state.targetsMission4;
            missionTime = SaveManager.Instance.state.timePlayedMission4;
            data = savedData.Split(',').Select(int.Parse).ToArray();
            shootTargetCount = data[0];
            enemy2Count = data[1];
            enemy3Count = data[2];
        }
        Debug.Log("Aktualna misja: " + activeScene);
        Debug.Log("wczytane targety: " + savedData);
        Debug.Log("wczytany czas w sekundach: " + missionTime);
        
        return missionTime;
    }
	
	void SaveTargetsAndTime(int T, int E1, int E2, int E3) {
        if (activeScene == "Training")
        {
            SaveManager.Instance.state.timePlayedTraining = timeAlreadyPlayed;     
            SaveManager.Instance.state.targetsTraining = T.ToString();
        }   
        if (activeScene == "Mission1")
        {
            SaveManager.Instance.state.timePlayedMission1 = timeAlreadyPlayed;     
            SaveManager.Instance.state.targetsMission1 = T.ToString();
        } 
        if (activeScene == "Mission2")
        {
            SaveManager.Instance.state.timePlayedMission2 = timeAlreadyPlayed;  
            SaveManager.Instance.state.targetsMission2 = T.ToString() + "," + E1.ToString();
        }
        if (activeScene == "Mission3")
        {
            SaveManager.Instance.state.timePlayedMission3 = timeAlreadyPlayed;  
            SaveManager.Instance.state.targetsMission3 = T.ToString() + "," + E1.ToString() + "," + E2.ToString();
        }
        if (activeScene == "Mission4")
        {
            SaveManager.Instance.state.timePlayedMission4 = timeAlreadyPlayed;  
            SaveManager.Instance.state.targetsMission4 = T.ToString() + "," + E2.ToString() + "," + E3.ToString();
        }
    }


    void EndMission()
    {
        if (Input.GetKey(KeyCode.Escape) || leftButton == 0) keypressTime += Time.deltaTime;
        else keypressTime = 0f;
        if (timePlayed >= maxTime)
        {
            timeAlreadyPlayed += 300f;
            SaveTargetsAndTime(shootTargetCount, enemy1Count, enemy2Count, enemy3Count);
            SaveManager.Instance.Save();
            ShowSavedData();
            GameObject.Instantiate(EndMissionParticles);
        }
        if (keypressTime > 3 && keyPressed == false)
        {
            keyPressed = true;
            timeAlreadyPlayed += timePlayed;
            SaveTargetsAndTime(shootTargetCount, enemy1Count, enemy2Count, enemy3Count);
            SaveManager.Instance.Save();
            ShowSavedData();
            SceneManager.LoadScene("MenuMedievalMissionChoice");
        }

    }

    void ShowSavedData()
    {
        if (activeScene == "Training")
        {
            Debug.Log("Zapisane targety: " + SaveManager.Instance.state.targetsTraining);
            Debug.Log("zapisany czas w sekundach: " + SaveManager.Instance.state.timePlayedTraining);
        }
        if (activeScene == "Mission1")
        {
            Debug.Log("Zapisane targety: " + SaveManager.Instance.state.targetsMission1);
            Debug.Log("zapisany czas w sekundach: " +  SaveManager.Instance.state.timePlayedMission1);
        }
        if (activeScene == "Mission2")
        {
            Debug.Log("Zapisane targety: " + SaveManager.Instance.state.targetsMission2);
            Debug.Log("zapisany czas w sekundach: " + SaveManager.Instance.state.timePlayedMission2);
        }
        if (activeScene == "Mission3")
        {
            Debug.Log("Zapisane targety: " + SaveManager.Instance.state.targetsMission3);
            Debug.Log("zapisany czas w sekundach: " + SaveManager.Instance.state.timePlayedMission3);
        }
        if (activeScene == "Mission4")
        {
            Debug.Log("Zapisane targety: " + SaveManager.Instance.state.targetsMission4);
            Debug.Log("zapisany czas w sekundach: " + SaveManager.Instance.state.timePlayedMission4);
        }
    }
}
