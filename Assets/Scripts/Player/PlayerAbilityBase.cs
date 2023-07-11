using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityBase : MonoBehaviour
{
    protected PlayerControler player;
    protected Inputs inputs;

    private void OnValidate()
    {
        if(player == null) player = GetComponent<PlayerControler>();
    }

    private void Start()
    {
        inputs = new Inputs();
        inputs.Enable();

        Init();
        OnValidate();
        RegisterListeners();
    }
    private void OnEnable()
    {
        if(inputs != null)
            inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void OnDestroy()
    {
        UnregisterListeners();
    }

    protected virtual void Init() { }

    protected virtual void RegisterListeners() { }

    protected virtual void UnregisterListeners() { }
}
