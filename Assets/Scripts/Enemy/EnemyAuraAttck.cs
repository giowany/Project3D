using DG.Tweening;
using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy 
{
    public class EnemyAuraAttck : InitialEnemy
    {
        public GunBase gun;

        public PlayerControler player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Player"))
            {
                isAttack = true;
                gun.StartShoot();
            }
        }

        private void OnTriggerExit(Collider other)
        {
                gun.StopShoot();
                isAttack = false;
        }

        public void LookAtPlayr()
        {
            if (player != null)
                transform.LookAt(player.transform.position);
        }

        protected override void Update()
        {
            base.Update();
            if (isAttack)
                LookAtPlayr();
        }
    }
}