using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFX;

namespace Itens
{

    public class CollectBase : MonoBehaviour
    {
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
            Destroy(gameObject, 3f);

        }
    }
}