using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;
using UnityEngine.PlayerLoop;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public float startLife = 10f;
        public Collider colliderE;
        public FlashColor flashColor;
        public ParticleSystem enemyParticleSystem;
        public float damage = 5;

        [SerializeField]private float _currentLife;
        [SerializeField]private AnimationBase _animationBase;
        protected bool isDead = false;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        #region Unity Functions
        private void Awake()
        {
            Init();
        }

        private  void OnCollisionEnter(Collision collision)
        {
            PlayerControler p = collision.transform.GetComponent<PlayerControler>();

            if(p != null)
            {
                Vector3 dir = collision.transform.position - transform.position;
                dir = -dir.normalized;
                dir.y = 0;

                p.Damage(damage, dir);
            }
        }
        #endregion

        #region Functions
        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            if (startWithBornAnimation)
                BornAniation();
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if(colliderE != null) colliderE.enabled = false;
            isDead = true;
            EnemyManager.instance.SpawEnemies();
            Destroy(gameObject, 3f);
            PlayAnimationByType(AnimationType.DEATH);
        }

        public void OnDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (enemyParticleSystem != null) enemyParticleSystem.Emit(10);

            _currentLife -= f;

            if (_currentLife <= 0)
            {
                Kill();
            }
        }
        #endregion

        #region ANIMATION
        private void BornAniation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByType(AnimationType animationType)
        {
            _animationBase.PlayAnimationByType(animationType);
        }
        #endregion

        public void Damage(float damage)
        {
            OnDamage(damage);
        }
        
        public void Damage(float damage, Vector3 dir)
        {
            transform.DOMove(transform.position - dir, .1f);
            OnDamage(damage);
        }
    }
}