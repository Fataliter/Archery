using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour {

    SkinnedMeshRenderer GO;

    private void Awake()
    {
        GO = GameObject.FindGameObjectWithTag("ArrowAnim").GetComponent<SkinnedMeshRenderer>();
        GO.enabled = true;
    }

    void Disappear()
    {
        GO.enabled = false;
    }

    void Appear()
    {
        GO.enabled = true;
    }
	
}
