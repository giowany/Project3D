using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Itens
{
    public class CollectCoin : CollectBase
    {
        [SerializeField] private ParticleSystem _particleSystem;
        public AudioSource AudioSource;

        private void Start()
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }
        private void DisableItem()
        {
            gameObject.SetActive(false);
        }

        protected override void OnCollect()
        {
            base.OnCollect();
            if(_particleSystem != null) _particleSystem.Play();
            if (AudioSource != null)  AudioSource.Play();
            if (_particleSystem != null)  Invoke("DisableItem", _particleSystem.main.duration);
        }

    }
}