using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    public ParticleSystem particleCommonHit, particleCommonHit2;
    ParticleSystem particleCommonHitPrivate;
    
    Rigidbody ArrowRB;
    float xVelocity;
    float yVelocity;
    float zVelocity;
    float rotateangle;

    AudioSource audioSrc;
    public AudioClip clipOnHit;

    void Start () {
        ArrowRB = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
    }
	
	void FixedUpdate () {
        rotationOfArrow();
	}

    void OnCollisionEnter(Collision collision)
    {
        ArrowRB.velocity = Vector3.zero;
        ArrowRB.useGravity = false;
        ArrowRB.isKinematic = true;
        PlayerRotation_keyboard.canRotateKeyboard = true;
        PlayerRotation.canRotateSlider = true;
        if (collision.gameObject.tag == "Target")
        {
            TakeParticles(collision.gameObject.transform.position);
            MissionManager.shootTargetCount++;
            //Destroy(collision.gameObject);
            Destroy(gameObject);
            RespawnTarget.ifDestroy = true; //do zmiany
            RespawnTarget.hitCounter++;  //do zmiany
        }
        else if (collision.gameObject.tag == "Enemy1")
        {
            MissionManager.enemy1Count++;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy2")
        {
            MissionManager.enemy2Count++;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy3")
        {
            MissionManager.enemy3Count++;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("PUDŁO!!!");
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
        if (ArrowRB.velocity != Vector3.zero)
        {
            xVelocity = ArrowRB.velocity.x;
            yVelocity = ArrowRB.velocity.y;
            zVelocity = ArrowRB.velocity.z;
            float combinedVelocity = Mathf.Sqrt(xVelocity * xVelocity + zVelocity * zVelocity);
            rotateangle = Mathf.Atan2(yVelocity, combinedVelocity) * 180 / Mathf.PI;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotateangle);
        }
    }
}
