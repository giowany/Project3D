using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMultiShoot : GunFireLimit
{
    public int amoutPerShoot = 4;
    public float angle = 15f;

    protected override void Shoot()
    {
        int mult = 0;

        for(int i = 0; i < amoutPerShoot; i++)
        {
            if (i % 2 == 0)
                mult++;

            var projectile = Instantiate(projectileBase, positionToshoot);
            projectile.transform.localPosition = Vector3.zero;
            projectile.transform.localEulerAngles = Vector3.zero + Vector3.up * (i % 2 == 0 ? angle : -angle) * mult;
            projectile.speed = speed;
            projectile.transform.parent = null;
        }
    }
}
