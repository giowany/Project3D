using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gun;
    public Transform gunPosition;

    private GunBase _currentGun;

    private void CreatGun()
    {
        _currentGun = Instantiate(gun, gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    protected override void Init()
    {
        base.Init();
        CreatGun();
        inputs.GamePlay.Shoot.performed += ctx => StartShoot();
        inputs.GamePlay.Shoot.canceled += ctx => StopShoot();
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
    }

    private void StopShoot()
    {
        _currentGun.StopShoot();
    }
}
