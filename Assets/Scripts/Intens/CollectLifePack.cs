using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itens
{
    public class CollectLifePack : CollectBase
    {
        public Action<CollectLifePack> onCollect;
        public GameObject text;
        public Transform container;

        private void Start()
        {
            onCollect += EnableText;
        }

        private void EnableText(CollectLifePack collectLifePack)
        {
            Instantiate(text, container);
        }

        protected override void OnCollect()
        {
            base.OnCollect();
            onCollect?.Invoke(this);
        }
    }
}