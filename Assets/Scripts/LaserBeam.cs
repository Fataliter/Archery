using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour {

    private LineRenderer lineRendComp;
    RaycastHit hit;

    void Start () {
        lineRendComp = GetComponent<LineRenderer>();
        lineRendComp.useWorldSpace = true;
    }
	
	void Update () {
        Ray myRay = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(myRay,out hit,Mathf.Infinity,1)) // 1 = pomijanie obiektów w warstwie 1 (wystrzelona strzała)
        {
            lineRendComp.SetPosition(0, transform.position);
            lineRendComp.SetPosition(1, hit.point);
        }

    }
}
