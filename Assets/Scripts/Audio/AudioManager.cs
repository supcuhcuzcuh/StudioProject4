using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List<SoundEffect> soundEffectsList = new List<SoundEffect>();
    private AudioSource sfxAudioSrc;

    void Start()
    {
        sfxAudioSrc = GetComponent<AudioSource>();

        // Set Volume from PlayerPrefs
        if (PlayerPrefs.HasKey("VFX") && PlayerPrefs.HasKey("Master")) 
            sfxAudioSrc.volume = PlayerPrefs.GetFloat("VFX") * PlayerPrefs.GetFloat("Master");
        else
            sfxAudioSrc.volume = 0.25f;
    }

    public bool PlaySoundEffect(string _soundEffectName)
    {
        foreach (SoundEffect soundEffect in soundEffectsList)
        {
            if (soundEffect.soundEffectName == _soundEffectName)
            {
                if (sfxAudioSrc != null)
                {
                    sfxAudioSrc.clip = soundEffect.soundEffectClip;
                    sfxAudioSrc.Play();
                }
                return true;
            }
        }
        Debug.Log("Sound effect not found");
        return false;
    }

    public bool PlaySoundEffectLoop(string _soundEffectName)
    {
        if (!sfxAudioSrc.isPlaying)
        {
            foreach (SoundEffect soundEffect in soundEffectsList)
            {
                if (soundEffect.soundEffectName == _soundEffectName)
                {
                    sfxAudioSrc.clip = soundEffect.soundEffectClip;
                    sfxAudioSrc.Play();
                    return true;
                }
            }
            Debug.Log("Sound effect not found");
            return false;
        }
        return false;
    }

    public void StopSoundEffect()
    {
        if (sfxAudioSrc.isPlaying)
        {
            sfxAudioSrc.Stop();
        }      
    }

    public void SetVolume(float volume)
    {
        if (sfxAudioSrc)
            sfxAudioSrc.volume = volume;
    }
}
