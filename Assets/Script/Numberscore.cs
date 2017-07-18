using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class Numberscore : MonoBehaviour
{


    public Text ScoreText;
    string[] scorestr;
    string path;
    // Use this for initialization
    void Start()
    {
        ScoreText = GameObject.Find("Canvas/Panel/Score").GetComponent<Text>();
        path = Application.dataPath + "/StreamingAssets" + "/score.txt";
        scorestr = File.ReadAllLines(path, Encoding.ASCII);
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "";
        for (int i = 0; i < 6; i++)
        {
             ScoreText.text +=scorestr[i*2+1] + "\n\n";
         }
    }
}
