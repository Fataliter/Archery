using UnityEngine;
using System.Collections;

public class PociskDetonacjaTraining : MonoBehaviour
{
    public ParticleSystem particleCommonHit, particleCommonHit2;
    ParticleSystem particleCommonHitPrivate;

    public GameObject target;
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
        audioSrc.clip = clipOnHit;
        audioSrc.Play();

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;

        Vector3 punkt = collision.contacts[0].point;
        Vector3 tarcza = target.transform.position;
        tarcza.x = tarcza.x + RespawnTargetTraining.xOff;
        tarcza.y = tarcza.y - 0.1f;
        tarcza.z = tarcza.z + RespawnTargetTraining.zOff;
        PlayerRotation.canRotateSlider = true;
        PlayerRotation_keyboard.canRotateKeyboard = true;
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
            Debug.Log("Nie zdobyles nic!!!");
        }
    }

    void GivePoints(string pointsFromTarget, Vector3 tarcza)
    {
        PersistentManagerScript.Instance.data.points += pointsFromTarget + ",";
        RespawnTargetTraining.ifDestroy = true;
        int random = Random.Range(0, 2);
        if (random == 1)
            PlayParticles(particleCommonHit, tarcza);
        else
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
