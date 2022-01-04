
using UnityEngine;

public class FishTrigger : MonoBehaviour
{
    //音效-----------

    public GameObject WaterSound;
    public GameObject CatSound;
    public GameObject SmokePuff;
    GameProcess gameProcess;
    void Start()
    {
        gameProcess = GameObject.Find("GameController").GetComponent<GameProcess>();
        Instantiate(WaterSound, transform.position, Quaternion.identity);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        if (other.tag == "Player")
        {
            Instantiate(SmokePuff, transform.position, Quaternion.identity);
            Instantiate(CatSound, transform.position, Quaternion.identity);
            Debug.Log(gameObject.name);
            Destroy(gameObject);
            Destroy(other.gameObject);
            gameProcess.FishEaten = true;
        }
    }

}
