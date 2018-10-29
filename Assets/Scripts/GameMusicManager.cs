using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicManager : MonoBehaviour {

    private AudioSource AudioSrc;
    public AudioClip[] Clips;

    void Awake()
    {
        bool[] ActSc = ActualSceneInfo.actualScene;
        AudioSrc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        for (int i=0; i< ActSc.Length; i++)
        {
            if (ActualSceneInfo.actualScene[i] == true)
            {
                AudioSrc.clip = Clips[i];
                AudioSrc.Play();
            }
        }
    }
    
}
