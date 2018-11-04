using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysLookAt : MonoBehaviour {

    Transform player;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Transform>();
    }

    void Update()
    {
        transform.LookAt(player);
    }
}
