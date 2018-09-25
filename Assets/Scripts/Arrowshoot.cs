using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrowshoot : MonoBehaviour
{

    public float ForceValue = 0f;
    public static bool keypressed = false;
    public GameObject arrow;    //zmienna przechowująca obiekt macierzysty dla strzały
    private Transform Arrowplace;   //zmienna pozycji strzały wystrzelanej
    private float forcefactor = 100f;
    private bool forcemax = false;
    private bool zoom = false;
    private float waitzoomout = 0f;
    private float shootangle;

    private float LeftLeg;
    private float RightLeg;
    private byte RightButton;
    public Image power;

    private Animator anim;

    
    private void Start()
    {
        PlayerRotation.canRotateSlider = true;
        Arrowplace = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>(); // pobranie komponentu pozycji obiektu "ArrowPosition"
        shootangle = Arrowplace.eulerAngles.z * Mathf.PI / 180;
        anim = GameObject.Find("bandit").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Parameters();
        AddingForce2();
        Shoot();
        ZoomOut();
    }

    void Parameters() //parametry pobierane ze skryptu nasłuchującego aplikacje serwera zintegrowaną z interpreterem pionizatora
    {
        LeftLeg = PersistentManagerScript.Instance.mydata.LeftLeg;
        RightLeg = PersistentManagerScript.Instance.mydata.RightLeg;
        RightButton = (byte)PersistentManagerScript.Instance.mydata.RightButton;
    }

    void addingforce1() //siła strzału (naciągnięcia cięciwy) = 0, rośnie, osiąga 100 i od razu powraca do 0, powtórzenie cyklu
    {
        if (RightButton == 0 && Mathf.Abs(LeftLeg - RightLeg) < 10)   //test
        {
            ZoomIn();
            if (Camera.main.fieldOfView == 40)
            {
                if (ForceValue > 100)
                    ForceValue = 0;
                else
                    ForceValue += (Time.deltaTime * 25);
            }
            keypressed = true;
        }
    }

    void AddingForce2() //siła strzału (naciągnięcia cięciwy) = 0, rośnie, osiąga 100, maleje, osiąga 0, powtórzenie cyklu
    {
        if (RightButton == 0 && ((Mathf.Abs(LeftLeg - RightLeg) < 10 && (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))) || keypressed == true))   
        {
            anim.SetBool("aimed", true);
            ZoomIn();
            if (ForceValue <= 100 && forcemax == false && Camera.main.fieldOfView == 40)
            {
                ForceValue += (Time.deltaTime * 25);    //ForceValue=100 osiągamy po 4s
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
        if (RightButton==1)
        {
            anim.SetBool("aimed", false);
            ZoomOut();
        }
    }

    void Shoot() //utworzenie pojedyńczej strzały i wystrzelenie jej
    {
        GameObject _arrow;  //zmienna sterująca pojedynczą strzała, która zostaje utworzona tuż po puszczeniu prawego przycisku
        if (keypressed == true && RightButton != 0) //test
        {
            anim.SetBool("ready", false);
            anim.SetBool("aimed", false);
            power.fillAmount = 0;
            _arrow = Instantiate(arrow, Arrowplace.transform.position, Arrowplace.transform.rotation) as GameObject;    //utworzenie strzały
            PlayerRotation.canRotateSlider = false;
            _arrow.GetComponent<Rigidbody>().useGravity = true;
            _arrow.GetComponent<Rigidbody>().AddForce(transform.right * ForceValue * forcefactor * Mathf.Cos(shootangle));
            _arrow.GetComponent<Rigidbody>().AddForce(transform.up * ForceValue * forcefactor * Mathf.Sin(shootangle));  //wystrzelenie
            if (Pillows.WindForce != 0f)
                _arrow.GetComponent<Rigidbody>().AddForce(transform.forward * Pillows.WindForce);  //uwzględnienie wiatru (utrudnienie stzrelania zintegrowane z poduszkami)
            keypressed = false;
            ForceValue = 0;
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
