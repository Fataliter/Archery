using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    public GameObject lightt;
    private void Start()
    {
        lightt = GameObject.Instantiate(lightt);
    }
}
