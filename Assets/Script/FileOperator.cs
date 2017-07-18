using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// The way of organizing files:
// filename: username
// the first line: coin number
// the second line: whether a owner of a dog


public class FileOperator : MonoBehaviour {

	public Text inputText;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}


	// return true if create successfully
	public bool CreateUser(string username){
		if (CreateFile (username + ".txt")) {
			Debug.Log ("New user created!");

			string path = Application.dataPath + "/UserFiles" + "//" + username + ".txt";
			FileStream fs = new FileStream (path, FileMode.Open);
			StreamWriter sw = new StreamWriter (fs);
			sw.WriteLine ("0");
			sw.WriteLine ("0");

			sw.Close ();
			fs.Close ();
			sw.Dispose ();
			return true;
		} else {
			Debug.Log ("User exists!");
			return false;
		}
	}

	public int GetCoin(string username){
		string path = Application.dataPath + "/UserFiles" + "//" + username + ".txt";
		StreamReader sr = null;

		sr = File.OpenText(path);
		string line;
		line = sr.ReadLine ();
		int c = Convert.ToInt32 (line);

		sr.Close ();
		sr.Dispose ();

		return c;
	}

	public bool HasPets(string username){
		string path = Application.dataPath + "/UserFiles" + "//" + username + ".txt";
		StreamReader sr = null;

		sr = File.OpenText(path);
		string line;
		line = sr.ReadLine (); line = sr.ReadLine ();

		bool hasIt;
		if (line == "True")
			hasIt = true;
		else if (line == "False")
			hasIt = false;
		else {
			// error occurs
			hasIt = false;
		}

		sr.Close ();
		sr.Dispose ();

		return hasIt;
	}

	public void SetCoin(string username, int newcoin){
		bool hasIt = HasPets (username);

		// ClearFile (username);

		string path = Application.dataPath + "/UserFiles" + "//" + username + ".txt";
		StreamWriter sw = null;

		sw = new StreamWriter (path, false);
		sw.WriteLine (newcoin.ToString());

		if (hasIt)
			sw.WriteLine ("1");
		else
			sw.WriteLine ("0");

		sw.Close ();
		sw.Dispose ();
	}

	public void SetHasPets(string username, bool haspets){
		int coin = GetCoin (username);
		string path = Application.dataPath + "/UserFiles" + "//" + username + ".txt";

		StreamWriter sw = null;

		sw = new StreamWriter (path, false);

		sw.WriteLine (coin.ToString() );
		if (haspets)
			sw.WriteLine ("1");
		else
			sw.WriteLine ("0");

		sw.Close ();
		sw.Dispose ();
	}

	private void ClearFile(string filename){
		string path = Application.dataPath + "/UserFiles" + "//" + filename;
		StreamWriter sw = null;

		sw = new StreamWriter (path, false);
		sw.WriteLine ("");
		sw.Close ();
		sw.Dispose ();
	}

	private bool CreateFile(string filename){
		string path = Application.dataPath + "/UserFiles" + "//" + filename; 
		//StreamReader sr = null;

		if (File.Exists (path)) {
			return false;
		} else {
			File.Create (path).Close();
			return true;
		}
	}

	public void testOnClick(){

	    CreateUser(inputText.text);

        UserInfo.USER = inputText.text;
        UserInfo.COIN = GetCoin(inputText.text);
        UserInfo.PETS = HasPets(inputText.text);

		if (inputText.text.Length<=8) SceneManager.LoadScene ("Main");
	}
}