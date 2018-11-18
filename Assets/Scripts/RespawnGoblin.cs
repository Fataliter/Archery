using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGoblin : MonoBehaviour {

    int goblinLife;
    Transform player;
    float moveSpeed;

    Animator anim;
    AnimatorClipInfo[] currentClipInfo;

    AudioSource audioSrc;
    public AudioClip dieFX, attackFX, hitFX;


    void Start () {
        moveSpeed = PersistentManagerScript.Instance.config["general"]["goblinMoveSpeed"].FloatValue;
        goblinLife = 2;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = this.gameObject.GetComponent<Animator>();
        currentClipInfo = this.anim.GetCurrentAnimatorClipInfo(0);
        audioSrc = GetComponent<AudioSource>();

    }
	
	void Update () {
        currentClipInfo = this.anim.GetCurrentAnimatorClipInfo(0);
        transform.LookAt(player);
        float distance = Vector3.Distance(player.position, transform.position);
        if(currentClipInfo[0].clip.name == "Run" && distance > 10)
        {
            anim.SetBool("backToRun", true);
            anim.SetBool("backToAttack", false);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        if(distance<11)
        {
            anim.SetBool("backToRun", false);
            anim.SetBool("backToAttack", true);
            anim.SetBool("isClose", true);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "arrow" && (currentClipInfo[0].clip.name == "Run" || currentClipInfo[0].clip.name == "Attack"))
        {
            goblinLife--;
            anim.SetBool("hit", true);
            if (goblinLife == 0)
            {
                MissionManager.enemy2Count++;
                MissionManager.hit = true;
                ProgressDuringMission.hit = true;
                ProgressDuringMission.targetName = "Enemy2";
                audioSrc.clip = dieFX;
                audioSrc.Play();
            }
            else
            {
                audioSrc.clip = hitFX;
                audioSrc.Play();
            }
        }
    }

    void GoblinHitReset()
    {
        anim.SetBool("hit", false);
        if(goblinLife == 0)
        {
            anim.SetBool("backToRun", false);
            anim.SetBool("backToAttack", false);
            Destroy(gameObject,.7f);
        }
    }

    void GoblinAttack()
    {
        audioSrc.clip = attackFX;
        audioSrc.Play();
    }
}
