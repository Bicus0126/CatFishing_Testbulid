using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokePuff : MonoBehaviour
{
    public SpriteRenderer thisSprite;
    public Transform thisTransform;
    Vector3 NewSize;
    Color NewColor;
    float TargetSize = 0.3f;
    void Start()
    {
        NewColor = thisSprite.color;
        StartCoroutine(Animation());
    }
    IEnumerator Animation()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        for (NewSize = new Vector3(0f, 0f, 1f); NewSize.x < TargetSize || thisSprite.color.a > 0;
             NewSize.x += 0.003f, NewSize.y = NewSize.x)
        {
            thisTransform.localScale = NewSize;
            if (NewSize.x >= (TargetSize *2/3))
            {
                NewColor.a -= 0.1f;
                thisSprite.color = NewColor;
            }
            yield return wait;
        }
        Destroy(gameObject);
        yield break;
    }
}
