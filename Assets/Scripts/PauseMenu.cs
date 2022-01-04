using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [ReadOnlyInspecter] public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    public AudioMixer audioMixer;
    public GameObject VolumeControl;
    public Slider VolumeSlider;
    void Start()
    {
        VolumeSlider = VolumeControl.GetComponent<Slider>();
        audioMixer.GetFloat("Volume", out float v);
        VolumeSlider.value = (v <= -80f) ? 0 : Mathf.Pow(10, v / 20);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
    
    public void SetVolume (float volume)
    {
        if (volume == 0)       //mute
            audioMixer.SetFloat("Volume", -80f);
        else
            audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}
