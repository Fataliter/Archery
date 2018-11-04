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
            MissionManager.shootTargetCount++;
            MissionManager.hit = true;
            ScoreBrowser.hit = true;
            ProgressDuringMission.hit = true;
            ProgressDuringMission.targetName = "Target";
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy1")
        {
            if (RespawnMission2.banditLife == 0)
            {
                MissionManager.enemy1Count++;
                MissionManager.hit = true;
                ProgressDuringMission.hit = true;
                ProgressDuringMission.targetName = "Enemy1";
                RespawnBandits.kill = true;
            }
            ScoreBrowser.hit = true;
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy2")
        {
            MissionManager.enemy2Count++;
            MissionManager.hit = true;
            ScoreBrowser.hit = true;
            ProgressDuringMission.hit = true;
            ProgressDuringMission.targetName = "Enemy2";
            //Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy3")
        {
            MissionManager.enemy3Count++;
            MissionManager.hit = true;
            ScoreBrowser.hit = true;
            ProgressDuringMission.hit = true;
            ProgressDuringMission.targetName = "Enemy3";
            //Destroy(collision.gameObject);
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
