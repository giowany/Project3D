using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase projectileBase;
    public Transform positionToshoot;
    public float timeBetweenShoot = .3f;
    public float speed = 50f;

    private Coroutine _currentCorotine;

    public void StartShoot()
    {
        _currentCorotine = StartCoroutine(ShootCadence());
    }

    public void StopShoot()
    {
        if (_currentCorotine != null)
            StopCoroutine(_currentCorotine);
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
    }
}
