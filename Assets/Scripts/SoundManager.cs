using UnityEngine;
using UnityEngine.Audio;
using System;


public class SoundManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private Sound[] _sounds;
    [SerializeField] private AudioSource _sourse;

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
    }

    void Start()
    {
        BasePickupController.PlayPickUpSound = OnPlayPickUpSound;
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

    #endregion
}
