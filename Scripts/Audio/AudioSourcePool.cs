using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioSourcePool
{
    [Header("Pool Configuration")]
    [SerializeField] private int size = 5;
    [SerializeField] private AudioMixerGroup mixerGroup;

    private AudioSource[] audioSources;
    private int currentAudioSourceIndex;

    public void Initialize(GameObject parent)
    {
        audioSources = new AudioSource[size];

        for (int i = 0; i < size; i++)
        {
            AudioSource source = parent.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = mixerGroup;
            audioSources[i] = source;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Attempted to play a null AudioClip.");
            return;
        }

        AudioSource source = audioSources[currentAudioSourceIndex];
        source.clip = clip;
        source.Play();

        currentAudioSourceIndex = (currentAudioSourceIndex + 1) % size;
    }
}