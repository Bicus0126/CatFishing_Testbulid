using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EndingScreen : MonoBehaviour
{
    public GameObject Wintext;
    public GameObject WinCat;
    public GameObject Losetext;
    public AudioSource winSound;
    public AudioSource loseSound;

    public void EndingAnimation(bool WinEnding)
    {
        Time.timeScale = 0f;    //pause game
        if (WinEnding)
            StartCoroutine(WinAnimation());
        else
            StartCoroutine(LoseAnimation());
    }
    
    IEnumerator WinAnimation()
    {
        Transform transform = Wintext.transform;
        Vector3 NewSize = new Vector3(0f, 0f, 1f);
        transform.localScale = NewSize;
        WinCat.SetActive(true);
        Wintext.SetActive(true);
        
        winSound.Play();
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.01f);
        for (; NewSize.x < 1f;
             NewSize.x += 0.02f, NewSize.y = NewSize.x)
        {
            transform.localScale = NewSize;
            yield return wait;
        }
        yield break;
    }
    
    IEnumerator LoseAnimation()
    {
        Transform transform = Losetext.transform;
        Vector3 NewSize;
        float OldPos = transform.position.y;
        Vector3 NewPos = transform.position;
        NewPos.y += 400f;
        transform.position = NewPos;
        Losetext.SetActive(true);

        loseSound.Play();
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.01f);
        for (NewSize = new Vector3(2f, 2f, 1f); NewSize.x > 1f || NewPos.y > OldPos;
             NewSize.x -= 0.04f, NewSize.y = NewSize.x, NewPos.y -= 16f)
        {
            transform.localScale = NewSize;
            transform.position = NewPos;
            yield return wait;
        }
        yield break;
    }
}
