using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class ServerDownManager : MonoBehaviour {

    public static ServerDownManager Instance { get; private set; }
    public Text textShutdown, textInfo;
    private string shutdownMessage;
    private float shutdownTime;
    private Canvas canvas;
    private const string infoReconnect = "Probuje polaczyc sie ponownie...";
    private const string infoShutdown = "Nalezy zrestartowac interpreter Jstep.";

    void Awake() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        shutdownMessage = "Gra zostanie wylaczona za: ";
    }
    // Use this for initialization
    void Start () {
        shutdownTime = 5f;
    }
	
	// Update is called once per frame
	void Update () {
        if(PersistentManagerScript.Instance.connected || PersistentManagerScript.Instance.userID == "OfflineLocalUser")
        {
            shutdownTime = 5f;
            canvas.enabled = false;
            textShutdown.enabled = false;
        }
        else
        {
            canvas.enabled = true;
            PersistentManagerScript.Instance.mydata.setToZero();
            if (PersistentManagerScript.Instance.serverDown)
            {
                textShutdown.enabled = true;
                textInfo.text = infoShutdown;
                shutdownTime -= Time.deltaTime;
                if (shutdownTime > 4) textShutdown.text = shutdownMessage + "5";
                if (shutdownTime > 3 && shutdownTime <= 4) textShutdown.text = shutdownMessage + "4";
                if (shutdownTime > 2 && shutdownTime <= 3) textShutdown.text = shutdownMessage + "3";
                if (shutdownTime > 1 && shutdownTime <= 2) textShutdown.text = shutdownMessage + "2";
                if (shutdownTime > 0 && shutdownTime <= 1) textShutdown.text = shutdownMessage + "1";
                if (shutdownTime < 0)
                {
                    textShutdown.text = shutdownMessage + "0";
                    PersistentManagerScript.Instance.WebSocketClose();
                    PersistentManagerScript.Instance.HideFolder();
                    try
                    {
                        PersistentManagerScript.Instance.data.timeOfPlaying = MissionManager.timePlayed;
                        SendData.SaveDataFromMission();
                    }
                    catch { }
                    Application.Quit();
                }
            }
            else
            {
                textShutdown.enabled = false;
                textInfo.text = infoReconnect;
            }

        }
    }

    

}
