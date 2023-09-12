using Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFX;

namespace Itens
{

    public class CollectBase : MonoBehaviour
    {
        public SFXType sfxType;
        public ItenType itenType;
        public string tagPlayer = "Player";
        public GameObject render;


        private void OnTriggerEnter(Collider other)
        {
            var c = GetComponent<Collider>();

            if (other.transform.CompareTag(tagPlayer))
            {
                Collect();
                c.enabled = false;
            }
        }

        protected virtual void Collect()
        {
            render.SetActive(false);
            OnCollect();
        }

        protected virtual void OnCollect()
        {
            InvetoryManager.instance.AddItensForType(itenType);
            PlaySFX();
            Destroy(gameObject, 3f);

        }

        private void PlaySFX()
        {
            SFXPool.instance.Play(sfxType);
        }
    }
}