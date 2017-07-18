using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour {

    public AudioSource bg;

	// Use this for initialization

    
	void Start () {
        UserInfo.IsPlayGame = false;
        DontDestroyOnLoad(bg);
	}
	
	// Update is called once per frame
	void Update () {
        if (UserInfo.IsPlayGame)
        {
            bg.Stop();
        }
        if (!bg.isPlaying) {
            bg.Play();
        }
	}
}
