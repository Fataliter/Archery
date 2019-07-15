using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    public ParticleSystem particleCommonHit, particleCommonHit2;
    ParticleSystem particleCommonHitPrivate;
    
    Rigidbody arrowRB;
    float xVelocity;
    float yVelocity;
    float zVelocity;
    float rotateangle;
    

    AudioSource audioSrc;
    public AudioClip clipOnHit;

    void Start () {
        arrowRB = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
    }
	
	void FixedUpdate () {
        rotationOfArrow();
	}

    void OnCollisionEnter(Collision collision)
    {
        arrowRB.velocity = Vector3.zero;
        arrowRB.useGravity = false;
        arrowRB.isKinematic = true;
        PlayerRotation_keyboard.canRotateKeyboard = true;
        PlayerRotation.canRotateSlider = true;
        if (collision.gameObject.tag == "Target" )
        {
            TakeParticles(collision.gameObject.transform.position);
            if (MissionManager.keyboardSteerPlayer != 1 || (MissionManager.keyboardSteerPlayer == 1 && MissionManager.keyboardSteerSaveStatusJSON == 1))
                MissionManager.shootTargetCount++; //MUST HAVE
            MissionManager.hit = true; //MUST HAVE
            ScoreBrowser.hit = true; //MUST HAVE
            ProgressDuringMission.hit = true; //MUST HAVE
            ProgressDuringMission.targetName = "Target"; //MUST HAVE
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy1" || collision.gameObject.tag == "Enemy2" || collision.gameObject.tag == "Enemy3")
        {
            ScoreBrowser.hit = true;
            Destroy(gameObject);
        }
        else
        {
            audioSrc.clip = clipOnHit;
            audioSrc.Play();
            Destroy(gameObject, 1);
        }

    }

    void TakeParticles(Vector3 pos)
    {
        int random = Random.Range(0, 2);
        if (random == 1)
            PlayParticles(particleCommonHit, pos);
        else
            PlayParticles(particleCommonHit2, pos);
    }

    void PlayParticles(ParticleSystem particle, Vector3 pos)
    {
        if (particleCommonHitPrivate == null)
            particleCommonHitPrivate = Instantiate(particle, pos, Quaternion.identity);
        particleCommonHitPrivate.Play();
    }
    

    void rotationOfArrow() 
    {
        if (arrowRB.velocity != Vector3.zero)
        {
            xVelocity = arrowRB.velocity.x;
            yVelocity = arrowRB.velocity.y;
            zVelocity = arrowRB.velocity.z;
            float combinedVelocity = Mathf.Sqrt(xVelocity * xVelocity + zVelocity * zVelocity);
            rotateangle = Mathf.Atan2(yVelocity, combinedVelocity) * 180 / Mathf.PI;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotateangle);
        }
    }
}
