using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    public int checkPointKey = 0;
    public string checkPointNameKey = "checkpoint";
    public MeshRenderer checkPointRenderer;

    private bool _checkPointActivated = false;

    public void ActiveCheckPoint()
    {
        TurnOn();
        _checkPointActivated = true;
        CheckPointManager.instance.SaveCheckPoint(checkPointKey, checkPointNameKey);
    }

    private void TurnOn()
    {
        checkPointRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    private void TurnOff()
    {
        checkPointRenderer.material.SetColor("_EmissionColor", Color.grey);
    }

    private void Awake()
    {
        TurnOff();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_checkPointActivated && other.transform.CompareTag("Player"))
        {
            ActiveCheckPoint();
        }
    }
}
