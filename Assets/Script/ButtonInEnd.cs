using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonInEnd : MonoBehaviour {

    public Text ScoreShow;

    public AudioSource click_audio;
    public AudioSource change_audio;


	// Use this for initialization
	void Start () {
        ScoreShow.text = "Score: \n" + UserInfo.highestscore;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void backtomain(){
		SceneManager.LoadScene ("Main");
	}
	public void gotoscene1(){
		SceneManager.LoadScene ("scene1");
	}
}
