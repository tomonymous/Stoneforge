using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider slider;
    public AudioMixer musicMixer;
    public Slider musicSlider;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MainVolume",volume);
    }


    public void SetSlider(float volume)
    {
        audioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20);
        slider.value = volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }


    public void SetMusicSlider(float volume)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        musicSlider.value = volume;
    }
}
