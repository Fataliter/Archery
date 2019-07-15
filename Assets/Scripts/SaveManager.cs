using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
//using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;

public class SaveManager : MonoBehaviour
{

    public static SaveManager Instance { get; private set; }
    public SaveState state;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        //Debug.Log(SerializeHelper.Serialize<SaveState>(state));

    }

    public void Save()
    {
        FileStream file = File.Create(PersistentManagerScript.Instance.gameFolder + "/status.json");
        AddText(file, JsonConvert.SerializeObject(state));
        Debug.Log("Zapisano postepy misji do status.json: " + JsonConvert.SerializeObject(state));
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(PersistentManagerScript.Instance.gameFolder + "/status.json"))
        {
            FileStream file = File.Open(PersistentManagerScript.Instance.gameFolder + "/status.json", FileMode.Open);
            byte[] b = new byte[1024];
            UTF8Encoding temp = new UTF8Encoding(true);
            string tempString = "";
            while (file.Read(b, 0, b.Length) > 0)
            {
                tempString+=temp.GetString(b);
            }
            Debug.Log("wczytano pomyslnie status.json: " + tempString);
            state = JsonUtility.FromJson<SaveState>(tempString);
            file.Close();
            PersistentManagerScript.Instance.finishedLoad = true;
        }
        else
        {
            Debug.Log("nie odnalezioni pliku status.json, zostanie utworzony nowy plik.");
            state = new SaveState();
            Save();
            Load();
        }
    }

    public void SaveToRegedit()
    {
        PlayerPrefs.SetString("save", SerializeHelper.Serialize<SaveState>(state));
        Debug.Log("zapisano postępy misji");
    }

    public void LoadFromRegedit()
    {
        
        if (PlayerPrefs.HasKey("save"))
        {
            state = SerializeHelper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("no save file found");
        }
    }

    private static void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }
}