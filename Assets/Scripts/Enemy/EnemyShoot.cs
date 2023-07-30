using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gun;
        public Transform player;

        private void Update()
        {
            transform.LookAt(player);
        }

        private void Start()
        {
            gun.StartShoot();
        }
    }
}