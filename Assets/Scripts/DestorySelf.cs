using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestorySelf : MonoBehaviour
{
    public float DestroyInTime = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyInTime);
    }

}
