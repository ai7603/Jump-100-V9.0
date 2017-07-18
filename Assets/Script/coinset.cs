using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
public class coinset : MonoBehaviour {

    public Text Coin;
    private int num;
    string filepath;
    string[] str;
	// Use this for initialization
	void Start () {
        Coin = GameObject.Find("Canvas/coin").GetComponent<Text>();
        filepath= filepath = Application.dataPath + "/UserFiles" + "//" + UserInfo.USER + ".txt";
        str = File.ReadAllLines(filepath);
        num = UserInfo.COIN;
    }
	
    public void Add(int tmp)
    {
        num += tmp;
        UserInfo.COIN = num;
        str[0] = UserInfo.COIN.ToString();
        File.WriteAllLines(filepath, str);     
    }

    public void Show()
    {
        Coin.text = "Coins: " + num;
    }

	// Update is called once per frame
	void Update () {
        Show();
	}
}
