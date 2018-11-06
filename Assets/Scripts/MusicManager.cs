using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance { get; private set; }
    string actualScene;
    public AudioClip[] clips = new AudioClip[6];
    AudioSource audioSrc;
    string[] sceneNames = { "MenuMedieval", "Training", "Mission1", "Mission2", "Mission3", "Mission4" };


    void Awake()
    {
        actualScene = SceneManager.GetActiveScene().name;
        audioSrc = GetComponent<AudioSource>();
        clips[0] = Resources.Load<AudioClip>("Music/MenuMusic");
        clips[1] = Resources.Load<AudioClip>("Music/TrainingMusic");
        clips[2] = Resources.Load<AudioClip>("Music/Mission1Music");
        clips[3] = Resources.Load<AudioClip>("Music/Mission2Music");
        //clips[4] = Resources.Load<AudioClip>("Music/");
        //clips[5] = Resources.Load<AudioClip>("Music/");
        audioSrc.clip = clips[0];
        audioSrc.Play();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if(MissionManager.fireworks)
        {
            audioSrc.Stop();
            MissionManager.fireworks = false;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MenuMedievalMissionChoice" && audioSrc.clip != clips[0])
        {
            audioSrc.clip = clips[0];
            audioSrc.Play();
        }
        for (int i=0; i<sceneNames.Length; i++)
        {
            if (scene.name == sceneNames[i] && audioSrc.clip!=clips[i])
            {
                audioSrc.clip = clips[i];
                audioSrc.Play();
            }
        }
        //Debug.Log("OnSceneLoaded: " + scene.name);
    }
   
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
