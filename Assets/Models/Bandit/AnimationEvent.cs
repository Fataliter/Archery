using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour {

    SkinnedMeshRenderer GO;
    AudioSource audioSrc;

    private void Awake()
    {
        GO = GameObject.FindGameObjectWithTag("ArrowAnim").GetComponent<SkinnedMeshRenderer>();
        GO.enabled = true;
        audioSrc = GameObject.Find("PlayerAudio").GetComponent<AudioSource>();
    }

    void Disappear()
    {
        GO.enabled = false;
    }

    void Appear()
    {
        GO.enabled = true;
    }

    void SoundEvent()
    {
        audioSrc.Play();
    }
}
