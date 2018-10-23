using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicManager : MonoBehaviour {

    private AudioSource AudioSrc;
    public AudioClip[] Clips;
    Scene activeScene;

    void Awake()
    {
        activeScene = SceneManager.GetActiveScene();
        AudioSrc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        string[] sceneNames = { "Training", "Mission1", "Mission2" };
        //Clips = new AudioClip[sceneNames.Length];
        for(int i=0; i<sceneNames.Length; i++)
        {
            if (sceneNames[i] == activeScene.name)
            {
                AudioSrc.clip = Clips[i];
                AudioSrc.Play();
            }
        }
    }
    
}
