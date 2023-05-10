using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Audio;
using TMPro;
using UnityEngine;

public class settingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown res_dropdown;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        res_dropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0;i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].width == Screen.currentResolution.width)
            {
                currentResolutionIndex = i;
            }
        }

        res_dropdown.AddOptions(options);
        res_dropdown.value = currentResolutionIndex;
        res_dropdown.RefreshShownValue();
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
