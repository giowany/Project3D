using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.VFX;
using VFX;

public class GunBase : MonoBehaviour
{
    public ProjectileBase projectileBase;
    public Transform positionToshoot;
    public float timeBetweenShoot = .3f;
    public float speed = 50f;
    
    
    [SerializeField] private float _bonusDamager = 0f;

    protected Coroutine _currentCorotine;

    public void ChangeBonus(float bonus)
    {
        _bonusDamager = bonus;
    }

    public virtual void StartShoot()
    {
        _currentCorotine = StartCoroutine(ShootCadence());
    }

    public virtual void StopShoot()
    {
        if (_currentCorotine != null)
        {
            StopCoroutine(_currentCorotine);
        }
    }

    protected virtual IEnumerator ShootCadence()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    protected virtual void Shoot()
    {
        var projectile = Instantiate(projectileBase);
        projectile.transform.position = positionToshoot.position;
        projectile.transform.rotation = positionToshoot.rotation;
        projectile.speed = speed;
        projectile.damageAmout += _bonusDamager;
        GameVFXManager.instance.PlayVFXForType(VFXType.GUN);
        SFXPool.instance.Play(Audio.SFXType.SHOOT);
    }

    public virtual void OnReload() { }
    protected virtual void Init() { }
}
