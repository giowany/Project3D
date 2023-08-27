using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Skins
{
    public class ClothChange : MonoBehaviour
    {
        public clothType clothType;
        public GameObject text;
        public GameObject description;

        private Inputs _inputs;
        private bool _isActive = false;

        private void Start()
        {
            Init();
        }

        private void OnEnable()
        {
            if (_inputs != null)
                _inputs.GamePlay.Enable();
        }

        private void OnDisable()
        {
            _inputs.GamePlay.Disable();
        }

        private void Init()
        {
            _inputs = new Inputs();
            _inputs.GamePlay.Enable();

            _inputs.GamePlay.Interact.performed += ctx => ChangeCloth();
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerControler p = other.transform.GetComponent<PlayerControler>();
            if (p != null)
            {
                text.SetActive(true);
                description.SetActive(false);
                _isActive = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            PlayerControler p = other.transform.GetComponent<PlayerControler>();
            if (p != null)
            {
                text.SetActive(false);
                description.SetActive(true);
                _isActive = false;
            }
        }

        private void ChangeCloth()
        {
            if (!_isActive) return;

            ClothManager.instance.ChangeTextureByType(clothType);
        }
    }
}