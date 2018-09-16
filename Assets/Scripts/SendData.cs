using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SendData : MonoBehaviour {
	public static void SaveDataFromMission()
    {
        if (PersistentManagerScript.Instance.data.points.Length > 1)
        {
            StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/Wyniki/" + PersistentManagerScript.Instance.date + ".txt", true);
            writer.WriteLine(PersistentManagerScript.Instance.data.missionName);
            writer.WriteLine(PersistentManagerScript.Instance.data.timeOfPlaying);
            writer.WriteLine(PersistentManagerScript.Instance.data.timeToHit);
            writer.WriteLine(PersistentManagerScript.Instance.data.angle);
            writer.WriteLine(PersistentManagerScript.Instance.data.points);
            writer.Close();
            PersistentManagerScript.Instance.data.timeToHit = "";
            PersistentManagerScript.Instance.data.angle = "";
            PersistentManagerScript.Instance.data.points = "";
        }
    }
}