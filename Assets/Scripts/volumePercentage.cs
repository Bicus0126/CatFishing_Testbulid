using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class volumePercentage : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void setValue (float value)
    {
        textMesh.SetText((int)(value * 100) + "%");
    }
}
