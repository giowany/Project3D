using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<GunBase> gun;
    public Transform gunPosition;

    private GunBase _currentGun;
    private int _currentGunIndex = 0;

    private void CreatGun()
    {
        if(_currentGun != null) Destroy(_currentGun);

        _currentGun = Instantiate(gun[_currentGunIndex], gunPosition);

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

    private void SwitchGuns()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
                _currentGunIndex = 0;
                CreatGun();
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
                _currentGunIndex = 1;
                CreatGun();
        }
    }

    private void Update()
    {
        SwitchGuns();
    }
}
