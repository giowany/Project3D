using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GunFireLimit : GunBase
{
    public List<UIGunUpdater> updaterList;
    public float maxBullet = 5f;
    public float timeToReload = 1f;

    private float _currentShoots;
    [SerializeField]private bool _reloading;

    private void Awake()
    {
        GetAllUIs();
    }

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
                UpdateUi();
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
            updaterList.ForEach(i => i.UpdateValue(time/timeToReload));
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _reloading = false;
    }

    private void UpdateUi()
    {
        updaterList.ForEach(i => i.UpdateValue(maxBullet, _currentShoots));
    }

    private void GetAllUIs()
    {
        updaterList = GameObject.FindObjectsOfType<UIGunUpdater>().ToList();
    }

    public override void OnReload()
    {
        Reload();
    }
}
