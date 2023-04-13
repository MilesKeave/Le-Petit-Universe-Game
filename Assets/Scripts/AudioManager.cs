using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgmAudio;
    public AudioMixerGroup soundMixerGroup;
    public AudioMixerGroup musicMixerGroup;
    private AudioSource soundAudio;



    private const string ResourcesPath = "Sounds/";

    public const string BGM = "BGM";

    public const string BUTTON_SOUND = "ClickButton";

    private List<AudioSource> audioSources = new List<AudioSource>();
    private List<AudioSource> playingSources = new List<AudioSource>();

    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if(instance != null)
                    instance.Init();
            }
            if (instance == null)
            {
                GameObject obj = new GameObject("AudioManager");
                instance =  obj.AddComponent<AudioManager>();
                instance.Init();
            }
            return instance;
        }
    }

    const float VolumeRange = 50;

    public float MusicVolume
    {
        get
        {
            float v;
            musicMixerGroup.audioMixer.GetFloat("MusicVolume", out v);
            return (v + VolumeRange) / VolumeRange;
        }

        set
        {
            musicMixerGroup.audioMixer.SetFloat("MusicVolume", value * VolumeRange - VolumeRange);
        }
    }

    public float SoundVolume
    {
        get
        {
            float v;
            soundMixerGroup.audioMixer.GetFloat("SoundVolume", out v);
            return (v + VolumeRange) / VolumeRange;
        }

        set
        {
            soundMixerGroup.audioMixer.SetFloat("SoundVolume", value * VolumeRange - VolumeRange);
        }
    }


    private void Init()
    {
        bgmAudio = gameObject.AddComponent<AudioSource>();
        soundAudio = gameObject.AddComponent<AudioSource>();
        soundMixerGroup = Resources.Load<AudioMixer>(ResourcesPath + "AudioMixerGroup").FindMatchingGroups("Sounds")[0];
        musicMixerGroup = Resources.Load<AudioMixer>(ResourcesPath + "AudioMixerGroup").FindMatchingGroups("Music")[0];
        soundAudio.outputAudioMixerGroup = soundMixerGroup;
        DontDestroyOnLoad(gameObject);
        
    }

    public void PlaySound(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(ResourcesPath + name);
        AudioSource source = GetAudioSource();
        playingSources.Add(source);
        source.clip = clip;
        source.Play();
        StartCoroutine(nameof(DelayRecycleSource), source);
    }

    public void StopSound(string name)
    {
        Debug.Log("StopSound " + name);
        AudioSource source = playingSources.Find(s => s.clip.name == name);
        if (source == null) return;

        source.Stop();
        source.clip = null;
        playingSources.Remove(source);
        audioSources.Add(source);
    }

    public void StopAllSounds()
    {
        for (int i = 0; i < playingSources.Count; i++)
        {
            StopSound(playingSources[i].clip.name);
        }
    }

    public void PlayBGM(string name)
    {
        Debug.Log("Play BGM " + name);
        StartCoroutine(nameof(ChangeBGM), name);
    }

    public void StopBGMImmediately()
    {
        bgmAudio.Stop();
        bgmAudio.clip = null;
    }

    public void StopBGM()
    {
        StartCoroutine(nameof(SmoothStopBGM));
    }

    private IEnumerator SmoothStopBGM()
    {
        if (bgmAudio.isPlaying)
        {
            while (bgmAudio.volume > 0.1f)
            {
                bgmAudio.volume -= Time.deltaTime;
                yield return 0;
            }
            bgmAudio.Stop();
            bgmAudio.clip = null;
        }
    }

    private IEnumerator ChangeBGM(string name)
    {
        if (bgmAudio.clip != null && bgmAudio.clip.name == name)
        {
            yield break;
        }
        AudioClip clip = Resources.Load<AudioClip>(ResourcesPath + name);
        if (clip == null)
        {
            Debug.LogError(name + "不存在");
            yield break;
        }

        if (bgmAudio.isPlaying)
        {
            while (bgmAudio.volume > 0.15f)
            {
                bgmAudio.volume -= Time.deltaTime / 2;
                yield return 0;
            }
            Debug.Log("stop sound " + bgmAudio.clip.name);
            bgmAudio.Stop();
            yield return 0;
        }
        bgmAudio.clip = clip;
        bgmAudio.volume = 1;
        bgmAudio.outputAudioMixerGroup = musicMixerGroup;
        bgmAudio.loop = true;
        bgmAudio.Play();
        Debug.Log("playing sound " + bgmAudio.clip.name);
    }

    private AudioSource GetAudioSource()
    {
        if (audioSources.Count == 0)
        {
            GameObject newAudioObj = new GameObject("AudioSource");
            newAudioObj.transform.parent = transform;
            AudioSource newSource = newAudioObj.AddComponent<AudioSource>();
            newSource.outputAudioMixerGroup = soundMixerGroup;
            audioSources.Add(newSource);
        }

        AudioSource source = audioSources[0];
        audioSources.RemoveAt(0);
        return source;
    }

    private IEnumerator DelayRecycleSource(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        if (!playingSources.Contains(source)) yield break;
        source.clip = null;
        source.Stop();
        audioSources.Add(source);
        playingSources.Remove(source);
    }
}
