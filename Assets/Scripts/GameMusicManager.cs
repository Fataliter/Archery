using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicManager : MonoBehaviour {

    private AudioSource audioSrc;
    public AudioClip[] clips;

    void Awake()
    {
        bool[] ActSc = ActualSceneInfo.actualScene;
        audioSrc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        for (int i=0; i< ActSc.Length; i++)
        {
            if (ActualSceneInfo.actualScene[i] == true)
            {
                audioSrc.clip = clips[i];
                audioSrc.Play();
            }
        }
    }
    
}
