using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouseGame : MonoBehaviour
{
    public MenuButtonsManager menu;

    private Inputs _inputs;
    private bool _enabled = false;

    private void Init()
    {
        _inputs.Menu.Pause.performed += ctx => Pause();
    }
    private void Start()
    {
        _inputs = new Inputs();
        _inputs.Menu.Enable();

        Init();
    }
    private void OnEnable()
    {
        if (_inputs != null)
            _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Menu.Disable();
    }

    public void Pause()
    {
        _enabled = !_enabled;
        if (_enabled) menu.AnimationButtons();
        else if(!_enabled) menu.HideButtons();
    }
}
