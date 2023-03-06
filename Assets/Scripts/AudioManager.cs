using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;
    public AudioMixerGroup musicGroup;
    public bool resume;
    public bool tutorial;
    public Sound[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
        mixerGroup.audioMixer.SetFloat("MainVolume", Mathf.Log10(PlayerPrefs.GetFloat("MainVolume")) * 20);
        musicGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
        foreach (Sound s in sounds)
		{

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup;
            
		}
        int resetNumber = 98;
        if (PlayerPrefs.GetInt("Reset Check") < resetNumber)
        {
            //PlayerPrefs.DeleteAll();
            PlayerPrefs.SetFloat("MainVolume", 1);
            PlayerPrefs.SetFloat("MusicVolume", 1);
            PlayerPrefs.SetInt("Reset Check", resetNumber);
        }
    }

    private void Start()
    {
        string randomTheme = "Music0" + UnityEngine.Random.Range(1, 4);
        Play(randomTheme);
    }
    public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        if (s.name.Length > 5 && s.name.Substring(0, 5) == "Music")
        {
            s.source.Play();
        }
        else
        {
            s.source.PlayOneShot(s.clip);
        }
    }

    public void PlayChaosMusic()
    {
        foreach(Sound s in sounds)
        {
            if (s.name.Length > 5 && s.name.Substring(0, 5) == "Music" && s.source.isPlaying)
            {
                s.source.Stop();
            }
        }
        Play("Music04");
    }

    public void PlayThemeMusic()
    {
        foreach (Sound s in sounds)
        {
            if (s.name.Length > 5 && s.name.Substring(0, 5) == "Music" && s.source.isPlaying)
            {
                s.source.Stop();
            }
        }
        string randomTheme = "Music0" + UnityEngine.Random.Range(1, 4);
        Play(randomTheme);
    }


}
