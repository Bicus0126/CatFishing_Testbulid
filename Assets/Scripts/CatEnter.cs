using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEnter : MonoBehaviour
{   
    public GameObject CatPrefab;
    public GameObject CatSound;
    [HideInInspector] public GameObject PlayerCat;
    public float AnimationTime = 1.6f;
    public float HorizontalSpeed = 9f;
    Rigidbody2D CatRigidbody;
    public Vector3 SpawnPoint = new Vector3(-15f, 8f, 0f);
    [HideInInspector] public bool AnimationEnd = false;
    public void CatAnimation()
    {
        AnimationEnd = false;
        StartCoroutine(AnimationProcess());
    }

    IEnumerator AnimationProcess()
    {
        PlayerCat = Instantiate(CatPrefab, SpawnPoint, Quaternion.identity);    //生成貓咪於SpawnPoint
        PlayerCat.SetActive(true);
        PlayerCat.GetComponent<PlayerControl>().enabled = false;
        PlayerCat.GetComponent<Rigidbody2D>().velocity = Vector2.right * HorizontalSpeed;
        PlayerCat.GetComponent<Animator>().SetBool("jump", true);
        PlayerCat.GetComponent<Animator>().SetFloat("Speed_Y", -1f);
        PlayerCat.GetComponent<Animator>().SetBool("IsAir", true);
        Instantiate(CatSound, transform.position, Quaternion.identity);     //播放音效
        yield return new WaitForSeconds(AnimationTime);

        PlayerCat.GetComponent<PlayerControl>().enabled = true;
        AnimationEnd = true;
        yield break;
    }
}
