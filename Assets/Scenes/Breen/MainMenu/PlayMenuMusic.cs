using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuMusic : MonoBehaviour
{
    [SerializeField] private AudioClip soundtrack;

    private AudioSource audioSource;

    void Start()
    {
        if (GetComponent<AudioSource>() == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        else
            audioSource = GetComponent<AudioSource>();

        // Set Volume from PlayerPrefs
        if (PlayerPrefs.HasKey("BGM") && PlayerPrefs.HasKey("Master")) 
            audioSource.volume = PlayerPrefs.GetFloat("BGM") * PlayerPrefs.GetFloat("Master");
        else
            audioSource.volume = 0.25f;

        audioSource.clip = soundtrack;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        if (audioSource)
            audioSource.volume = volume;
    }
}
