using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class RespawnTargetMission1 : MonoBehaviour {

    public static float xOff;
    public static float zOff;
    public static bool ifDestroy = false;
    public static float timer=0f;
    public GameObject targetPosition;
    public GameObject target;
    Medals medals = new Medals();

    // Use this for initialization
    void Start () {
        RespawnArcherTarget();
    }
	
	// Update is called once per frame
	void Update () {
        if (ifDestroy == true)
        {
            Destroy(target);
            ifDestroy = false;
            Debug.Log("czas: " + timer);
            if (timer < 20)
            {
                medals.medalb1 = 1;
                Save();
            }
            if (timer < 18)
            {
                medals.medals1 = 1;
                Save();
            }
            if (timer < 15)
            {
                medals.medalg1 = 1;
                Save();
            }
            if (timer < 15)
            {
                medals.trophy1 = 1;
                Save();
            }
            RespawnArcherTarget();
        }
        timer += Time.deltaTime;
    }

    public void RespawnArcherTarget()
    {
        timer = 0f;
        target = GameObject.Instantiate(targetPosition);
        target.name = "target";
        Vector3 vector = transform.position;
        xOff = UnityEngine.Random.Range(-20f, 20f);
        zOff = UnityEngine.Random.Range(0f, 1f);
        vector.z = vector.z + zOff;
        vector.x = vector.x + xOff;
        target.transform.position = vector;
    }

    void Save()
    {
        FileStream file = File.Create(Application.persistentDataPath + "/saveik.data");
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, medals);
        file.Close();
    }
}
/*[Serializable]
class Medals
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
}*/
