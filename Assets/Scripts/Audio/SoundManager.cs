using EBAC.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public enum SFXType
    {
        NONE,
        COIN,
        TYPE_02,
        TYPE_03
    }

    public enum MusicType
    {
        TYPE_01,
        TYPE_02,
        TYPE_03
    }

    public class SoundManager : Singleton<SoundManager>
    {
        public List<MusicSetup> musicSetup;
        public List<SFXSetup> sfxSetup;

        public AudioSource musicSource;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        public void PlayMusicByType(MusicType type)
        {
            var music = GetMusicByType(type);
            musicSource.clip = music.audioClip.clip;
            musicSource.Play();
        }

        public void PlaySFXByType(SFXType type)
        {
            var music = GetSFXByType(type);
            musicSource.clip = music.audioClip.clip;
            musicSource.Play();
        }

        public MusicSetup GetMusicByType(MusicType type)
        {
            return musicSetup.Find(i => i.musicType == type);
        }
        
        public SFXSetup GetSFXByType(SFXType type)
        {
            return sfxSetup.Find(i => i.sfxType == type);
        }

        public void ChangeVolumeMusic(float v)
        {
            foreach (var item in musicSetup)
            {
                item.audioClip.volume = v;
            }
        }

        public void ChangeVolumeVFX(float v)
        {
            foreach (var item in sfxSetup)
            {
                item.audioClip.volume = v;
            }
        }
    }

    [System.Serializable]
    public class SFXSetup
    {
        public SFXType sfxType;
        public AudioSource audioClip;
    }

    [System.Serializable]
    public class MusicSetup
    {
        public MusicType musicType;
        public AudioSource audioClip;
    }
}