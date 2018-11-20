using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        Load();
        Debug.Log(SerializeHelper.Serialize<SaveState>(state));

    }

    public void Save()
    {
        PlayerPrefs.SetString("save", SerializeHelper.Serialize<SaveState>(state));
        Debug.Log("zapisano postępy misji");
    }

    public void Load()
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
}