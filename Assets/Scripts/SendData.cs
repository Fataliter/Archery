﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SendData : MonoBehaviour {


	public static void SaveDataFromMission()
    {
        StreamWriter writer = new StreamWriter(PersistentManagerScript.Instance.gameFolder + @"\Scores\" + PersistentManagerScript.Instance.date + ".txt", true);
        writer.Write(PersistentManagerScript.Instance.data.missionName + "@");
        writer.Write(PersistentManagerScript.Instance.data.timeOfPlaying + "@");
        writer.Write(PersistentManagerScript.Instance.data.timeToHit + "@");
        writer.Write(PersistentManagerScript.Instance.data.angle + "@");
        writer.Write(PersistentManagerScript.Instance.data.targetLocation + "@");
        writer.Write(PersistentManagerScript.Instance.data.hitAngle + "@");
        writer.Write(PersistentManagerScript.Instance.data.pressOnLeftLeg + "@");
        writer.Write(PersistentManagerScript.Instance.data.pressOnRightLeg + "@");
        writer.Write(PersistentManagerScript.Instance.data.pressOnLeft + "@");
        writer.Write(PersistentManagerScript.Instance.data.pressOnRight + "@");
        writer.Write(PersistentManagerScript.Instance.data.pressOnRear + "@");
        writer.Write(PersistentManagerScript.Instance.data.targetAngleLeft + "@");
        writer.Write(PersistentManagerScript.Instance.data.targetAngleRight + "@");
        writer.Write(PersistentManagerScript.Instance.data.pillowsLevel + "@");
        writer.Write(Mathf.Round(PersistentManagerScript.Instance.config["general"]["rotationSensitivity"].FloatValue * 100) + "#"); //deg/sec
        writer.Close();
        

        
        PersistentManagerScript.Instance.data.timeToHit = "";
        PersistentManagerScript.Instance.data.angle = "";
        PersistentManagerScript.Instance.data.targetLocation = "";
        PersistentManagerScript.Instance.data.hitAngle = "";
        PersistentManagerScript.Instance.data.pressOnLeftLeg = "";
        PersistentManagerScript.Instance.data.pressOnRightLeg = "";
        PersistentManagerScript.Instance.data.pressOnLeft = "";
        PersistentManagerScript.Instance.data.pressOnRight = "";
        PersistentManagerScript.Instance.data.pressOnRear = "";
        PersistentManagerScript.Instance.data.targetAngleLeft = "";
        PersistentManagerScript.Instance.data.targetAngleRight = "";
        PersistentManagerScript.Instance.data.pillowsLevel = "";
    }
}