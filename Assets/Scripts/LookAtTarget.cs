using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {

    
    string[] tags = { "Target", "Enemy1", "Enemy2", "Enemy3" };
    GameObject target, enemy1, enemy2, enemy3;


	void Update () {
        target = GameObject.FindGameObjectWithTag(tags[0]);
        enemy1 = GameObject.FindGameObjectWithTag(tags[1]);
        enemy2 = GameObject.FindGameObjectWithTag(tags[2]);
        enemy3 = GameObject.FindGameObjectWithTag(tags[3]);
        if (target != null)
            transform.LookAt(target.transform.position);
        if (enemy1 != null)
            transform.LookAt(enemy1.transform.position);
        if (enemy2 != null)
            transform.LookAt(enemy2.transform.position);
        if (enemy3 != null)
            transform.LookAt(enemy3.transform.position);
    }
}
