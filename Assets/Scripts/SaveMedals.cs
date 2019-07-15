using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveMedals {

    public void Save()
    {
        FileStream file = File.Create(PersistentManagerScript.Instance.gameFolder + "/trophies.data");
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, PersistentManagerScript.Instance.medalsMenu);
        file.Close();
    }

}
