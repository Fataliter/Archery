using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WebSocketSharp;
using SharpConfig;

public class PersistentManagerScript : MonoBehaviour {

    public string date;
    public static PersistentManagerScript Instance { get; private set; }
    public MyData mydata = new MyData();
    public Data data = new Data();
    public Medals medalsMenu = new Medals();
    public Configuration config = new Configuration();
    WebSocket ws;


    public SaveMedals saveMedals;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        date = System.DateTime.Now.ToString("dd_MM_yyyy HH_mm_ss");
        if (!Directory.Exists(Application.persistentDataPath + "/Wyniki"))
            Directory.CreateDirectory(Application.persistentDataPath + "/Wyniki");
        ReadTrophies();
        ReadConfig();
    }

    public void WebSocketClose()
    {
        ws.Close();
    }

    void Start()
    {
        ws = new WebSocket("ws://localhost:8080/JanekWebServer/measurement");
        ws.OnOpen += (sender, e) => Debug.Log("opened");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            mydata = JsonUtility.FromJson<MyData>(e.Data);
        };
    }

    void AssignData(string json)
    {
        mydata = JsonUtility.FromJson<MyData>(json);
    }

    void ReadConfig()
    {
        config = Configuration.LoadFromFile(Application.persistentDataPath + "/config.cfg");
    }

    void ReadTrophies()
    {
        if (File.Exists(Application.persistentDataPath + "/trophies.data"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/trophies.data", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            medalsMenu = (Medals)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            saveMedals = new SaveMedals();
            saveMedals.Save();
        }
    }
}
public class MyData
{
    public int LeftLeg = 0;
    public int RightLeg = 0;
    public int LeftPillow = 0;
    public int RightPillow = 0;
    public int RearPillow = 0;
    //public int FrontPillow = 1;
    public int LeftButton = 1;
    public int RightButton = 1;
}
public class Data
{
    public string missionName = "";
    public float timeOfPlaying = 0;
    public string timeToHit = "";
    public string angle = "";
    public string targetLocation = "";
    public string hitAngle = "";
    public string points = "";
    public string pressOnLeftLeg = "";
    public string pressOnRightLeg = "";
    public string pressOnLeft = "";
    public string pressOnRight = "";
    public string pressOnRear = "";
    public string targetAngleLeft = "";
    public string targetAngleRight = "";
}
[Serializable]
public class Medals
{
    public int medalb1 = 0;
    public int medalb2 = 0;
    public int medalb3 = 0;
    public int medalb4 = 0;
    public int medals1 = 0;
    public int medals2 = 0;
    public int medals3 = 0;
    public int medals4 = 0;
    public int medalg1 = 0;
    public int medalg2 = 0;
    public int medalg3 = 0;
    public int medalg4 = 0;
    public int trophy1 = 0;
    public int trophy2 = 0;
    public int trophy3 = 0;
    public int trophy4 = 0;
    public int training1 = 0;
    public int training2 = 0;
    public int training3 = 0;
    public int training4 = 0;
}