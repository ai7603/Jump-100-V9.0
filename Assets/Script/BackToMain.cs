using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class BackToMain : MonoBehaviour {
    public AudioSource click_audio;

    private bool IsButtonPressed;
    private int aux_counter;
    private GameObject SHADE;
    private GameObject REAL_SHADE;
    // Use this for initialization
    void Start () {
        IsButtonPressed = false;
        aux_counter = 0;

        SHADE = (GameObject)Resources.Load("Prefab/shadow2");
    }
	
	// Update is called once per frame
	void Update () {
        if (IsButtonPressed) {
            if (aux_counter == 60)
            {
                SceneManager.LoadScene("Main");
            }
            aux_counter++;
        }
	}

	public void backtomain(){
        click_audio.Play();

        IsButtonPressed = true;
        REAL_SHADE = Instantiate(SHADE) as GameObject;
        REAL_SHADE.transform.position = new Vector3(-1.5f, -3.2f, -2.82f);
        REAL_SHADE.transform.localScale = new Vector3(1000.0f, 1000.0f, 1000.0f);
        // SceneManager.LoadScene ("Main");
    }
}
