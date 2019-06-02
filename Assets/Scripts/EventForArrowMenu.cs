using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventForArrowMenu : MonoBehaviour {

	

	public void PlayFalse()
	{ 
		gameObject.GetComponent<Animator>().SetBool("play",false);
	}
}
