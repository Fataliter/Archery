using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnLightTrophies : MonoBehaviour {

    public Light lightPrefab;

    void Start()
    {
        GameObject.Instantiate(lightPrefab);
    }

}
