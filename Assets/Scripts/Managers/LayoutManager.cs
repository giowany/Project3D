using EBAC.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Itens
{
    public class LayoutManager : Singleton<LayoutManager>
    {
        public List<ItensLayout> prefabIten;
        public Transform Container;

        public List<ItensLayout> layouts;

        private void Start()
        {
            CreatItens();
        }

        private void CreatItens()
        {
           foreach(var setup in InvetoryManager.instance.InventorySetups)
            {
                var iten = Instantiate(prefabIten.Find(i => i.itentype == setup.itenType), Container);
                iten.Load(setup);
                layouts.Add(iten);
            }
        }

        public void UpdateUI(ItenType itenType)
        {
            layouts.Find(i => i.itentype == itenType).UpdateText();
        }


    }
}