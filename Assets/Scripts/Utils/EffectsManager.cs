using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EffectsManager : MonoBehaviour
{
    public PostProcessVolume postProcess;

    private Vignette _vignette;

    public void ChangeVignette(float f)
    {
        Vignette temp;

        if(postProcess.profile.TryGetSettings<Vignette>(out temp))
        {
            _vignette = temp;
        }

        FloatParameter parameter = new FloatParameter();

        parameter.value = f;

        _vignette.intensity.Override(parameter);
    }
}
