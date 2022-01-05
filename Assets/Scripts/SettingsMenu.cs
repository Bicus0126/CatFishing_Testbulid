using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public GameObject FullscreenTgl;
    public GameObject VolumeControl;
    Slider VolumeSlider;
    public GameObject Debugger;
    Text LOG;
    Resolution[] resolutions;
    void Start()
    {
        LOG = Debugger.GetComponent<Text>();

        VolumeSlider = VolumeControl.GetComponent<Slider>();
        audioMixer.GetFloat("Volume", out float v);
        VolumeSlider.value = (v <= -80f) ? 0 : Mathf.Pow(10, v / 20);

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        Toggle FSToggle = FullscreenTgl.GetComponent<Toggle>();
        Vector2 gameSize = new Vector2(Screen.width, Screen.height);

        int currentResIndex = 0;
        List<string> ResOptions = new List<string>();
        Resolution lastRes = resolutions[resolutions.Length-1];
        for (int i = resolutions.Length-1; i >= 0; i--)
        {
            Debug.Log("i="+i);
            
            string option = resolutions[i].ToString();
            ResOptions.Add(option);
            if (resolutions[i].width == gameSize.x &&
                resolutions[i].height == gameSize.y)
            {
                currentResIndex = resolutions.Length-1 - i;
            }
            Debug.Log("LoadResolution = " + resolutions[i].width + " x " + resolutions[i].height);

            lastRes = resolutions[i];
        }


        resolutionDropdown.AddOptions(ResOptions);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
        
        FSToggle.isOn = Screen.fullScreen;
    }
    public void SetResolution (int resIndex)
    {
        Resolution resolution = resolutions[resolutions.Length-1 - resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("current Resolution = " + Screen.width + " x " + Screen.height);
        LOG.text += "\nSelect Resolution = " + resolution.width + " x " + resolution.height;
        LOG.text += "\nCurrent Resolution = " + Screen.width + " x " + Screen.height;
    }
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Fullscreen = " + isFullscreen);
        LOG.text += "\nFullscreen = " + isFullscreen;
    }
    public void SetVolume (float volume)
    {
        if (volume == 0)
            audioMixer.SetFloat("Volume", -80f);
        else
            audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}
