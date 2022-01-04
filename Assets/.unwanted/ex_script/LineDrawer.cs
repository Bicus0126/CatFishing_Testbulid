using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRend;
    private Vector2 mousePos;
    private Vector2 startMousePos;
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRend.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
            lineRend.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
        }
    }
    public void Drawline(float objx, float objy, float mx, float my)
    {
        if (Input.GetMouseButton(0))
        {
            lineRend.SetPosition(0, new Vector3(objx, objy, 0f));
            lineRend.SetPosition(1, new Vector3(mx, my, 0f));
        }
    }
}
