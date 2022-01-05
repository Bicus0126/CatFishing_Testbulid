using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Blackscreen;
    Image FadeBlack;
    public float FadeOutTime = 1f;

    public GameObject BGPanel;
    AudioSource BGM;
    public GameObject CatObject;
    public AudioSource CatSound;
    Transform CatTransform;
    Animator CatAnimator;
    Rigidbody2D CatRigidbody;
    Vector2 SpeedDir;
    void Start()
    {
        Blackscreen.SetActive(false);
        FadeBlack = Blackscreen.GetComponent<Image>();
        CatTransform = CatObject.GetComponent<Transform>();
        CatAnimator = CatObject.GetComponent<Animator>();
        CatRigidbody = CatObject.GetComponent<Rigidbody2D>();
    }
    public void PlayGame ()
    {
        BGM = BGPanel.GetComponent<AudioSource>();
        Blackscreen.SetActive(true);
        CatSound.Play();
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        CatAnimator.SetBool("jump", true);
        yield return new WaitForSeconds(0.2f);

        Vector2 gameSize = new Vector2(Screen.width, Screen.height);

        SpeedDir.x = 400f;      //貓咪跳耀速度
        SpeedDir.y = 500f;
        SpeedDir *= gameSize.x / 1920;
        CatRigidbody.velocity = SpeedDir;
        CatRigidbody.gravityScale = 100f * gameSize.y / 1080;
        yield return new WaitUntil(() => CatTransform.position.y <= -90f);

        Color FadeColor = FadeBlack.color;
        WaitForSeconds WaitToFade = new WaitForSeconds(FadeOutTime/100f);
        for (float t = 0; t <= 1 || BGM.volume > 0; t += 0.01f, BGM.volume -= 0.005f)
        {
            // Debug.Log("t = " + t);
            FadeBlack.color = new Color(FadeColor.r, FadeColor.g, FadeColor.b, t);
            yield return WaitToFade;
        }   //漸入黑屏 音樂淡出

        SceneManager.LoadScene("level0");
        yield break;
    }
    void FixedUpdate()
    {
        CatAnimator.SetFloat("speed", CatRigidbody.velocity.y);
    }
    public void QuitGame ()
    {
        Debug.Log ("Game QUIT!");
        Application.Quit();
    }
}