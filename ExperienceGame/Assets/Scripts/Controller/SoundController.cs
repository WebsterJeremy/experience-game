using System;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

[ExecuteInEditMode]
public class SoundController : MonoBehaviour
{
    #region AccessVariables

    public static bool MUTED = false;

    [Serializable]
    public class Sound
    {
        public string key = "";
        public AudioClip audioClip;
        [Range(0,1)]
        public float volume = 1f;
        [Range(-3, 3)]
        public float pitch = 1f;
    }
    [SerializeField] private Sound[] soundEffects;


    #endregion
    #region PrivateVariables


    private Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();
    private AudioSource audioSource;
    private AudioSource musicSource;


    #endregion
    #region Initlization


    private static SoundController instance;
    public static SoundController Instance // Assign Singlton
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundController>();
                if (Instance == null)
                {
                    var instanceContainer = new GameObject("SoundController");
                    instance = instanceContainer.AddComponent<SoundController>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        audioSource = GetComponents<AudioSource>()[0];
        musicSource = GetComponents<AudioSource>()[1];

        foreach (Sound sound in soundEffects)
        {
            if (sound.audioClip != null)
            {
                if (sound.key == "") sound.key = sound.audioClip.name;

                sounds.Add(sound.key, sound);
            }
        }
    }


    #endregion
    #region Main


    public static void SetMuted(bool value)
    {
        MUTED = value;

        Instance.audioSource.mute = value;
        Instance.musicSource.mute = value;
    }

    public static Sound GetSound(string key)
    {
        return Instance.sounds.ContainsKey(key) ? Instance.sounds[key] : null;
    }

    /**
     * Description: Plays a sound effect
     * How to call: SoundController.PlaySound("button");
     */
    public static void PlaySound(string key)
    {
        Sound sound = GetSound(key);

        if (sound != null && !MUTED)
        {
            Instance.audioSource.volume = sound.volume;
            Instance.audioSource.pitch = sound.pitch;
            Instance.audioSource.PlayOneShot(sound.audioClip);
        }
    }

    #endregion
}