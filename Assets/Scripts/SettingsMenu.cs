using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResIndex = 0;
        List<string> ResOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            ResOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(ResOptions);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution (int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Fullscreen = " + isFullscreen);
    }
    public void SetVolume (float volume)
    {
        if (volume == 0)
            audioMixer.SetFloat("Volume", -80f);
        else
            audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}
