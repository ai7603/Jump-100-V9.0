﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonInSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UserInfo.IsPlayGame = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void backtomain(){
		SceneManager.LoadScene ("Main");
	}
}
