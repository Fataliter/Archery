using System.Collections;
using UnityEngine;

public class RespawnBandits : MonoBehaviour {
    public static bool kill = false;
    bool canPlayAttack = true;
    Transform player;
    Animator animator;
    AnimatorClipInfo[] currentClipInfo;

	void Start () {
        RespawnMission2.banditLife = 2;
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
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.position, 3 * Time.deltaTime);
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
    }

    void DestroyBandit()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "arrow" && (currentClipInfo[0].clip.name != "Matinee_sleep1" && currentClipInfo[0].clip.name != "Airborne_Down"))
        {
            animator.SetBool("airborneDown", true);
            RespawnMission2.banditLife--;
        }
    }

    void BanditDeath()
    {
        if (kill == true)
        {
            DestroyBandit();
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
