using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMusicVolume : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle toggle;

    private void Start()
    {
        UpdateValue();
    }

    public void UpdateValue()
    {
        volumeSlider.value = SaveManager.instance.SaveSetup.musicVolume;
        toggle.isOn = SaveManager.instance.SaveSetup.sfxOn;
    }
}
