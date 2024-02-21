using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [Header("Serialize")]
    [SerializeField] private Slider volumeSlider;

    private AudioManager[] audioManagers;
    private PlayMenuMusic playMenuMusic;

    private void Start()
    {
        // Init stuff
        audioManagers = FindObjectsOfType<AudioManager>();
        playMenuMusic = FindObjectOfType<PlayMenuMusic>();

        // Set value on sliders
        if (PlayerPrefs.HasKey("Volume"))
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        else
            PlayerPrefs.SetFloat("Volume", 0.5f);
    }

    public void OnVolumeSliderChange()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        UpdateVolumes();
    }

    private void UpdateVolumes()
    {
        foreach (AudioManager audioManager in audioManagers) 
        {
            audioManager.SetVolume(volumeSlider.value);
        }

        playMenuMusic.SetVolume(volumeSlider.value);
    }
}
