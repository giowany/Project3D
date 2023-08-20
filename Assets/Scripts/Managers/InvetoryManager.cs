using EBAC.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Itens
{
    public enum ItenType
    {
        COIN,
        LIFE_PACK
    }

    public class InvetoryManager : Singleton<InvetoryManager>
    {
        public List<InventorySetup> InventorySetups;
        public TextMeshProUGUI text;

        public void Start()
        {
            ResetValues();
        }

        public void ResetValues()
        {
            foreach(var i in InventorySetups)
            {
                i.soInt.value = 0;
            }
        }

        public InventorySetup GetItensForType(ItenType itenType)
        {
            return InventorySetups.Find(i => i.itenType == itenType);
        }
        
        public void AddItensForType(ItenType itenType, int amount = 1)
        {
            if (amount < 0) return;

            InventorySetups.Find(i => i.itenType == itenType).soInt.value += amount;

        }
        
        public void RemoveItensForType(ItenType itenType, int amount = 1)
        {
            if (amount < 0) return;

            var iten = InventorySetups.Find(i => i.itenType == itenType);
                iten.soInt.value -= amount;
            LayoutManager.instance.UpdateUI(itenType);

            if(iten.soInt.value < 0) iten.soInt.value = 0;
        }

    }

    [System.Serializable]
    public class InventorySetup
    {
        public ItenType itenType;
        public SOInt soInt;
        public Sprite sprite;
        public Texture texture;
    }
}