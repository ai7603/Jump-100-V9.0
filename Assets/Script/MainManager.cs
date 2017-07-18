using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class MainManager : MonoBehaviour {

    public AudioSource click_audio;

    [SerializeField]
    Transform UIPanel; //Will assign our panel to this variable so we can enable/disable it

    public Text HelloText;

    private void Start()
    {
        SayHello();
    }
    public void StartGame()
    {
        click_audio.Play();
        print("StartGame!");


        //SceneManager.LoadScene("Test");
        SceneManager.LoadScene("scene1");
    }

    public void Highest()
    {
        click_audio.Play();
        print("Highest");
        SceneManager.LoadScene("Highest");
    }

    public void Quit()
    {
        click_audio.Play();
        Application.Quit();
    }
    void Update()
    {

        //  if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        //       Pause();
        //   else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        //      UnPause();
    }
	public void gotoStore(){
        click_audio.Play();
		SceneManager.LoadScene ("Store");
	}
	public void gotoSettings(){
        click_audio.Play();
		SceneManager.LoadScene ("Settings");
	}

    public void SayHello() {
        print(UserInfo.USER);
        HelloText.text = "Hello, " + UserInfo.USER + ".";
    }
}
