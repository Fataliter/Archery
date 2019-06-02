using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WebSocketSharp;
using SharpConfig;
using System.Linq;

public class PersistentManagerScript : MonoBehaviour {
    
    public string date;
    public static PersistentManagerScript Instance { get; private set; }
    public MyData mydata = new MyData();
    public Data data = new Data();
    public Medals medalsMenu = new Medals();
    public Configuration config = new Configuration();
    public MessageFromJstep msg = new MessageFromJstep();
    WebSocket ws;
    public string userFolder, userID;


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
        var mesin = new MessageToJstep()
        {
            type = "getUserID"
        };
        string requestID = JsonUtility.ToJson(mesin);
        ws.Send(requestID);
        ws.OnMessage += (sender, e) =>
        {
            try{ mydata = JsonUtility.FromJson<MyData>(e.Data);} //to jest lekko z dupy :V
            catch{ msg = JsonUtility.FromJson<MessageFromJstep>(e.Data); 
            //userID = msg.user.name;
            }
        };
        ws.OnClose += (sender, e) =>
        {
            try{ws.Connect();}
            catch(Exception exc){Debug.Log(exc.Message);}
        };
        //userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }
    

    void ReadConfig()
    {
        if(!File.Exists(Application.persistentDataPath + "/config.cfg")) 
        { 
            var g = new GeneralConfig() 
            {
                rotationSensitivity=0.18f,
                legsDifferenceForRotation=30,
                pillowsPressTimeCount=30,
                timePercentageForPillows=50,
                pillowsLevels="110,85",
                arrowPositionAngle=9,
                banditMoveSpeed=3,
                goblinMoveSpeed=3,
                dragonMoveSpeed=4,
                lineRGBColor="df11d9",
                fadeTime=0.4f,
                maxMusicVolume=0.09f,
                calmMusicRange=6,
                keyboardSteerPlayer=0,
                pillowOnIfTimeLow=0
            }; config.Add(Section.FromObject("general", g));
            var t = new TrainingConfig()
            {
                bronze = "10,40",
                silver = "30,100",
                gold = "60,250",
                trophy = "120,600"
            }; config.Add(Section.FromObject("training", t));
            var m = new MissionConfig()
            {
                bronze = "10,40",
                silver = "30,100",
                gold = "60,250",
                trophy = "120,600",
                missionTime = 120
            }; config.Add(Section.FromObject("mission1", m));

            m.bronze = "10,40,3"; m.silver = "30,100,10";
            m.gold = "60,250,24";m.trophy = "120,600,55";m.missionTime = 120;
            config.Add(Section.FromObject("mission2", m));

            m.bronze = "12,50,4,2"; m.silver = "36,120,12,6";
            m.gold = "75,280,27,14";m.trophy = "140,700,60,35";m.missionTime = 150;
            config.Add(Section.FromObject("mission3", m));

            m.bronze = "15,60,8,6"; m.silver = "40,130,20,13";
            m.gold = "80,300,40,30";m.trophy = "160,750,75,60";m.missionTime = 180;
            config.Add(Section.FromObject("mission4", m));

            config.SaveToFile(Application.persistentDataPath + "/config.cfg");

        }
        else config = Configuration.LoadFromFile(Application.persistentDataPath + "/config.cfg");
        
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
    public string pillowsLevel = "";
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

public class GeneralConfig 
{
    public float rotationSensitivity {get;set;}
    public int legsDifferenceForRotation {get;set;}
    public int pillowsPressTimeCount {get;set;}
    public int timePercentageForPillows  {get;set;}
    public string pillowsLevels  {get;set;}
    public float arrowPositionAngle  {get;set;}
    public int banditMoveSpeed  {get;set;}
    public int goblinMoveSpeed  {get;set;}
    public int dragonMoveSpeed  {get;set;}
    public string lineRGBColor  {get;set;}
    public float fadeTime  {get;set;}
    public float maxMusicVolume  {get;set;}
    public float calmMusicRange {get;set;}
    public byte keyboardSteerPlayer {get;set;}
    public byte pillowOnIfTimeLow {get;set;}
}

public class MissionConfig
{
    public string bronze {get;set;}
    public string silver {get;set;}
    public string gold {get;set;}
    public string trophy {get;set;}
    public int missionTime {get;set;}
}

public class TrainingConfig
{
    public string bronze {get;set;}
    public string silver {get;set;}
    public string gold {get;set;}
    public string trophy {get;set;}
}

public class MessageToJstep
{
    public string type {get; set;}
    ///////////////////////////////////////////////////
    ///                    AKTUALNIE
    ///               {"type":"getUserId"}
    ///                   W PRZYSZŁOŚCI
    /// {"type":"request",data:{"requestName":"getUserId"} }
    //////////////////////////////////////////////////
    // public struct data
    // {
    //     string requestName {get; set;}
    // }
}

public class MessageFromJstep
{
    public struct user {
        int id {get; set;}
        string name {get; set;}
        string lastoLoggedOn {get; set;}
        string lang {get; set;}

    }
}