using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScreen : MonoBehaviour
{

    private float Internal = 90;
    float count = 0;
    public bool flag;
    // Use this for initialization
    void Start()
    {
        count = 0;
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (count < Internal && flag)
        {
            transform.GetComponent<SpriteRenderer>().color = new Color(256, 256, 256, 1.0f * (Internal - count) / Internal);
            count++;
        }
        else
        {
            flag = false;
            transform.GetComponent<SpriteRenderer>().color = new Color(256, 256, 256, 0.0f);
            count = 0;
        }
    }
}
