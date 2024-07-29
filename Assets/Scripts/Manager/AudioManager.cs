using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource MusicAudioSource;
    [SerializeField] AudioSource SFXAudioSource;

    void Awake() {
        InitializeSingleton();
    }

    void InitializeSingleton() {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clipToPlay) {
        MusicAudioSource.clip = clipToPlay;
        MusicAudioSource.Play();
    }

    public void StopMusic() {
        MusicAudioSource.Stop();
    }

    public void PlaySFX(AudioClip clipToPlay) {
        SFXAudioSource.Stop();
        SFXAudioSource.clip = clipToPlay;
        SFXAudioSource.Play();
    }

    public void StopSFX() {
        SFXAudioSource.Stop();
    }
}