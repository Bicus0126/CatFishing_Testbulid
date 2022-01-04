using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    public bool checkEmpty = true;

    void OnTriggerStay2D()
    {
        checkEmpty = false;
    }

    void OnTriggerExit2D()
    {
        checkEmpty = true;
    }
}
