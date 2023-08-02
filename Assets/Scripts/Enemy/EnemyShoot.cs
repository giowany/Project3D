using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : InitialEnemy
    {
        public GunBase gun;
        public Transform player;

        public void StartShoot()
        {
            gun.StartShoot();
        }

        public void StopShoot()
        {
            gun.StopShoot();
        }

        public void LookAtPlayr()
        {
            transform.LookAt(player);
        }
    }
}