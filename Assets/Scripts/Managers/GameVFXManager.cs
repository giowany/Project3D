using EBAC.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VFX
{
    public enum VFXType
    {
        GUN,
        LIFEPACK,
        COIN
    }

    public class GameVFXManager : Singleton<GameVFXManager>
    {
        public List<GameVFXSetup> setups;

        public void PlayVFXForType(VFXType vFXType)
        {
            var s = setups.Find(i => i.VFXType == vFXType);

            if (s != null)
            {
                s.particleSystem.Play();
            }
        }
    }

    [System.Serializable]
    public class GameVFXSetup
    {
        public VFXType VFXType;
        public ParticleSystem particleSystem;
    }
}