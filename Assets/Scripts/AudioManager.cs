using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //Component
    private AudioSource musicSource;
    private AudioSource soundSource;
    [SerializeField]
    public AudioMixer mixer;
    //Settings
    public AudioClip music;
    //Data
    [SerializeField]
    private AudioClip[] sfxClips;
    //Write sounds when added
    void Start()
    {
        musicSource = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        soundSource = transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        //If there are not instances 
        if(instance == null)
        {
            instance = this;
            instance.musicSource.clip = instance.music;
            instance.musicSource.Play();
            DontDestroyOnLoad(gameObject);
        } else
        {
            //if there are more we take the data we want from the new ones and destroy them
            if(instance.music != music)
            {
                instance.musicSource.Stop();
                instance.music = music;
                instance.musicSource.clip = music;
                instance.musicSource.Play();
                Destroy(gameObject);
            }
        }
    }
    /// <summary>
    /// Recieves a float that using a log10 gets turned into the volume for the music
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeMusicVolume(float volume)
    {
        float trueVolume = Mathf.Log10(volume) * 20;
        print(trueVolume);
        if(float.IsInfinity(trueVolume))
        {
            trueVolume = -80;
        }
        PlayerPrefs.SetFloat(UsefulConstants.MUSICVOLPARAM, trueVolume);
        mixer.SetFloat(UsefulConstants.MUSICVOLPARAM, trueVolume);
    }
    /// <summary>
    /// Recieves a float that using a log10 gets turned into the volume for the sound
    /// </summary>
    /// <param name="volume"></param>
    public void ChangeSfxVolume(float volume)
    {
        float trueVolume = Mathf.Log10(volume) * 20;
        if (float.IsInfinity(trueVolume))
        {
            trueVolume = -80;
        }
        PlayerPrefs.SetFloat(UsefulConstants.SFXVOLPARAM, trueVolume);
        mixer.SetFloat(UsefulConstants.SFXVOLPARAM, trueVolume);
    }

    
}
