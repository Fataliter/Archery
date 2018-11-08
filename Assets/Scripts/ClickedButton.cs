using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedButton : MonoBehaviour {

    private static ClickedButton Instance;
    byte leftButton;
    byte rightButton;
    byte leftButtonBefore;
    byte rightButtonBefore;
    public static bool leftButtonDown { get; private set; }
    public static bool rightButtonDown { get; private set; }

    void Start () {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Parameters();
        leftButtonBefore = 0;
        rightButtonBefore = 0;
    }

	void Update () {
        Parameters();
        SetButtonsDown();
        SetBeforeButtons();
	}

    void Parameters()
    {
        leftButton = (byte)PersistentManagerScript.Instance.mydata.LeftButton;
        rightButton = (byte)PersistentManagerScript.Instance.mydata.RightButton;
    }

    void SetButtonsDown()
    {
        if (leftButtonBefore == 1 && leftButton == 0) leftButtonDown = true;
        else leftButtonDown = false;
        if (rightButtonBefore == 1 && rightButton == 0) rightButtonDown = true;
        else rightButtonDown = false;
    }

    void SetBeforeButtons()
    {
        rightButtonBefore = rightButton;
        leftButtonBefore = leftButton;
    }

}
