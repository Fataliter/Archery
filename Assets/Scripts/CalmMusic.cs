using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalmMusic : MonoBehaviour {

    AudioSource calmSrc, musicSrc;
    Transform player, lookerLeft, lookerRight;
    float difference;
    float playerAngle, rightAngle, leftAngle;
    float maxVolume;
    float calmMusicRange;


    private void Awake()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Training")
            this.enabled = false;
        else
            this.enabled = true;
        if(PersistentManagerScript.Instance.config["general"]["calmMusicOn"].IntValue==1)
            this.enabled = true;
        else this.enabled = false;
    }
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        lookerLeft = GameObject.Find("LookAtTarget/LookAtRight").GetComponent<Transform>();
        lookerRight = GameObject.Find("LookAtTarget/LookAtLeft").GetComponent<Transform>();
        calmSrc = gameObject.GetComponent<AudioSource>();
        musicSrc = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        difference = 0.005f;
        maxVolume = PersistentManagerScript.Instance.config["general"]["maxMusicVolume"].FloatValue;
        calmMusicRange = PersistentManagerScript.Instance.config["general"]["calmMusicRange"].FloatValue;
    }
	
	// Update is called once per frame
	void Update () {

        SetAngles();

        if (playerAngle>= (rightAngle - calmMusicRange) && playerAngle<=(leftAngle + calmMusicRange))
        {
            VolumeUp(calmSrc);
            VolumeDown(musicSrc);
        }
        else
        {
            VolumeUp(musicSrc);
            VolumeDown(calmSrc);
        }
	}

    void SetAngles()
    {
        if (player.eulerAngles.y > 180)
            playerAngle = player.eulerAngles.y - 360f;
        else
            playerAngle = player.eulerAngles.y;

        if (lookerLeft.eulerAngles.y > 180)
            leftAngle = lookerLeft.eulerAngles.y - 360f;
        else
            leftAngle = lookerLeft.eulerAngles.y;

        if (lookerRight.eulerAngles.y > 180)
            rightAngle = lookerRight.eulerAngles.y - 360f;
        else
            rightAngle = lookerRight.eulerAngles.y;

    }

    void VolumeUp(AudioSource audio)
    {
        audio.volume += difference;
        audio.volume = Mathf.Clamp(audio.volume, 0f, maxVolume);
    }

    void VolumeDown(AudioSource audio)
    {
        audio.volume -= difference;
        audio.volume = Mathf.Clamp(audio.volume, 0f, maxVolume);
    }
}
