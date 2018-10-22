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

    AudioSource audioSrc;
    public AudioClip clipOnHit;

    void Start()
    {
        ArrowRB = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
    }


    void FixedUpdate()
    {
        rotationOfArrow();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (RespawnTarget.hitCounter < 30)
        {
            audioSrc.clip = clipOnHit;
            audioSrc.Play();
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        PlayerRotation_keyboard.canRotateKeyboard = true;
        PlayerRotation.canRotateSlider = true;
        if (RespawnTarget.hitCounter < 30)
        {
            Vector3 punkt = collision.contacts[0].point;
            Vector3 tarcza = target.transform.position;
            tarcza.x = tarcza.x + RespawnTarget.xOff;
            tarcza.y = tarcza.y - 0.1f;
            tarcza.z = tarcza.z + RespawnTarget.zOff;
            
            float dist = Vector3.Distance(punkt, tarcza);
            if (dist <= 2.7)
            {
                if (dist <= 0.35)
                    GivePoints("5", tarcza);
                else if (dist <= 0.7)
                    GivePoints("4", tarcza);
                else if (dist <= 1.5)
                    GivePoints("3", tarcza);
                else if (dist <= 2.3)
                    GivePoints("2", tarcza);
                else
                    GivePoints("1", tarcza);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject, 1);
                Debug.Log("Nie zdobyles nic");
            }
        }
        else
        {
            Destroy(gameObject, 1);
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
        if (RespawnTarget.hitCounter <= 15)
            PlayParticles(particleCommonHit, tarcza);
        else if (RespawnTarget.hitCounter <= 30)
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
