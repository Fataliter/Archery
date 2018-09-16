using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour {
    public static float timePlay = 0;
	
	public void Update () {
        timePlay += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PersistentManagerScript.Instance.data.timeOfPlaying = timePlay;
            PersistentManagerScript.Instance.data.missionName = "mission1";
            SendData.SaveDataFromMission();
            SceneManager.LoadScene("MenuMedievalMissionChoice");
        }
	}
}
