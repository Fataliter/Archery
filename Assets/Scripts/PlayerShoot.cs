using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{

    public float forceValue = 0f;
    public static bool keypressed = false;
    public GameObject arrow;
    private Transform arrowplace;  
    private float forcefactor = 100f;
    private bool forcemax = false;
    private bool zoom = false;
    private float waitzoomout = 0f;
    private float shootangle;


    int legsDiff;
    private float leftLeg;
    private float rightLeg;
    private byte rightButton;
    public Image power;

    private Animator anim;

    private void Awake()
    {
        if (PersistentManagerScript.Instance.config["general"]["keyboardSteerPlayer"].IntValue == 1)
            this.enabled = false;
        else
            this.enabled = true;
    }

    private void Start()
    {
        PlayerRotation.canRotateSlider = true;
        arrowplace = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>(); 
        shootangle = PersistentManagerScript.Instance.config["general"]["arrowPositionAngle"].FloatValue * Mathf.PI / 180;
        anim = GameObject.Find("bandit").GetComponent<Animator>();
        legsDiff = PersistentManagerScript.Instance.config["general"]["LegsDifferenceForRotation"].IntValue;
    }

    private void FixedUpdate()
    {
        Parameters();
        AddingForce();
        Shoot();
        ZoomOut();
    }

    void Parameters() 
    {
        leftLeg = PersistentManagerScript.Instance.mydata.LeftLeg;
        rightLeg = PersistentManagerScript.Instance.mydata.RightLeg;
        rightButton = (byte)PersistentManagerScript.Instance.mydata.RightButton;
    }
    

    void AddingForce() 
    {
        //if (rightButton == 0 && ((Mathf.Abs(leftLeg - rightLeg) < legsDiff && (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))) || keypressed == true))  //moze stac byle jak podczas ladowania siły strzału  
        if (rightButton == 0 && ((Mathf.Abs(leftLeg - rightLeg) < legsDiff && (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))) || (Mathf.Abs(leftLeg - rightLeg) < legsDiff && keypressed == true)))  //traci rownowage = strzela
        {
            anim.SetBool("aimed", true);
            ZoomIn();
            if (forceValue <= 100 && forcemax == false && Camera.main.fieldOfView == 40)
            {
                forceValue += (Time.deltaTime * 25);    //ForceValue=100 osiągamy po 4s
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
        if (rightButton==1)
        {
            anim.SetBool("aimed", false);
            ZoomOut();
        }
    }

    void Shoot() 
    {
        GameObject _arrow;
        //if (keypressed == true && rightButton != 0) //moze stac byle jak podczas ladowania siły strzału
        if ((keypressed == true && rightButton != 0) || (keypressed == true && Mathf.Abs(leftLeg - rightLeg) >= legsDiff)) //traci rownowage = strzela
        {
            anim.SetBool("ready", false);
            anim.SetBool("aimed", false);
            power.fillAmount = 0;
            if (forceValue != 0)
            {
                _arrow = Instantiate(arrow, arrowplace.transform.position, arrowplace.transform.rotation) as GameObject;  
                PlayerRotation.canRotateSlider = false;
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

    void ZoomIn() //przybliżenie kamery do strzelania
    {
        if (Camera.main.fieldOfView > 40)
        {
            Camera.main.fieldOfView -= Time.deltaTime * 20 * 10 / 2;    //przyblizenie trwa 0,2s
            // 20 - róznica pomiędzy fov (field of view) z oddalonej kamery do przyblizonej
            // 10/2 - mnożnik długości czasu trwania przybliżenia (odwrotność czasu przybliżania). Domyślnie Time.deltaTime*20 --> wartość przybliżenia zwiększona o 1 w czasie 1s.
        }
        if (Camera.main.fieldOfView <= 40)
        {
            Camera.main.fieldOfView = 40;
            anim.SetBool("ready", true);
        }
    }

    void ZoomOut() //oddalenie kamery
    {
        waitzoomout += Time.deltaTime;
        if (Camera.main.fieldOfView < 60 && zoom == true && waitzoomout >= .8f)
        {
            Camera.main.fieldOfView += Time.deltaTime * 20 * 10 / 6;       //oddalenie trwa 0,6s
        }
        if (Camera.main.fieldOfView >= 60)
        {
            Camera.main.fieldOfView = 60;
            zoom = false;
        }
    }
}
