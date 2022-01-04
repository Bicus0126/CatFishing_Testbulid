using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highpointFinder : MonoBehaviour
{
    //private GameObject Cam;
    public GameObject blockSpawner;    //生成器
    public GameObject blockRecycler;   //回收區
    private EdgeCollider2D BRcollider;  //回收區碰撞
    private RaycastHit2D hit;   //高點下界
    private RaycastHit2D noHit; //高點上界
    //private RaycastHit2D Spawnpoint;
    public Vector3 rayPos;  
    public float startHeight;
    //public float lastHeight;
    public float spawnerHeight = 15f;   //生成高度
    //public string detectedBlock;


    void Start()
    {
        BRcollider = blockRecycler.GetComponent<EdgeCollider2D>();
        startHeight = gameObject.transform.position.y;
        blockSpawner.transform.position = new Vector3(0f, startHeight + spawnerHeight, 0f);
    }
    void Update()
    {
        rayPos = gameObject.transform.position;
        hit = Physics2D.Raycast(new Vector2(rayPos.x, rayPos.y), Vector2.right, 20, LayerMask.GetMask("Props"));
        noHit = Physics2D.Raycast(new Vector2(rayPos.x, rayPos.y + 0.2f), Vector2.right, 20, LayerMask.GetMask("Props"));

        Debug.DrawRay(new Vector2(rayPos.x, rayPos.y), Vector3.right * 20, Color.green);
        if (noHit.collider)
        {
            //lastHeight = rayPos.y;
            rayPos.y += 0.1f;
            gameObject.transform.position = rayPos;
            blockSpawner.transform.position = new Vector3(0f, rayPos.y + spawnerHeight, 0f);

            /*if(hit.collider)
            {
                detectedBlock = hit.transform.name;
                Debug.Log("highpoint found " + detectedBlock);
            }*/
        }
        else if (!hit.collider && !noHit.collider)
        {
            if (rayPos.y > startHeight)
            {
                rayPos.y -= 0.1f;
                gameObject.transform.position = rayPos;
                blockSpawner.transform.position = new Vector3(0f, rayPos.y + spawnerHeight, 0f);
            }
        }

        // Height = blockSpawner.transform.position.y;
        // if (Height >= flag)                                      //過特定高度
        // {
        //     toohigh = true;
        //     T_counter += Time.deltaTime;
        //     if (T_counter > 10.0f && !Generate)                  //超過3秒 且 未生成過 player
        //     {
        //         Instantiate(player);
        //         for (int i = -5; i < 6; i += 2)
        //             Instantiate(Fish, new Vector3(i, Height - flag + 1.0f, 0.0f), Quaternion.identity);

        //         Generate = true;
        //     }
        // }
        // else
        // {
        //     T_counter = 0.0f;
        //     toohigh = false;
        // }
    }

    void LateUpdate()
    {
        float recycler_y = blockRecycler.transform.position.y + BRcollider.points[0].y;
        if (rayPos.y + spawnerHeight + 25 >= recycler_y)
        {
            Vector2[] newPoints = BRcollider.points;
            newPoints[0].y += 25;
            newPoints[1].y += 25;
            newPoints[4].y += 25;
            newPoints[5].y += 25;
            BRcollider.points = newPoints;
            //Debug.Log("Recycler border updated.");
        }
    }
}
