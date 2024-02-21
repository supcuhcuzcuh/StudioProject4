using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [Header("Serialize")]
    [SerializeField] private Slider volumeSliderMaster;
    [SerializeField] private Slider volumeSliderBGM;
    [SerializeField] private Slider volumeSliderVFX;
    [SerializeField] private Slider sensitivitySliderVFX;

    private AudioManager[] audioManagers;
    private PlayMenuMusic playMenuMusic;

    private void Start()
    {
        // Init stuff
        audioManagers = FindObjectsOfType<AudioManager>();
        playMenuMusic = FindObjectOfType<PlayMenuMusic>();

        // Set value on sliders
        if (PlayerPrefs.HasKey("Master"))
            volumeSliderMaster.value = PlayerPrefs.GetFloat("Master");
        else
            PlayerPrefs.SetFloat("Master", 0.5f);


        if (PlayerPrefs.HasKey("BGM"))
            volumeSliderBGM.value = PlayerPrefs.GetFloat("BGM");
        else
            PlayerPrefs.SetFloat("BGM", 0.5f);


        if (PlayerPrefs.HasKey("VFX"))
            volumeSliderMaster.value = PlayerPrefs.GetFloat("VFX");
        else
            PlayerPrefs.SetFloat("VFX", 0.5f);
    }

    public void OnVolumeSliderChange(string type)
    {
        Slider slider = null;

        switch (type)
        {
            case "Master":
                slider = volumeSliderMaster;
                break;
            case "BGM":
                slider = volumeSliderBGM;
                break;
            case "VFX":
                slider = volumeSliderVFX;
                break;
            default:
                slider = sensitivitySliderVFX;
                break;
        }

        PlayerPrefs.SetFloat(type, slider.value);
        UpdateVolumes();
    }

    public void OnVolumeSliderReset()
    {
        PlayerPrefs.SetFloat("Master", 0.5f);
        volumeSliderMaster.value = PlayerPrefs.GetFloat("Master");
        PlayerPrefs.SetFloat("BGM", 0.5f);
        volumeSliderBGM.value = PlayerPrefs.GetFloat("BGM");
        PlayerPrefs.SetFloat("VFX", 0.5f);
        volumeSliderVFX.value = PlayerPrefs.GetFloat("VFX");
    }

    private void UpdateVolumes()
    {
        foreach (AudioManager audioManager in audioManagers) 
        {
            audioManager.SetVolume(volumeSliderVFX.value * volumeSliderMaster.value);
        }

        playMenuMusic.SetVolume(volumeSliderBGM.value * volumeSliderMaster.value);
    }
}
