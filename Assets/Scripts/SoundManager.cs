using UnityEngine;
using UnityEngine.Audio;
using System;


public class SoundManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private Sound[] _sounds;
    [SerializeField] private AudioSource _sourse;
    [SerializeField] private Sound _backgroundMusic;
    [SerializeField] private Sound _bossFightMusic;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        for (int i = 0; i < _sounds.Length; i++)
        {
            _sounds[i].Source = gameObject.AddComponent<AudioSource>();
            _sounds[i].Source.clip = _sounds[i].Clip;
            _sounds[i].Source.volume = _sounds[i].Volume;
            _sounds[i].Source.pitch = _sounds[i].Pitch;
        }
        _backgroundMusic.Source = gameObject.AddComponent<AudioSource>();
        _backgroundMusic.Source.clip = _backgroundMusic.Clip;
        _backgroundMusic.Source.volume = _backgroundMusic.Volume;
        _backgroundMusic.Source.pitch = _backgroundMusic.Pitch;
        _backgroundMusic.Source.loop = true;

        _bossFightMusic.Source = gameObject.AddComponent<AudioSource>();
        _bossFightMusic.Source.clip = _bossFightMusic.Clip;
        _bossFightMusic.Source.volume = _bossFightMusic.Volume;
        _bossFightMusic.Source.pitch = _bossFightMusic.Pitch;
        _bossFightMusic.Source.loop = true;
    }

    void Start()
    {
        BasePickupController.PlayPickUpSound = OnPlayPickUpSound;
        TriggerController.Triggered += OnBossFight;
        _backgroundMusic.Source.Play();
    }

    #endregion


    #region Methods

    private void OnPlayPickUpSound(AudioClip clip)
    {
        _sourse.PlayOneShot(clip);
    }

    public void PlaySoundByName(string name)
    {
        if (Array.Exists(_sounds, e => e.Name == name))
        {
            Sound sound = Array.Find(_sounds, e => e.Name == name);
            sound.Source.Play();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (Array.Exists(_sounds, e => e.Clip == clip))
        {
            Sound sound = Array.Find(_sounds, e => e.Clip == clip);
            sound.Source.Play();
        }
    }

    private void OnBossFight()
    {
        _backgroundMusic.Source.Stop();
        _bossFightMusic.Source.Play();
    }

    #endregion
}
