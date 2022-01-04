using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockRecycler : MonoBehaviour
{
    public GameObject GameController;
    public float BufferTime = 1f;
    float buftime = 0f;
    private Health HP;
    void Start()
    {
        HP = GameController.GetComponent<Health>();
    }
    void OnTriggerEnter2D(Collider2D ObjectCollider)
    {
        GameObject CollideObject = ObjectCollider.gameObject;
        string ObjTag = CollideObject.tag;
        Debug.Log(ObjTag + " detected");
        if(ObjTag == "Blocks")
        {
            CollideObject.GetComponent<onBlockSpawn>().recycled();
            CollideObject.SetActive(false);
            if (buftime >= BufferTime)
            {
                HP.hurt(1);
                buftime = 0f;
            }
            Debug.Log("Block " + CollideObject.name + " recycled.");
        }
        else if (ObjTag == "Player")
        {
            Destroy(CollideObject);
            GameController.GetComponent<GameProcess>().CatDied();
            Debug.Log("Player Died!");
        }
    }

    void Update()
    {
        if (buftime <= BufferTime)
        {
            buftime += Time.deltaTime;
        }
    }
}
