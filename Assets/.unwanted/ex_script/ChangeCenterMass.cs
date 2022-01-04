using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCenterMass : MonoBehaviour
{
    private Transform CenterMass;
    private Rigidbody2D Rigidbody2D;
    public Vector3 CoM;
    // Start is called before the first frame update
    void Start()
    {
        CenterMass = this.gameObject.transform.GetChild(0);
        Rigidbody2D = this.GetComponent<Rigidbody2D>();
        //Rigidbody2D.centerOfMass = CenterMass.localPosition;
        CoM = Rigidbody2D.centerOfMass;
    }
}
