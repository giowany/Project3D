using EBAC.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Audio
{
    public class MusicPlayer : Singleton<MusicPlayer>
    {
        public MusicType type;
        public AudioSource audioSource;

        private MusicSetup _currMusicSetup;
        [SerializeField] private bool _activate = false;

        private void Start()
        {
            Play();
        }

        private void Play()
        {
            if (_activate) return;

            _currMusicSetup = SoundManager.instance.GetMusicByType(type);
            audioSource.clip = _currMusicSetup.audioClip.clip;
            audioSource.volume = _currMusicSetup.audioClip.volume;
            audioSource.Play();
        }

        public void activete(bool b)
        {
            _activate = b;
            if (b) audioSource.Stop();
            else audioSource.Play();
        }

        public bool GetActivate()
        {
            return _activate;
        }

        private void ChangeVolume()
        {
            audioSource.volume = _currMusicSetup.audioClip.volume;
        }

        private void Update()
        {
            ChangeVolume();
        }
    }
}