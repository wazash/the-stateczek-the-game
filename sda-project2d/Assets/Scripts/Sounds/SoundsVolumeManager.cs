using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundsVolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private const string MASTER_KEY = "MasterVolume";
    private const string MUSIC_KEY = "MusicVolume";
    private const string SFX_KEY = "SFXVolume";

    private void OnEnable()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);

        masterSlider.value = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(SFX_KEY, 1f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(MASTER_KEY, masterSlider.value);
        PlayerPrefs.SetFloat(MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(SFX_KEY, sfxSlider.value);

        masterSlider.onValueChanged.RemoveAllListeners();
    }

    private void Start()
    {
        SetMasterVolume(masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetSfxVolume(sfxSlider.value);
    }

    private void SetMasterVolume(float value)
    {
        mixer.SetFloat(MASTER_KEY, Mathf.Log10(value) * 20);
    }

    private void SetMusicVolume(float value)
    {
        mixer.SetFloat(MUSIC_KEY, Mathf.Log10(value) * 20);
    }
    private void SetSfxVolume(float value)
    {
        mixer.SetFloat (SFX_KEY, Mathf.Log10(value) * 20);
    }

}
