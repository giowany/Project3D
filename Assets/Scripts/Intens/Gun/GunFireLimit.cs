using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GunFireLimit : GunBase
{
    public List<UIUpdater> updaterList;
    public float maxBullet = 5f;
    public float timeToReload = 1f;
    public string tagGun = "gun";

    [SerializeField] private float _currentShoots;
    [SerializeField] private bool _reloading;

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
                UpdateUi(1 - (_currentShoots / maxBullet));
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
            UpdateUi(time / timeToReload);
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _reloading = false;
    }

    private void UpdateUi(float value)
    {
        foreach (UIUpdater updater in updaterList)
        {
            if (updater.CompareTag(tagGun))
            {
                updater.UpdateValue(value);
            }
        }
    }

    private void GetAllUIs()
    {
        updaterList = new List<UIUpdater>();

        updaterList = GameObject.FindObjectsOfType<UIUpdater>().ToList();
    }

    public override void OnReload()
    {
        Reload();
    }
}
