using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    private float volume;

    public void ChangeVolume(float v)
    {
        volume = v;
        AudioListener.volume = volume;
    }
}
