using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GunFireLimit : GunBase
{
    public List<UIUpdater> updaterList;
    public float ammo = 5f;
    public float timeToReload = 1f;
    public string tagGun = "gun";

    [SerializeField] private float _currentAMMO;
    [SerializeField] private bool _reloading;

    private void Awake()
    {
        GetAllUIs();
    }

    protected override void Init()
    {
        _currentAMMO = ammo;
    }

    protected override IEnumerator ShootCadence()
    {
        if(_reloading) yield break;

        while (true)
        {
            if(_currentAMMO > 0)
            { 
                Shoot();
                _currentAMMO--;
                if(_currentAMMO == 0) CheckReaload();
                UpdateUi((_currentAMMO / ammo));
                
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void CheckReaload()
    {
        if(_currentAMMO < ammo)
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
        _currentAMMO = ammo;
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
        CheckReaload();
    }
}
