using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PersistentManagerScript : MonoBehaviour {

    public string date;
    public static PersistentManagerScript Instance { get; private set; }
    static string HOST = "127.0.0.1";
    static int PORT = 5001;
    public static TcpClient client;
    static Thread clientReceiveThread;
    public MyData mydata = new MyData();
    public Data data = new Data();
    public Medals medalsMenu = new Medals();
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
    }

    void Start()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
        date = System.DateTime.Now.ToString("dd_MM_yyyy HH_mm_ss");
        if (!Directory.Exists(Application.persistentDataPath + "/Wyniki"))
            Directory.CreateDirectory(Application.persistentDataPath + "/Wyniki");
        ReadTrophies();
    }

    void AssignData(string json)
    {
        mydata = JsonUtility.FromJson<MyData>(json);
    }

    private void ListenForData()
    {
        try
        {
            client = new TcpClient(HOST, PORT);
            byte[] bytes = new byte[1024];
            while (true)
            {
                // Get a stream object for reading 				
                using (NetworkStream stream = client.GetStream())
                {
                    int length;
                    // Read incomming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);
                        // Convert byte array to string message. 						
                        string serverMessage = Encoding.ASCII.GetString(incommingData);
                        Debug.Log(serverMessage);
                        AssignData(serverMessage);
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
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
    //public float timeAim;
    public string timeToHit = "";
    //public string timeOnTarget;
    public string angle = "";
    public string points = "";
    public string timeOnRightPillow = "";
    public string timeOnLeftPillow = "";
    public string timeOnRearPillow = "";
    public string pressOnLeftLeg = "";
    public string pressOnRightLeg = "";
    public string pressOnLeft = "";
    public string pressOnRight = "";
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
}