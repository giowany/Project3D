using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLifePack : ActionBase
{

    public int amountRecover = 10;

    private Inputs _inputs;
    protected override void RecoverLife()
    {
        if (PlayerControler.instance.healthBase.CurrentLife() == PlayerControler.instance.healthBase.startLife) return;

        if (soInt.value > 0)
            PlayerControler.instance.healthBase.ResetLife();
        base.RecoverLife();
    }

    public void Init()
    {
        _inputs = new Inputs();
        _inputs.GamePlay.Enable();

        _inputs.GamePlay.Recover.performed += ctx => RecoverLife();
    }

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        if (_inputs != null)
            _inputs.GamePlay.Enable();
    }

    private void OnDisable()
    {
        _inputs.GamePlay.Disable();
    }
}
