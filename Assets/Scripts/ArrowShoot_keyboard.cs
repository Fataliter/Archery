using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowShoot_keyboard : MonoBehaviour
{
    public float ForceValue = 0f;
    private bool keypressed = false;
    public GameObject arrow;
    float forcefactor = 100f;
    Transform Arrowplace;
    private bool forcemax = false;
    private bool zoom = false;
    private float waitzoomout = 0f;
    private float shootangle;
    public Image power;

    private Animator anim;


    private void Start()
    {
        PlayerRotation_keyboard.canRotateKeyboard = true;
        Arrowplace = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>();
        shootangle = Arrowplace.eulerAngles.z * Mathf.PI / 180;
        anim = GameObject.Find("bandit").GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        addingforce2();
        shoot();
        zoomout();
    }


    void addingforce2() //0, raising, 100, decrease, 0, repeat of cycle
    {
        if (Input.GetKey(KeyCode.Space) && ((anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) || keypressed==true))
       // if (Input.GetKey(KeyCode.Space))
        {
            
            zoomin();
            anim.SetBool("aimed", true);
            if (ForceValue <= 100 && forcemax == false && Camera.main.fieldOfView == 40)
            {
                ForceValue += (Time.deltaTime * 25);
            }
            else if (ForceValue > 100 || forcemax == true)
            {
                forcemax = true;
                ForceValue -= (Time.deltaTime * 25);
                if (ForceValue <= 0)
                    forcemax = false;
            }
            keypressed = true;
            power.fillAmount = ForceValue / 100f;
        }
        if (!Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("aimed", false);
            zoomout();
        }
    }

    void shoot() //instantiating arrow and shoot it
    {
        GameObject _arrow;
        if (keypressed == true && Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("ready", false);
            anim.SetBool("aimed", false);
            power.fillAmount = 0;
            if (ForceValue != 0)
            {
                _arrow = Instantiate(arrow, Arrowplace.transform.position, Arrowplace.transform.rotation) as GameObject;
                PlayerRotation_keyboard.canRotateKeyboard = false;
                _arrow.GetComponent<Rigidbody>().useGravity = true;
                _arrow.GetComponent<Rigidbody>().AddForce(transform.right * ForceValue * forcefactor * Mathf.Cos(shootangle));
                _arrow.GetComponent<Rigidbody>().AddForce(transform.up * ForceValue * forcefactor * Mathf.Sin(shootangle));
                if (Pillows.WindForce != 0f)
                    _arrow.GetComponent<Rigidbody>().AddForce(transform.forward * Pillows.WindForce * 10);
            }
            keypressed = false;
            ForceValue = 0;
            zoom = true;
            waitzoomout = 0f;
        }
    }

    void zoomin() //camera zoom in
    {
        if (Camera.main.fieldOfView > 40)
        {
            Camera.main.fieldOfView -= Time.deltaTime * 20 * 10 / 2;
        }
        if (Camera.main.fieldOfView <= 40)
        {
            Camera.main.fieldOfView = 40;
            anim.SetBool("ready", true);
        }
    }

    void zoomout() //camera zoom out
    {
        waitzoomout += Time.deltaTime;
        if (Camera.main.fieldOfView < 60 && zoom == true && waitzoomout >= .8f)
        {
            Camera.main.fieldOfView += Time.deltaTime * 20 * 10 / 6;
        }
        if (Camera.main.fieldOfView >= 60)
        {
            Camera.main.fieldOfView = 60;
            zoom = false;
        }
    }


}
