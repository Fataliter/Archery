using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFaster : MonoBehaviour {

    Canvas canvas;
    
	void Start ()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }
	
	void Update ()
    {
        ShowCanvas();
	}
    
    void ShowCanvas()
    {
        if (MissionManager.keyPressed == true)
        {
            canvas.enabled = true;
            Time.timeScale = 0.0f;
            if (ClickedButton.leftButtonDown || Input.GetKeyDown(KeyCode.Y))
            {
                Time.timeScale = 1.0f;
                MissionManager.endGameFaster = true;
                MissionManager.keyPressed = false;
            }
            if (ClickedButton.rightButtonDown || Input.GetKeyDown(KeyCode.N))
            {
                canvas.enabled = false;
                MissionManager.keyPressed = false;
                Time.timeScale = 1.0f;
            }
        }
    }
}
