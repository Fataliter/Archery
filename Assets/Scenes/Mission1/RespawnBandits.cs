using System.Collections;
using UnityEngine;

public class RespawnBandits : MonoBehaviour {

    Transform player;
    Animator animator;
    AnimatorClipInfo[] currentClipInfo;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = gameObject.GetComponent<Animator>();
	}
	
	void Update () {
        gameObject.transform.LookAt(player);
        currentClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
        if (currentClipInfo[0].clip.name == "Walk")
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.position, 3 * Time.deltaTime);
        }

        if (Vector3.Distance(gameObject.transform.position, player.position) < 10)
        {
            animator.Play("Attack2");
        }
	}

    void DestroyBandit()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "arrow" && (currentClipInfo[0].clip.name == "Walk" || currentClipInfo[0].clip.name == "Attack2"))
        {
            animator.SetBool("airborneDown", true);
            StartCoroutine("OnHitAnimation");
        }
    }

    IEnumerator OnHitAnimation()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && !animator.IsInTransition(0))
            yield return null;

        PociskDetonacja.banditsLife--;
        animator.SetBool("airborneDown", false);
        if (PociskDetonacja.banditsLife == 0)
        {
            DestroyBandit();
            RespawnTarget.hitCounter++;
            PociskDetonacja.banditsLife = 3;
        }
    }
}
