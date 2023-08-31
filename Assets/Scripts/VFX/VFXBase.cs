using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXBase : MonoBehaviour
{
    public ParticleSystem particle;

    public void PlayPartcle()
    {
        particle.Stop();

        if (PlayerControler.instance.Isgrounded())
        {
            particle.Play();

        }
    }
}
