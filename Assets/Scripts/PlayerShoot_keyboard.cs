using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot_keyboard : MonoBehaviour
{
    public float forceValue = 0f;
    private bool keypressed = false;
    public GameObject arrow;
    float forcefactor = 100f;
    Transform arrowplace;
    private bool forcemax = false;
    private bool zoom = false;
    private float waitzoomout = 0f;
    private float shootangle;
    public Image power;

    private Animator anim;

    private void Awake()
    {
        if (PersistentManagerScript.Instance.config["general"]["keyboardSteerPlayer"].IntValue == 1)
            this.enabled = true;
        else
            this.enabled = false;
    }


    private void Start()
    {
        PlayerRotation_keyboard.canRotateKeyboard = true;
        arrowplace = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>();
        shootangle = PersistentManagerScript.Instance.config["general"]["arrowPositionAngle"].FloatValue * Mathf.PI / 180;
        anim = GameObject.Find("bandit").GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        AddingForce();
        Shoot();
        ZoomOut();
    }


    void AddingForce() //0, raising, 100, decrease, 0, repeat of cycle
    {
        if (Input.GetKey(KeyCode.Space) && ((anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) || keypressed==true))
       // if (Input.GetKey(KeyCode.Space))
        {
            
            ZoomIn();
            anim.SetBool("aimed", true);
            if (forceValue <= 100 && forcemax == false && Camera.main.fieldOfView == 40)
            {
                forceValue += (Time.deltaTime * 25);
            }
            else if (forceValue > 100 || forcemax == true)
            {
                forcemax = true;
                forceValue -= (Time.deltaTime * 25);
                if (forceValue <= 0)
                    forcemax = false;
            }
            keypressed = true;
            power.fillAmount = forceValue / 100f;
        }
        if (!Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("aimed", false);
            ZoomOut();
        }
    }

    void Shoot() //instantiating arrow and shoot it
    {
        GameObject _arrow;
        if (keypressed == true && Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("ready", false);
            anim.SetBool("aimed", false);
            power.fillAmount = 0;
            if (forceValue != 0)
            {
                _arrow = Instantiate(arrow, arrowplace.transform.position, arrowplace.transform.rotation) as GameObject;
                PlayerRotation_keyboard.canRotateKeyboard = false;
                _arrow.GetComponent<Rigidbody>().useGravity = true;
                _arrow.GetComponent<Rigidbody>().AddForce(transform.forward * forceValue * forcefactor * Mathf.Cos(shootangle));
                _arrow.GetComponent<Rigidbody>().AddForce(transform.up * forceValue * forcefactor * Mathf.Sin(shootangle));
            }
            keypressed = false;
            forceValue = 0;
            zoom = true;
            waitzoomout = 0f;
        }
    }

    void ZoomIn() //camera zoom in
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

    void ZoomOut() //camera zoom out
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
