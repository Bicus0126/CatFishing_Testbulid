using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishTrigger : MonoBehaviour
{
    public GameObject CatSound;
    public GameObject SmokePuff;
    public AudioSource WaterSound;
    GameProcess gameProcess;
    void Start()
    {
        gameProcess = GameObject.Find("GameController").GetComponent<GameProcess>();
        WaterSound.time = 2.4f;
        WaterSound.Play();
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        Vector3 NewSize;
        float TargetSize = transform.localScale.x;
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        for (NewSize = new Vector3(0.2f, 0.2f, 1f); NewSize.x < TargetSize;
             NewSize.x += 0.06f, NewSize.y = NewSize.x)
        {
            transform.localScale = NewSize;
            yield return wait;
        }
        yield break;
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
