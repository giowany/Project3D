using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gun;

    protected override void Init()
    {
        base.Init();

        inputs.GamePlay.Shoot.performed += ctx => StartShoot();
        inputs.GamePlay.Shoot.canceled += ctx => StopShoot();
    }

    private void StartShoot()
    {
        gun.StartShoot();
    }

    private void StopShoot()
    {
        gun.StopShoot();
    }
}
