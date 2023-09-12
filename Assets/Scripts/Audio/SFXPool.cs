using Audio;
using EBAC.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using VFX;

public class SFXPool : Singleton<SFXPool>
{
    public int poolSize = 10;
    public AudioMixerGroup mixerGroup;

    private List<AudioSource> _audioSources;
    private int _index = 0;
    private SFXSetup _sfxSetup;
    private bool _activate = false;

    private void Start()
    {
        CreatPool();
        GetSetup(SFXType.COIN);
    }

    private void CreatPool()
    {
        _audioSources = new List<AudioSource>();

        for(int i = 0; i< poolSize; i++)
        {
            CreatAudioSourceItem();
        }
    }

    private void CreatAudioSourceItem()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        _audioSources.Add(go.AddComponent<AudioSource>());
    }

    private void GetSetup(SFXType sfxType)
    {
        _sfxSetup = SoundManager.instance.GetSFXByType(sfxType);
    }

    public void Play(SFXType sfxType)
    {
        if (_activate) return;
        if (sfxType == SFXType.NONE) return;

        GetSetup(sfxType);

        _audioSources[_index].clip = _sfxSetup.audioClip.clip;
        _audioSources[_index].outputAudioMixerGroup = mixerGroup;
        _audioSources[_index].Play();

        _index++;
        if (_index >= _audioSources.Count) _index = 0;
    }

    public void activete(bool b)
    {
        _activate = b;
    }

    public bool GetActivate()
    {
        return _activate;
    }

    private void Update()
    {
        ChangeVolume();
    }

    private void ChangeVolume()
    {
        if (_sfxSetup == null) return;

        foreach (var item in _audioSources)
        {
            item.volume = _sfxSetup.audioClip.volume;
        }
    }
}
