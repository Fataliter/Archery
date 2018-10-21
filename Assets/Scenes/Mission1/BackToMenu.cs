using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour {
    public static float timePlay;

    private void Start()
    {
        timePlay = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "arrow")
        {
            PersistentManagerScript.Instance.data.timeOfPlaying = timePlay;
            PersistentManagerScript.Instance.data.missionName = "village";
            SendData.SaveDataFromMission();
            SceneManager.LoadScene("MenuMedieval");
        }
    }

    void Update () {
        timePlay += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PersistentManagerScript.Instance.data.timeOfPlaying = timePlay;
            PersistentManagerScript.Instance.data.missionName = "village";
            SendData.SaveDataFromMission();
            SceneManager.LoadScene("MenuMedievalMissionChoice");
        }
	}
}
