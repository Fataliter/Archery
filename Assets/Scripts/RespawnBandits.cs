﻿using System.Collections;
using UnityEngine;

public class RespawnBandits : MonoBehaviour {
    public static bool kill;
    bool canPlayAttack = true;
    Transform player;
    Animator animator;
    AnimatorClipInfo[] currentClipInfo;
    AudioSource audioSrc;
    public AudioClip dieFX;
    float moveSpeed;
    public static int banditLife;

	void Start () {
        kill = false;
        moveSpeed = PersistentManagerScript.Instance.config["general"]["banditMoveSpeed"].FloatValue;
        audioSrc = GetComponent<AudioSource>();
        banditLife = 2;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = gameObject.GetComponent<Animator>();
        currentClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
    }
	
	void Update () {
        gameObject.transform.LookAt(player);
        currentClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
        float distance = Vector3.Distance(gameObject.transform.position, player.position);
        if (currentClipInfo.Length > 0)
        {
            if (currentClipInfo[0].clip.name == "Walk" && distance > 10)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }

        if (distance < 11)
        {
            if (canPlayAttack == true)
            {
                animator.Play("Attack2");
                canPlayAttack = false;
            }
        }
        //Debug.Log(banditNoLife);
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "arrow" && (currentClipInfo[0].clip.name != "Matinee_sleep1" && currentClipInfo[0].clip.name != "Airborne_Down"))
        {
            banditLife--;
            animator.SetBool("airborneDown", true);
            if (banditLife == 0)
            {
                MissionManager.enemy1Count++;
                MissionManager.hit = true;
                ProgressDuringMission.hit = true;
                ProgressDuringMission.targetName = "Enemy1";
                kill = true;
                audioSrc.clip = dieFX;
                audioSrc.Play();
            }
            else audioSrc.Play();
        }
    }

    void BanditDeath()
    {
        if (kill == true)
        {
            Destroy(gameObject);
            kill = false;
        }
        else
            canPlayAttack = true;
    }

    void StopAirborneDown()
    {
        animator.SetBool("airborneDown", false);
    }
}