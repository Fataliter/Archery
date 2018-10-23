using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActualSceneInfo : MonoBehaviour {

    Scene activeScene;
    public static bool[] actualScene;

    void Awake () {
        activeScene = SceneManager.GetActiveScene();
        string[] sceneNames = { "Training", "Mission1", "Mission2", "Mission3", "Mission4"};
        actualScene = new bool[sceneNames.Length];
        for (int i = 0; i < sceneNames.Length; i++)
        {
            if (sceneNames[i] == activeScene.name) actualScene[i] = true;
            else actualScene[i] = false;
        }
    }
	
}
