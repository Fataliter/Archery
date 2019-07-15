using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour {

    public Image imageCircle;
    public Text textLoading, textFailed, textShutdown;
    private float shutdownTime, spinTimeCount, loadingTime;
    private const float reconnectDelay = 2.5f;
    private const int maxLoadingTime = 15;
    private string shutdownMessage;
    private bool loadingFailed;

    private float offlineConnectTime;

    bool onetime;

	// Use this for initialization
	void Start () {
        //onetime = true;
        loadingFailed = false;
        shutdownMessage = "Gra zostanie wylaczona za: ";
        textLoading.enabled = true;
        textFailed.enabled = false;
        textShutdown.enabled = false;


        shutdownTime = 5f;
        loadingTime = 0f;
        spinTimeCount = 0f;
        offlineConnectTime=0f;


}

    // Update is called once per frame
    void Update () {
        SpinCircle();
        OnConnect();
        OnServerDown();
        OnFailedConnection();
        
        
        if (PersistentManagerScript.Instance.finishedLoad)
        {
            textLoading.enabled = true;
            textFailed.enabled = false;
            textShutdown.enabled = false;
            textLoading.text = "Pomyslnie wczytano dane uzytkownika.";
            SceneManager.LoadScene("MenuMedieval");
        }
        
        
    }

    void SpinCircle()
    {
        spinTimeCount += Time.deltaTime;
        if (spinTimeCount >= 0.5f)
        {
            imageCircle.transform.Rotate(0, 0, 22.5f);
            spinTimeCount = 0;
            if(PersistentManagerScript.Instance.userID=="")
                PersistentManagerScript.Instance.SendRequestId();
        }
    }

    void OnConnect()
    {
        if (PersistentManagerScript.Instance.connected)
            textLoading.text = "Ladowanie danych uzytkownika...";
    }

    void OnServerDown()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.K))
        {
            offlineConnectTime += Time.deltaTime;
            if (offlineConnectTime >= 1.5f)
            {
                string userID = "OfflineLocalUser";
                PersistentManagerScript.Instance.userID = "OfflineLocalUser";
                PersistentManagerScript.Instance.WebSocketClose();
                PersistentManagerScript.Instance.hiddenFolder = true;
                PersistentManagerScript.Instance.LoadGame(userID, 1, 1);
            }
        }
        else offlineConnectTime = 0f;

        if (PersistentManagerScript.Instance.connected == false)
        {
            textLoading.text = "Laczenie z serwerem Jstep...";
            if (PersistentManagerScript.Instance.serverDown) { loadingTime += Time.deltaTime; }
            if (loadingTime>=15f)
            {
                textLoading.enabled = false;
                textFailed.enabled = true;
                textShutdown.enabled = true;
                loadingFailed = true;
            }
        }
    }

    void OnFailedConnection()
    {
        if (loadingFailed && !PersistentManagerScript.Instance.finishedLoad)
        {
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
                Application.Quit();
                Debug.Log("wychodze");
            }
        }
    }
}
