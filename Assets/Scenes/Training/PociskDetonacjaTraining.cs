using UnityEngine;
using System.Collections;

public class PociskDetonacjaTraining : MonoBehaviour
{
    public GameObject target;
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

        Vector3 punkt = collision.contacts[0].point;
        Vector3 tarcza = target.transform.position;
        tarcza.x = tarcza.x + RespawnTargetTraining.xOff;
        tarcza.y = tarcza.y - 0.1f;
        tarcza.z = tarcza.z + RespawnTargetTraining.zOff;
        Destroy(gameObject);
        float dist = Vector3.Distance(punkt, tarcza);
        if (dist <= 0.35)
        {
            PersistentManagerScript.Instance.data.points += "5,";
            RespawnTargetTraining.ifDestroy = true;
        }
        else if (dist <= 0.7)
        {
            PersistentManagerScript.Instance.data.points += "4,";
            RespawnTargetTraining.ifDestroy = true;
        }
        else if (dist <= 1.5)
        {
            PersistentManagerScript.Instance.data.points += "3,";
            RespawnTargetTraining.ifDestroy = true;
        }
        else if (dist <= 2.3)
        {
            PersistentManagerScript.Instance.data.points += "2,";
            RespawnTargetTraining.ifDestroy = true;
        }
        else if (dist <= 2.7)
        {
            PersistentManagerScript.Instance.data.points += "1,";
            RespawnTargetTraining.ifDestroy = true;
        }
        else
        {
            Debug.Log("Nie zdobyles nic!!!");
        }
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
