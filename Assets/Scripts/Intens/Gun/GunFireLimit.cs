using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFireLimit : GunBase
{
    public float maxBullet = 5f;
    public float timeToReload = 1f;

    private float _currentShoots;
    [SerializeField]private bool _reloading;

    protected override IEnumerator ShootCadence()
    {
        if(_reloading) yield break;

        while (true)
        {
            if(_currentShoots < maxBullet) 
            { 
                Shoot();
                _currentShoots++;
                CheckReaload();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void CheckReaload()
    {
        if(_currentShoots >= maxBullet)
        {
            StopShoot();
            Reload();
        }
    }

    private void Reload()
    {
        _reloading = true;
        StartCoroutine(ReloadCorotine());
    }

    IEnumerator ReloadCorotine()
    {
        float time = 0;
        while (time < timeToReload)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _reloading = false;
    }
}
