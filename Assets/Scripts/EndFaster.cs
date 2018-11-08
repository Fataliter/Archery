using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFaster : MonoBehaviour {

    Canvas canvas;
    byte leftButton;
    byte rightButton;
    byte leftButtonBefore;
    byte rightButtonBefore;
    
	void Start ()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        leftButtonBefore = leftButton;
        rightButtonBefore = rightButton;
    }
	
	void Update ()
    {
        Parameters();
        ShowCanvas();
        SetBeforeButtons();

	}

    void Parameters()
    {
        leftButton = (byte)PersistentManagerScript.Instance.mydata.LeftButton;
        rightButton = (byte)PersistentManagerScript.Instance.mydata.RightButton;
    }

    bool LeftButtonDown()
    {
        if (leftButtonBefore == 1 && leftButton == 0) return true;
        else return false;
    }

    bool RightButtonDown()
    {
        if (rightButtonBefore == 1 && rightButton == 0) return true;
        else return false;
    }

    void SetBeforeButtons()
    {
        rightButtonBefore = rightButton;
        leftButtonBefore = leftButton;
    }

    void ShowCanvas()
    {
        if (MissionManager.keyPressed == true)
        {
            canvas.enabled = true;
            Time.timeScale = 0.0f;
            if (LeftButtonDown() || Input.GetKeyDown(KeyCode.Y))
            {
                Time.timeScale = 1.0f;
                MissionManager.endGameFaster = true;
                MissionManager.keyPressed = false;
            }
            if (RightButtonDown() || Input.GetKeyDown(KeyCode.N))
            {
                canvas.enabled = false;
                MissionManager.keyPressed = false;
                Time.timeScale = 1.0f;
            }
        }
    }
}
