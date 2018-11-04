using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndMissionParticles : MonoBehaviour {
    public ParticleSystem fireworks;
    ParticleSystem fireworksInst;
    Transform player;

    float timer = 0f;

	void Start () {
        PlayerRotation_keyboard.canRotateKeyboard = false;
        PlayerRotation.canRotateSlider = false;

        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Transform>();

        Vector3 pos = new Vector3(player.position.x, 0f, player.position.z + 50f);
        if (fireworksInst == null)
            fireworksInst = Instantiate(fireworks, pos, Quaternion.identity);
        fireworksInst.transform.position = pos;
        Vector3 rotation = fireworksInst.transform.localEulerAngles;
        rotation.x += -90f;
        fireworksInst.transform.localEulerAngles = rotation;
        fireworksInst.Play();
        fireworksInst.Play();
	}
	
	void Update () {
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(-45, 0, 0), Time.deltaTime * 0.3f);
        timer += Time.deltaTime;
        if (timer > 15f)
            SceneManager.LoadScene("MenuMedievalMissionChoice");
	}
}
