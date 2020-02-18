﻿using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound
{
    #region Fields

    [HideInInspector] public AudioSource Source;

    public AudioClip Clip;

    public string Name;

    [Range(0.0f, 1.0f)]
    public float Volume;
    [Range(0.1f, 3.0f)]
    public float Pitch;

    #endregion
}
