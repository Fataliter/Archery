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
        Ray myRay = new Ray(transform.position, transform.right);
        if(Physics.Raycast(myRay,out hit,400,1)) // 1 = pomijanie obiektów w warstwie 1 (wystrzelona strzała)
        {
            //Debug.Log(hit.transform.name);
            //Debug.DrawLine(myRay.origin, hit.point, Color.magenta);
            lineRendComp.SetPosition(0, transform.position);
            lineRendComp.SetPosition(1, hit.point);
        }

    }
}
