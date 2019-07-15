using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour {

    private LineRenderer lineRendComp;
    RaycastHit hit;


    void Start ()
    {
        Color lineColor = new Color();
        string colorFromCfg = "#" + PersistentManagerScript.Instance.config["general"]["lineRGBColor"].StringValue.ToUpper();
        ColorUtility.TryParseHtmlString(colorFromCfg, out lineColor);
        lineRendComp = GetComponent<LineRenderer>();
        lineRendComp.useWorldSpace = true;
        lineRendComp.material.color = lineColor;
        lineRendComp.positionCount = 2;
    }
	
	void Update () {
        Vector3 lessLength = new Vector3(Mathf.Sin(transform.eulerAngles.y * Mathf.PI / 180)/1.5f, 0, Mathf.Cos(transform.eulerAngles.y * Mathf.PI / 180)/1.5f);
        Ray myRay = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(myRay,out hit,Mathf.Infinity,1)) // 1 = pomijanie obiektów w warstwie 1 (wystrzelona strzała)
        {
            lineRendComp.SetPosition(0, transform.position);
            lineRendComp.SetPosition(1, hit.point - lessLength);
        }

    }

   

}
