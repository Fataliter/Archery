using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour {

    ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    void Update () {
        if (!particle.IsAlive()) Destroy(gameObject);
	}
}
