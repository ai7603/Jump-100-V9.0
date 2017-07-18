using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class Number: MonoBehaviour {


     public Text NoText;
     string[] str;
     string filepath;
// Use this for initialization
     void Start () {
        NoText = GameObject.Find("Canvas/Panel/Norank").GetComponent<Text>();
        filepath = Application.dataPath + "/StreamingAssets" + "/score.txt";
        str = File.ReadAllLines(filepath, Encoding.ASCII);
     }
	
	// Update is called once per frame
	void Update () {
        NoText.text = "";
        for (int i = 0; i < 6; i++)
        {
            int len = str[i * 2].Length;
            NoText.text += str[i * 2];
            NoText.text+="\n\n";
        }
    }
}
