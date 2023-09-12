using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMasterVolume : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        UpdateValue();
    }

    public void UpdateValue()
    {
        volumeSlider.value = SaveManager.instance.SaveSetup.masterVolume;
    }
}
