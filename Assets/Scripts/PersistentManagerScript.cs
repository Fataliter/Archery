using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WebSocketSharp;
using SharpConfig;
using System.Linq;
using Newtonsoft.Json;

public class PersistentManagerScript : MonoBehaviour {

    public string date;
    public static PersistentManagerScript Instance { get; private set; }
    public MyData mydata = new MyData();
    public Data data = new Data();
    public Medals medalsMenu = new Medals();
    public Configuration config = new Configuration();
    public MessageFromJstep msg = new MessageFromJstep();
    WebSocket ws;
    public string gameFolder, userID = "";
    public bool finishedLoad, connected, serverDown;

    public bool hiddenFolder;

    private string requestID = JsonUtility.ToJson(new MessageToJstep("request", "getUserData"));


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
        hiddenFolder = false;
    }

    

    public void WebSocketClose()
    {
        ws.Close();
    }

    

    void Start()
    {
        finishedLoad = false;
        connected= false;
        ws = new WebSocket("ws://localhost:8080/JanekWebServer/measurement");
        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("opened");
            serverDown = false;
            connected = true;
        };
        ws.OnError += (sender, e) =>
        {
            if (e.Message.Contains("An error has occurred during the OnClose event."))
            {
                serverDown = true;
                Debug.Log("after 10 reconnect retries, serverDown = " + serverDown);
            }
        };
        ws.OnMessage += (sender, e) =>
        {
            //Debug.Log("achieved data from server: " + e.Data);
            if (e.Data == "Connection Established")
            {}
            else
            {
                if (e.Data.Contains("userDataReply"))
                {
                    msg = JsonConvert.DeserializeObject<MessageFromJstep>(e.Data);
                    userID = msg.data.userName + "_" + msg.data.userId;
                    hiddenFolder = false;
                    LoadGame(userID, 0, 0);
                }
                else mydata = JsonUtility.FromJson<MyData>(e.Data);
            }
            if (userID == "") ws.Send(requestID);

        };
        ws.OnClose += (sender, e) =>
        {
            if (userID != "OfflineLocalUser")
            {
                if (e.Code != (ushort)CloseStatusCode.NoStatus)
                {
                    Debug.Log("Disconnected! CloseStatusCode #" + e.Code + ": " + e.Reason);
                    connected = false;
                    ws.Connect();
                }
                else Debug.Log("Disconnected! CloseStatusCode #" + e.Code + ": Used disconnecting with server function websocket.close() during game [Exit button in MenuMedieval]");
            }
            else
            {
                Debug.Log("Logged as OfflineLocalUser");
                serverDown = true;
            }


        };
        ws.Connect();
    }
    

    void ReadConfig(byte steer, byte save)
    {
        if(!File.Exists(gameFolder + "/config.cfg")) 
        {
            var g = new GeneralConfig()
            {
                rotationSensitivity = 0.18f,
                legsDifferenceForRotation = 30,
                pillowsPressTimeCount = 30,
                timePercentageForPillows = 50,
                pillowsLevels = "110,85",
                arrowPositionAngle = 9,
                banditMoveSpeed = 3,
                goblinMoveSpeed = 3,
                dragonMoveSpeed = 4,
                lineRGBColor = "df11d9",
                fadeTime = 0.4f,
                maxMusicVolume = 0.09f,
                calmMusicRange = 6,
                calmMusicOn = 0,
                keyboardSteerPlayer = steer,
                keyboardSteerSaveStatusJSON = save,
                pillowOnIfTimeLow = 0
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
            m.gold = "60,250,24"; m.trophy = "120,600,55"; m.missionTime = 120;
            config.Add(Section.FromObject("mission2", m));

            m.bronze = "12,50,4,2"; m.silver = "36,120,12,6";
            m.gold = "75,280,27,14"; m.trophy = "140,700,60,35"; m.missionTime = 150;
            config.Add(Section.FromObject("mission3", m));

            m.bronze = "15,60,8,6"; m.silver = "40,130,20,13";
            m.gold = "80,300,40,30"; m.trophy = "160,750,75,60"; m.missionTime = 180;
            config.Add(Section.FromObject("mission4", m));

            config.SaveToFile(gameFolder + "/config.cfg");

        }
        else config = Configuration.LoadFromFile(gameFolder + "/config.cfg");
        
    }

    void ReadTrophies()
    {
        if (File.Exists(gameFolder + "/trophies.data"))
        {
            FileStream file = File.Open(gameFolder + "/trophies.data", FileMode.Open);
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

    public void LoadGame(string userID, byte steerByKeyboard, byte saveStatusKeyboard)
    {
        string userFolder = System.Environment.GetEnvironmentVariable("USERPROFILE") + @"\Balancer\Users\" + userID;
        gameFolder = userFolder + @"\Archery";
        if (!Directory.Exists(userFolder))
            Directory.CreateDirectory(userFolder);
        DirectoryInfo dir = new DirectoryInfo(userFolder);
        dir.Attributes &= ~FileAttributes.Hidden;
        if (!Directory.Exists(gameFolder))
            Directory.CreateDirectory(gameFolder);
        if (!Directory.Exists(gameFolder + @"\Scores"))
            Directory.CreateDirectory(gameFolder + @"\Scores");
        ReadConfig(steerByKeyboard, saveStatusKeyboard);
        ReadTrophies();
        SaveManager.Instance.Load();
    }

    public void HideFolder()
    {
        if(hiddenFolder)
        {
            string userFolder = System.Environment.GetEnvironmentVariable("USERPROFILE") + @"\Balancer\Users\" + userID;
            DirectoryInfo dir = new DirectoryInfo(userFolder);
            dir.Attributes |= FileAttributes.Hidden;
        }
    }

    public void SendRequestId()
    {
        ws.Send(requestID);
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

    public void setToZero()
    {
        this.LeftLeg = 0;
        this.RightLeg = 0;
        this.LeftPillow = 0;
        this.RightPillow = 0;
        this.RearPillow = 0;
        this.LeftButton = 1;
        this.RightButton = 1;
    }
}
public class Data
{
    public string missionName = "";
    public float timeOfPlaying = 0;
    public string timeToHit = "";
    public string angle = "";
    public string targetLocation = "";
    public string hitAngle = "";
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
    public byte calmMusicOn { get; set; }
    public byte keyboardSteerPlayer {get;set;}
    public byte keyboardSteerSaveStatusJSON { get; set; }
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
    public string type; 
    public string requestName;
    public MessageToJstep(string type, string requestName)
    {
        this.type = type;
        this.requestName = requestName;
    }
    

}

public class MessageFromJstep
{
    public string type { get; set; }
    public data data { get; set; }

}

public class data
{
    public int userId { get; set; }
    public string userName { get; set; }
    public string userLastLoggedOn { get; set; }
    public string userLang { get; set; }
}