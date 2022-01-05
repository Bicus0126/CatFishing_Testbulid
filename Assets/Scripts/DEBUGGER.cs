using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEBUGGER : MonoBehaviour
{
    Text LOGtext;
    bool DEBUGMode;
    void Start()
    {
        LOGtext = GetComponent<Text>();
        DEBUGMode = false;
        DEBUG();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.BackQuote))
            DEBUG();
    }

    void DEBUG()
    {
        DEBUGMode = !DEBUGMode;
        if (DEBUGMode) LOGtext.color = Color.clear;
        else LOGtext.color = Color.black;
    }
}
