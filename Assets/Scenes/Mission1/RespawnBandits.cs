using System.Collections;
using UnityEngine;

public class RespawnBandits : MonoBehaviour {
    public static bool destroyBandit = false;
    public static int banditsLife = 3;

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
        if (destroyBandit)
        {
            DestroyBandit();
        }
	}

    void DestroyBandit()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "arrow")
        {
            animator.SetBool("airborneDown", true);
            StartCoroutine("OnHitAnimation");
        }
    }

    IEnumerator OnHitAnimation()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;
        Debug.Log("hi");
        animator.SetBool("airborneDown", false);
    }
}
