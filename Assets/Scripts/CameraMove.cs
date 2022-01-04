using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //moving camera in y axis by LMB, reset camera by middle mouse button
    //floor hard limited at y=0
    public GameObject highpointFinder;
    public float Fzone = 2f;
    public float followInTime = 3f;
    [ReadOnlyInspecter] public float velocity;
    [ReadOnlyInspecter] public float highpoint;
    private float Diff_y;
    private Vector3 originPos;
    private Vector3 ResetCamera;

    private bool drag = false;

    private void Start()
    {
        //Camera.main.transform.position = Vector3.zero;
        //ResetCamera = Camera.main.transform.position;
    }

    private Vector3 camPos;
    private Vector3 mousePos;
    [ReadOnlyInspecter] public Vector3 newCamPos;
    [ReadOnlyInspecter] public bool followmode = true;
    [ReadOnlyInspecter] public bool inZone;
    private void LateUpdate()
    {
        camPos = transform.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        highpoint = highpointFinder.transform.position.y +2f;   //offsetted
        inZone = (camPos.y + Fzone > highpoint && highpoint > camPos.y);
        followmode = inZone && !drag ? true : followmode;

        if (Input.GetMouseButton(1))
        {
            Diff_y = mousePos.y - camPos.y;
            if (!drag)
            {
                drag = true;
                followmode = false;
                originPos = mousePos;
            }
        }
        else
            drag = false;

        newCamPos = camPos;
        newCamPos.y = originPos.y - Diff_y;

        if (drag)
        {
            //Debug.Log("Dragging Camera");
            //set camera position ceiling limit
            //set camera position floor limit
            if (camPos.y < highpoint || newCamPos.y <= camPos.y)
            {
                if (newCamPos.y < 0)
                {
                    newCamPos.y = 0;
                    drag = false;
                }
                if (newCamPos != camPos) transform.position = newCamPos;
            }
            else drag = false;
        }

        if (camPos.y > highpoint && !drag)
        {
            //Debug.Log("Camera Over highpoint");
            if (highpoint < 0 && camPos.y < 0.02f) newCamPos.y = 0;
            else
            {
                highpoint -= 0.5f;
                newCamPos.y =
                Mathf.SmoothDamp(camPos.y, highpoint > 0 ? highpoint : 0, ref velocity, followInTime);
            }
            if (newCamPos != camPos) transform.position = newCamPos;
        }
        else if (followmode && !inZone && !drag)
        {
            //Debug.Log("Follow Cam");
            //highpoint = camPos.y + Fzone < highpoint ? highpoint - 0.1f : highpoint + 0.1f;
            newCamPos.y =
            Mathf.SmoothDamp(camPos.y, highpoint > 0 ? highpoint : 0, ref velocity, followInTime);
            if (newCamPos != camPos) transform.position = newCamPos;
        }
        /*if (Input.GetMouseButtonDown(2))
            Camera.main.transform.position = ResetCamera;*/
    }
}