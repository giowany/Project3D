using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Itens
{
    public class CollectCoin : CollectBase
    {
        [SerializeField] private ParticleSystem _coin;
        public AudioSource coinAudioSource;

        private void Start()
        {
            _coin = GetComponentInChildren<ParticleSystem>();
        }
        private void DisableItem()
        {
            gameObject.SetActive(false);
        }

        protected override void OnCollect()
        {
            base.OnCollect();
            if(_coin != null) _coin.Play();
            if (coinAudioSource != null)  coinAudioSource.Play();
            if (_coin != null)  Invoke("DisableItem", _coin.main.duration);
        }

    }
}