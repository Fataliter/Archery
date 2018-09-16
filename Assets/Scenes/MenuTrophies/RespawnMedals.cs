using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class RespawnMedals : MonoBehaviour {

    public GameObject[] medal;
    public GameObject[] lightPrefab;
    GameObject[] medalModel = new GameObject[4];
    GameObject[] lightModel = new GameObject[4];
    public GameObject lightDirect;
    GameObject lightDirectInstance;

    void Start ()
    {
        Respawn();
    }
	
	void Update ()
    {
		
	}

    public void Respawn()
    {
        DestroyObjects();
            if (ControlMenuTrophies.index == 0)
            {
                lightDirectInstance = GameObject.Instantiate(lightDirect);
                if (PersistentManagerScript.Instance.medalsMenu.medalb1 == 1)
                    InstantiateModels(0);
                if (PersistentManagerScript.Instance.medalsMenu.medals1 == 1)
                    InstantiateModels(1);
                if (PersistentManagerScript.Instance.medalsMenu.medalg1 == 1)
                    InstantiateModels(2);
                if (PersistentManagerScript.Instance.medalsMenu.trophy1 == 1)
                    InstantiateModels(3);
            }
            else if (ControlMenuTrophies.index == 1)
            {
                lightDirectInstance = GameObject.Instantiate(lightDirect);
                if (PersistentManagerScript.Instance.medalsMenu.medalb2 == 1)
                    InstantiateModels(0);
                if (PersistentManagerScript.Instance.medalsMenu.medals2 == 1)
                    InstantiateModels(1);
                if (PersistentManagerScript.Instance.medalsMenu.medalg2 == 1)
                    InstantiateModels(2);
                if (PersistentManagerScript.Instance.medalsMenu.trophy2 == 1)
                    InstantiateModels(3);
            }
            else if (ControlMenuTrophies.index == 2)
            {
                lightDirectInstance = GameObject.Instantiate(lightDirect);
                if (PersistentManagerScript.Instance.medalsMenu.medalb3 == 1)
                    InstantiateModels(0);
                if (PersistentManagerScript.Instance.medalsMenu.medals3 == 1)
                    InstantiateModels(1);
                if (PersistentManagerScript.Instance.medalsMenu.medalg3 == 1)
                    InstantiateModels(2);
                if (PersistentManagerScript.Instance.medalsMenu.trophy3 == 1)
                    InstantiateModels(3);
            }
            else if (ControlMenuTrophies.index == 3)
            {
                lightDirectInstance = GameObject.Instantiate(lightDirect);
                if (PersistentManagerScript.Instance.medalsMenu.medalb4 == 1)
                    InstantiateModels(0);
                if (PersistentManagerScript.Instance.medalsMenu.medals4 == 1)
                    InstantiateModels(1);
                if (PersistentManagerScript.Instance.medalsMenu.medalg4 == 1)
                    InstantiateModels(2);
                if (PersistentManagerScript.Instance.medalsMenu.trophy4 == 1)
                    InstantiateModels(3);
            }
    }

    void DestroyObjects()
    {
        foreach (GameObject medall in medalModel)
        {
            if (medall != null)
                Destroy(medall);
        }
        foreach (GameObject lightt in lightModel)
        {
            if (lightt != null)
                Destroy(lightt);
        }
        if (lightDirectInstance != null)
            Destroy(lightDirectInstance);
    }

    void InstantiateModels(int which)
    {
        medalModel[which] = GameObject.Instantiate(medal[which]);
        lightModel[which] = GameObject.Instantiate(lightPrefab[which]);
    }
}
