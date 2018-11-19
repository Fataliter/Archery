using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnDragon : MonoBehaviour {

    int dragonLife;
    Transform player;
    float moveSpeed;

    AudioSource audioSrc;
    public AudioClip landFX, idleFX, runFX, attackFX, hitFX, breathFX, dieFX;


    Animator anim;
    AnimatorClipInfo[] currentClipInfo;

    Vector3 stompPos = new Vector3(200, 0, 210); //(200, 0, 20); <-- misja 2
    float stompDist;

    GameObject dragonMaw;
    ParticleSystem particle;
    Vector3 breathPos = new Vector3(200, 0, 190); //(200, 0, 0); <-- misja 2
    float breathDist;
    bool oneTime;

    int dragonHits;

    void Start() {
        dragonMaw = GameObject.FindGameObjectWithTag("DragonMaw");
        particle = dragonMaw.GetComponent<ParticleSystem>();
        dragonLife = 3;
        moveSpeed = PersistentManagerScript.Instance.config["general"]["dragonMoveSpeed"].FloatValue;
        //moveSpeed = 4f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        currentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        stompDist = Vector3.Distance(gameObject.transform.position, stompPos);
        audioSrc = GetComponent<AudioSource>();
    }

    void Update() {
        currentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        if (anim.GetBool("stomp") == true && anim.GetBool("instantiated") == true)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, stompPos, stompDist * Time.deltaTime / 2.7f);
        }
        transform.LookAt(player);
        float distance = Vector3.Distance(player.position, transform.position);
        if (currentClipInfo[0].clip.name == "run" && distance > 14)
        {
            particle.Stop();
            dragonHits = 0;
            oneTime = true;
            anim.SetBool("dragonBreath", false);
            anim.SetBool("instantiated", false);
            anim.SetBool("backToRun", true);
            anim.SetBool("backToAttack", false);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        if (distance < 15)
        {
            anim.SetBool("backToRun", false);
            anim.SetBool("backToAttack", true);
            anim.SetBool("isClose", true);
        }
        if (currentClipInfo[0].clip.name == "die")
            anim.SetBool("noHealth", false);
        if (currentClipInfo[0].clip.name == "hit")
            anim.SetBool("hit", false);
        if (dragonHits == 5)
        {
            if (oneTime)
            {
                breathDist = Vector3.Distance(transform.position, breathPos);
                anim.SetBool("stomp", true);
                oneTime = false;
            }
            anim.SetBool("dragonBreath", true);
            anim.SetBool("isClose", false);
            if (anim.GetBool("stomp") == true)
                transform.position = Vector3.MoveTowards(transform.position, breathPos, breathDist * Time.deltaTime / 2.7f);
            
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "arrow" && (currentClipInfo[0].clip.name == "run" || currentClipInfo[0].clip.name == "atk01"))
        {
            dragonLife--;
            Debug.Log(dragonLife);
            if (dragonLife == 0)
            {
                gameObject.GetComponent<MeshCollider>().enabled = false;
                anim.SetBool("noHealth", true);
                MissionManager.enemy3Count++;  //comment dla etstu w innej misji
                MissionManager.hit = true;
                ProgressDuringMission.hit = true;
                ProgressDuringMission.targetName = "Enemy3"; //comment dla testu w innej misji
            }
            else
            {
                anim.SetBool("hit", true);
            }

        }
    }

    void DragonStomped()
    {
        anim.SetBool("stomp", false);
    }

    void KillDragon()
    {
        Destroy(gameObject);
    }

    void DragonAttacked()
    {
        dragonHits++;
    }

    void DragonLanding()
    {
        audioSrc.clip = landFX;
        audioSrc.Play();
    }

    void DragonRoar()
    {
        audioSrc.clip = idleFX;
        audioSrc.Play();
    }

    void DragonMove()
    {
        audioSrc.clip = runFX;
        audioSrc.Play();
    }

    void DragonHit()
    {
        anim.SetBool("hit", false);
        audioSrc.clip = hitFX;
        audioSrc.Play();
    }

    void DragonAttacking()
    {
        audioSrc.clip = attackFX;
        audioSrc.Play();
    }

    void DragonBreath()
    {
        audioSrc.clip = breathFX;
        audioSrc.Play();
    }

    void DragonDie()
    {
        audioSrc.clip = dieFX;
        audioSrc.Play();
    }

    void DragonParticles()
    {
        particle.Play();
    }

}
