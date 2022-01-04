using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Heightnumber : MonoBehaviour
{
    public GameObject Heightpoint;
    public Text HeightText;
    [ReadOnlyInspecter] public float Height;

    // Start is called before the first frame update
    void Start()
    {
        Height = 0.0f;
        HeightText.text = Height.ToString("0.0");
    }

    // Update is called once per frame
    void Update()
    {
        Height = Heightpoint.transform.position.y + 3.9f;
        HeightText.text = Height.ToString("0.0");
        //Debug.Log(Height);
    }
}
