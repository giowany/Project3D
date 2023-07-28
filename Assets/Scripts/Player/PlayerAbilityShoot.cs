using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<GunBase> gun;
    public Transform gunPosition;

    [SerializeField]private List<GunBase> _currentGuns;
    private int _currentGunIndex = 0;

    private void CreatGun()
    {
        foreach(var i  in gun)
        {
            _currentGuns.Add(Instantiate(i, gunPosition));
            i.transform.localPosition = i.transform.localEulerAngles = Vector3.zero;
        }
    }

    protected override void Init()
    {
        base.Init();
        CreatGun();
        OnSwitchGun();
        inputs.GamePlay.Shoot.performed += ctx => StartShoot();
        inputs.GamePlay.Shoot.canceled += ctx => StopShoot();
    }

    private void StartShoot()
    {
        _currentGuns[_currentGunIndex].StartShoot();
    }

    private void StopShoot()
    {
        _currentGuns[_currentGunIndex].StopShoot();
    }

    private void SwitchGuns()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            _currentGunIndex = 0;
            OnSwitchGun(_currentGunIndex);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            _currentGunIndex = 1;
            OnSwitchGun(_currentGunIndex);
        }
    }

    private void OnSwitchGun(int i = 0)
    {
        foreach (var gun in _currentGuns)
            gun.gameObject.SetActive(false);
        _currentGuns[_currentGunIndex].gameObject.SetActive(true);
        _currentGuns[_currentGunIndex].OnReload();
    }

    private void Update()
    {
        SwitchGuns();
    }
}
