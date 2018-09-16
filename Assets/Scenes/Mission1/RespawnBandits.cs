using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class RespawnBandits : MonoBehaviour {

    public GameObject bandit;
    GameObject banditInstantiate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void BanditInst()
    {
        banditInstantiate = GameObject.Instantiate(bandit);
        
    }

}
