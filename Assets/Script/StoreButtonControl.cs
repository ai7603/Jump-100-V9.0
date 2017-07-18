using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class StoreButtonControl : MonoBehaviour {

    public AudioSource click_audio;

    public Transform UIButton;
	public Transform UITextOne;
	public Transform UITextTwo;
	public Transform UIImg;

    public Text CoinNum;

    private bool IsButtonPressed;
    private int aux_counter;

    private GameObject SHADE;
    private GameObject REAL_SHADE;
	
	// Use this for initialization
	void Start () {

        UserInfo.IsPlayGame = false;

        IsButtonPressed = false;
        aux_counter = 0;

        SHADE = (GameObject)Resources.Load("Prefab/shadow2");

        CoinNum.text = "$" + UserInfo.COIN;
        if (UserInfo.PETS == true)
        {
            UIButton.gameObject.SetActive(false);
            UITextOne.gameObject.SetActive(false);
            UITextTwo.gameObject.SetActive(false);
            UIImg.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        CoinNum.text = "$" + UserInfo.COIN;
       
        if (IsButtonPressed)
        {
            aux_counter++;
            if (aux_counter == 60) {
                SceneManager.LoadScene("Main");
            }
        }
    }
	void OnGUI(){
		//if(cont)
			// GUI.Button (new Rect (300, 300, 350, 350), "BOUGHT");
	}

	public void Disable_Button(){
        click_audio.Play();
        string filepath = Application.dataPath + "/UserFiles" + "//" + UserInfo.USER + ".txt";
        string[] str = File.ReadAllLines(filepath);
        if (UserInfo.COIN >= 50 && UserInfo.PETS==false)
        { 
            UserInfo.COIN -= 50;
            UserInfo.PETS = true;
            str[0] = UserInfo.COIN.ToString();
            str[1] = UserInfo.PETS.ToString();
            File.WriteAllLines(filepath, str);
            UIButton.gameObject.SetActive(false);
            UITextOne.gameObject.SetActive(false);
            UITextTwo.gameObject.SetActive(false);
            UIImg.gameObject.SetActive(false);
           
        }

        
    }

    public void backtoMain() {
        //  SceneManager.LoadScene("Main");
        click_audio.Play();
        REAL_SHADE = Instantiate(SHADE) as GameObject;
        REAL_SHADE.transform.position = new Vector3(-17.37334f, -15.87492f, 1.0f);
        REAL_SHADE.transform.localScale = new Vector3(1000.0f, 1000.0f, 1000.0f);

        IsButtonPressed = true;
    }
}