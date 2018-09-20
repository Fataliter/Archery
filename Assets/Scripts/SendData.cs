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
            writer.WriteLine(PersistentManagerScript.Instance.data.timeOnLeftPillow);
            writer.WriteLine(PersistentManagerScript.Instance.data.timeOnRightPillow);
            writer.WriteLine(PersistentManagerScript.Instance.data.timeOnRearPillow);
            writer.WriteLine("left_leg");
            writer.WriteLine(PersistentManagerScript.Instance.data.pressOnLeftLeg);
            writer.WriteLine("right_leg");
            writer.WriteLine(PersistentManagerScript.Instance.data.pressOnRightLeg);
            writer.WriteLine("left_pillow");
            writer.WriteLine(PersistentManagerScript.Instance.data.pressOnLeft);
            writer.WriteLine("right_pillow");
            writer.WriteLine(PersistentManagerScript.Instance.data.pressOnRight);
            writer.Close();

            PersistentManagerScript.Instance.data.timeToHit = "";
            PersistentManagerScript.Instance.data.angle = "";
            PersistentManagerScript.Instance.data.points = "";
            PersistentManagerScript.Instance.data.timeOnLeftPillow = "";
            PersistentManagerScript.Instance.data.timeOnRightPillow = "";
            PersistentManagerScript.Instance.data.timeOnRearPillow = "";
            PersistentManagerScript.Instance.data.pressOnLeftLeg = "";
            PersistentManagerScript.Instance.data.pressOnRightLeg = "";
            PersistentManagerScript.Instance.data.pressOnLeft = "";
            PersistentManagerScript.Instance.data.pressOnRight = "";
        }
    }
}