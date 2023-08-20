using Itens;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    public float timeToDisable = 3f;

    private void Awake()
    {
        Invoke(nameof(DisableText), timeToDisable);
    }

    private void DisableText()
    {
        gameObject.SetActive(false);
    }
}
