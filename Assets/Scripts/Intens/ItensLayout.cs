using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Itens
{
    public class ItensLayout : MonoBehaviour
    {
        public Image uiImage;
        public RawImage uiRawImage;
        public TextMeshProUGUI uiText;
        public ItenType itentype;

        private InventorySetup _curSetup;


        public void Load(InventorySetup setup)
        {
            _curSetup = setup;
            UpdateUI();
        }

        public void UpdateUI() 
        {
            if (uiImage != null) uiImage.sprite = _curSetup.sprite;
            if (uiRawImage != null) uiRawImage.texture = _curSetup.texture;
        }

        public void UpdateText()
        {
            uiText.text = _curSetup.soInt.value.ToString();
        }
    }
}