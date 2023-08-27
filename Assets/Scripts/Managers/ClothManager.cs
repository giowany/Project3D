using EBAC.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skins
{
    public enum clothType
    {
        BASIC,
        ALTERNATIVE,
        SPEED,
        DAMAGE
    }

    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> setups;


        public SkinnedMeshRenderer mesh;
        public clothType clothType;

        [SerializeField] private ClothSetup _setup;

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void Init()
        {
            ChangeTextureByType(clothType.BASIC);
        }

        public void ChangeTextureByType(clothType clothType)
        {
            _setup = setups.Find(i => i.clothType == clothType);
            mesh.sharedMaterials[0].SetTexture(_setup.shaderIdName, _setup.texture);
        }

        public float BunosDamager()
        {
            return _setup.bonusDamager;

        }

        public float BonusSpeed()
        {
            return _setup.bonusSpeed;
        }

        [NaughtyAttributes.Button]
        private void Test()
        {
            ChangeTextureByType(clothType);
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public clothType clothType;
        public string shaderIdName = "_EmissionMap";
        public Texture2D texture;
        public float bonusDamager = 0f;
        public float bonusSpeed = 0f;
    }
}