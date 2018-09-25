using UnityEngine;
using System.Collections;

public class PociskDetonacja : MonoBehaviour
{
    public static int banditsLife = 3;

    public static float points;
    public GameObject target;
    public ParticleSystem particleCommonHit, particleCommonHit2;
    ParticleSystem particleCommonHitPrivate;
    Rigidbody ArrowRB;
    float xvelocity;
    float yvelocity;
    float zvelocity;
    float rotateangle;

    void Start()
    {
        ArrowRB = this.gameObject.GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        rotationOfArrow();
    }

    void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        PlayerRotation_keyboard.canRotateKeyboard = true;
        PlayerRotation.canRotateSlider = true;
        if (RespawnTarget.hitCounter < 20)
        {
            Vector3 punkt = collision.contacts[0].point;
            Vector3 tarcza = target.transform.position;
            tarcza.x = tarcza.x + RespawnTarget.xOff;
            tarcza.y = tarcza.y - 0.1f;
            tarcza.z = tarcza.z + RespawnTarget.zOff;
            Destroy(gameObject);
            float dist = Vector3.Distance(punkt, tarcza);
            if (dist <= 0.35)
                GivePoints("5", tarcza);
            else if (dist <= 0.7)
                GivePoints("4", tarcza);
            else if (dist <= 1.5)
                GivePoints("3", tarcza);
            else if (dist <= 2.3)
                GivePoints("2", tarcza);
            else if (dist <= 2.7)
                GivePoints("1", tarcza);
            else
            {
                Debug.Log("Nie zdobyles nic");
            }
        }
        else
        {
            Destroy(gameObject);
            if (collision.collider.tag == "Bandit")
            {
                if (banditsLife == 1)
                {
                    RespawnTarget.ifDestroy = true;
                }
            }
        }
    }

    void GivePoints(string pointsFromTarget, Vector3 tarcza)
    {
        PersistentManagerScript.Instance.data.points += pointsFromTarget + ",";
        RespawnTarget.hitCounter++;
        RespawnTarget.ifDestroy = true;
        points = float.Parse(pointsFromTarget);
        if (RespawnTarget.hitCounter <= 10)
            PlayParticles(particleCommonHit, tarcza);
        else if (RespawnTarget.hitCounter <= 20)
            PlayParticles(particleCommonHit2, tarcza);
    }

    void PlayParticles(ParticleSystem particle, Vector3 tarcza)
    {
        if (particleCommonHitPrivate == null)
            particleCommonHitPrivate = Instantiate(particle, tarcza, Quaternion.identity);
        particleCommonHitPrivate.transform.position = tarcza;
        particleCommonHitPrivate.startColor = new Color(Random.value, Random.value, Random.value, 1f);
        particleCommonHitPrivate.Play();
    }

    void rotationOfArrow() //liczenie kątu obrotu strzały na podstawie jej współrzędnych wektora prędkości
    {
        if (this.gameObject.GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            xvelocity = ArrowRB.velocity.x;
            yvelocity = ArrowRB.velocity.y;
            zvelocity = ArrowRB.velocity.z;
            float _combinedvelocity = Mathf.Sqrt(xvelocity * xvelocity + zvelocity * zvelocity);
            rotateangle = Mathf.Atan2(yvelocity, _combinedvelocity) * 180 / Mathf.PI;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotateangle);
        }
    }
}
