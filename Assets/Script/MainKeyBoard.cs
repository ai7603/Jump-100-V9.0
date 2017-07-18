using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MainKeyBoard : MonoBehaviour {

    public AudioSource change_audio;
    public AudioSource click_audio;

	private int ButtonId;
    private bool IsButtonPressed;
    private int aux_counter;

    public GameObject SHADE;
    public GameObject REAL_SHADE;


	public Transform UIButtonOne;
	public Transform UIButtonTwo;
	public Transform UIButtonThree;
	public Transform UIButtonFour;
	public Transform UIButtonFive;

	public Transform UIPicOne;
	public Transform UIPicTwo;
	public Transform UIPicThree;
	public Transform UIPicFour;
	public Transform UIPicFive;
	// Use this for initialization
	void Start () {
		ButtonId = 0;
        IsButtonPressed = false;
        aux_counter = 0;

        SHADE = (GameObject)Resources.Load("Prefab/shadow2");
	}
	
	// Update is called once per frame
	void Update () {
        if (IsButtonPressed)
        {
            if (aux_counter == 60) {
                SwitchFromMainToOther(ButtonId);
            }
            aux_counter++;
        }

		// print (ButtonId);
		// 0 -- play, 1 -- logout, 2 -- store, 3 -- settings, 4 -- highest
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
            change_audio.Play();
            if (ButtonId == 0) ButtonId = 4;
            else ButtonId = ButtonId - 1;
		} else if (Input.GetKeyUp (KeyCode.RightArrow)) {
            change_audio.Play();
            if (ButtonId == 4) ButtonId = 0;
            else ButtonId = ButtonId + 1;
		} else if (Input.GetKeyUp (KeyCode.UpArrow)) {
            change_audio.Play();
            if (ButtonId == 0) ButtonId = 4;
            else ButtonId = ButtonId - 1;
        } else if (Input.GetKeyUp (KeyCode.DownArrow)) {
            change_audio.Play();
            if (ButtonId == 4) ButtonId = 0;
            else ButtonId = ButtonId + 1;
        } else if (Input.GetKey (KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space)) {
            click_audio.Play();
            IsButtonPressed = true;

            REAL_SHADE = Instantiate(SHADE) as GameObject;
            REAL_SHADE.transform.position = new Vector3(-0.2539f, 0.09f, -2.34f);
            REAL_SHADE.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
        }   

		switch (ButtonId) {
		case 0:
			UIPicOne.gameObject.SetActive (true);
			UIPicTwo.gameObject.SetActive (false);
			UIPicThree.gameObject.SetActive (false);
			UIPicFour.gameObject.SetActive (false);
			UIPicFive.gameObject.SetActive (false);
			break;
		case 1:
			UIPicOne.gameObject.SetActive (false);
			UIPicTwo.gameObject.SetActive (true);
			UIPicThree.gameObject.SetActive (false);
			UIPicFour.gameObject.SetActive (false);
			UIPicFive.gameObject.SetActive (false);
			break;
		case 2:
			UIPicOne.gameObject.SetActive (false);
			UIPicTwo.gameObject.SetActive (false);
			UIPicThree.gameObject.SetActive (true);
			UIPicFour.gameObject.SetActive (false);
			UIPicFive.gameObject.SetActive (false);
			break;
		case 3:
			UIPicOne.gameObject.SetActive (false);
			UIPicTwo.gameObject.SetActive (false);
			UIPicThree.gameObject.SetActive (false);
			UIPicFour.gameObject.SetActive (true);
			UIPicFive.gameObject.SetActive (false);
			break;
		case 4:
			UIPicOne.gameObject.SetActive (false);
			UIPicTwo.gameObject.SetActive (false);
			UIPicThree.gameObject.SetActive (false);
			UIPicFour.gameObject.SetActive (false);
			UIPicFive.gameObject.SetActive (true);
			break;
		}
	}

    private void SwitchFromMainToOther(int btnid) {
       // Destroy(REAL_SHADE);
        switch (btnid)
        {
            case 0:
                SceneManager.LoadScene("scene1");
                break;
            case 1:
                Application.Quit();
                break;
            case 2:
                SceneManager.LoadScene("Store");
                break;
            case 3:
                SceneManager.LoadScene("Settings");
                break;
            case 4:
                SceneManager.LoadScene("Highest");
                break;
            default:
                break;
        }
    }

	public void PatchOne(){
        // SceneManager.LoadScene ("scene1");
        IsButtonPressed = true;
        click_audio.Play();
        ButtonId = 0;
        REAL_SHADE = Instantiate(SHADE) as GameObject;
        REAL_SHADE.transform.position = new Vector3(-0.2539f, 0.09f, -2.34f);
        REAL_SHADE.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
    }

	public void PatchTwo(){
        // Application.Quit ();
        
        IsButtonPressed = true;
        click_audio.Play();
        ButtonId = 1;
        REAL_SHADE = Instantiate(SHADE) as GameObject;
        REAL_SHADE.transform.position = new Vector3(-0.2539f, 0.09f, -2.34f);
        REAL_SHADE.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
    }
	public void PatchThree(){
        // SceneManager.LoadScene ("Store");
        IsButtonPressed = true;
        click_audio.Play();
        ButtonId = 2;
        REAL_SHADE = Instantiate(SHADE) as GameObject;
        REAL_SHADE.transform.position = new Vector3(-0.2539f, 0.09f, -2.34f);
        REAL_SHADE.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
    }
	public void PatchFour(){
        // SceneManager.LoadScene ("Settings");
        IsButtonPressed = true;
        click_audio.Play();
        ButtonId = 3;
        REAL_SHADE = Instantiate(SHADE) as GameObject;
        REAL_SHADE.transform.position = new Vector3(-0.2539f, 0.09f, -2.34f);
        REAL_SHADE.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
    }
	public void PatchFive(){
        // SceneManager.LoadScene ("Highest");
        IsButtonPressed = true;
        click_audio.Play();
        ButtonId = 4;
        REAL_SHADE = Instantiate(SHADE) as GameObject;
        REAL_SHADE.transform.position = new Vector3(-0.2539f, 0.09f, -2.34f);
        REAL_SHADE.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
    }

}
