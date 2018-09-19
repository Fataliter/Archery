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
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.position, 2 * Time.deltaTime);
        }
	}

    void DestroyBandit()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "arrow" && currentClipInfo[0].clip.name == "Walk")
        {
            animator.SetBool("airborneDown", true);
            StartCoroutine("OnHitAnimation");
        }
    }

    IEnumerator OnHitAnimation()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && !animator.IsInTransition(0))
            yield return null;

        PociskDetonacja.banditsLife--;
        if (PociskDetonacja.banditsLife == 0)
        {
            DestroyBandit();
            RespawnTarget.hitCounter++;
            PociskDetonacja.banditsLife = 3;
        }
        animator.SetBool("airborneDown", false);
    }
}
