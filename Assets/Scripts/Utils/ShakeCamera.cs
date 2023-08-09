using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public List<CinemachineVirtualCamera> virtualCamera;
    public float shakTime = .2f;

    [Header("Atributs")]
    public float amplitude = 3f;
    public float frequency = 3f;
    public float timeToShake = .3f;

    [SerializeField] private List<CinemachineBasicMultiChannelPerlin> perlin;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        ShakeToTime();
    }

    private void Init()
    {
        foreach(var cam in virtualCamera)
        {
            perlin.Add(cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>());
        }
    }

    public void Shake(float amplitude, float frequency, float time)
    {
        perlin.ForEach(i => i.m_AmplitudeGain = amplitude);
        perlin.ForEach(i => i.m_FrequencyGain = frequency);

        shakTime = time;
    }

    [NaughtyAttributes.Button]
    public void Shake()
    {
        Shake(amplitude, frequency, timeToShake);
    }

    private void ShakeToTime()
    {
        if (shakTime > 0) shakTime -= Time.deltaTime;
        else
        {
            perlin.ForEach(i => i.m_AmplitudeGain = 0f);
            perlin.ForEach(i => i.m_FrequencyGain = 0f);
        }
    }
}
