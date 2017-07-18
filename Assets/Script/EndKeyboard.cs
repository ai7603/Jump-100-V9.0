using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class EndKeyboard : MonoBehaviour {

    public AudioSource change_audio;
    public AudioSource click_audio;

	public Transform UIButtonOne;
	public Transform UIButtonTwo;

	public Transform UIImgOne;
	public Transform UIImgTwo;

	private int ButtonId;
    private bool IsButtonPressed;
    private int aux_counter;

    private GameObject SHADE;
    private GameObject REAL_SHADE;

	// Use this for initialization
	void Start () {

        UserInfo.IsPlayGame = false;

		ButtonId = 0;
        aux_counter = 0;
        IsButtonPressed = false;

        SHADE = (GameObject)Resources.Load("Prefab/shadow2");
    }

	// Update is called once per frame
	void Update () {

        if (IsButtonPressed) {
            if (aux_counter == 60) {

                if (ButtonId == 0)
                {
                    SceneManager.LoadScene("scene1");
                }
                else {
                    SceneManager.LoadScene("Main");
                }
            }

            aux_counter++;
        }


		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
            change_audio.Play();

            ButtonId = ButtonId == 0 ? 1 : 0;
           
		} else if (Input.GetKeyUp (KeyCode.RightArrow)) {
            change_audio.Play();
            ButtonId = ButtonId == 0 ? 1 : 0;
        } else if (Input.GetKeyUp (KeyCode.UpArrow)) {
            change_audio.Play();
            ButtonId = ButtonId == 0 ? 1 : 0;
        } else if (Input.GetKeyUp (KeyCode.DownArrow)) {
            change_audio.Play();
            ButtonId = ButtonId == 0 ? 1 : 0;
        } else if (Input.GetKey (KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space)) {
			
			switch (ButtonId) {
			case 0:
                    // SceneManager.LoadScene ("scene1");
                patchOne();
              
				break;
			case 1:
                    // SceneManager.LoadScene ("Main");
                patchTwo();
				break;
			default:
				break;	
			}
		}

		switch (ButtonId) {
		case 0:
			UIImgOne.gameObject.SetActive (true);
			UIImgTwo.gameObject.SetActive (false);
			break;
		case 1:
			UIImgOne.gameObject.SetActive (false);
			UIImgTwo.gameObject.SetActive (true);
			break;
		}


       
	}

    public void patchOne() {
        // SceneManager.LoadScene("scene1");
        click_audio.Play();
        REAL_SHADE = Instantiate(SHADE) as GameObject;
        REAL_SHADE.transform.position = new Vector3(-2.2f, -2.6f, 0.0f);
        REAL_SHADE.transform.localScale = new Vector3(1000.0f, 1000.0f, 1000.0f);
        ButtonId = 0;
        IsButtonPressed = true;
    }

    public void patchTwo() {

        click_audio.Play();
        REAL_SHADE = Instantiate(SHADE) as GameObject;
        REAL_SHADE.transform.position = new Vector3(-2.2f, -2.6f, 0.0f);
        REAL_SHADE.transform.localScale = new Vector3(1000.0f, 1000.0f, 1000.0f);
        ButtonId = 1;
        IsButtonPressed = true;
    }
}
