using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EBAC.StateMachine;
using DG.Tweening;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        public float speed = 5f;
        public List<Transform> waypoints;

        public PlayerControler player;

        [Header("Animation")]
        public float animationDuration = .5f;
        public Ease ease = Ease.OutBack;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;
        public GunBase gun;

        public HealthBase health;

        private StateMachine<BossAction> stateMachine;
        private Tween _currTween;
        private Coroutine _currCoroutine;

        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            SwitchStatte(BossAction.INIT);
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());


            health.onKill += OnBossKill;
        }

        public void StopCoroutine()
        {
            if (_currCoroutine != null)
                StopCoroutine(_currCoroutine);
        }

        #region Helath
        private void OnBossKill(HealthBase health)
        {
            SwitchStatte(BossAction.DEATH);
        }
        #endregion

        #region Attack
        public void StartAttack(Action endAttack = null)
        {
            _currCoroutine = StartCoroutine(AttackCoroutine(endAttack));
        }

        IEnumerator AttackCoroutine(Action endAttack = null)
        {
            int attacks = 0;
            while (attacks < attackAmount) 
            {
                attacks++;
                transform.localScale = Vector3.one;
                transform.LookAt(player.transform.position);
                gun.StartShoot();
                transform.DOScale(1.5f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            endAttack?.Invoke();
        }

        public void StopShoot()
        {
            gun.StopShoot();
        }

        #endregion

        #region Walk
        public void GoToRandomPoint(Action onArrive = null)
        {
            _currCoroutine = StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t = null, Action onArrive = null)
        {
            while(Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.LookAt(t.position);
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }

            onArrive?.Invoke();
        }
        #endregion

        #region ANIMATION
        public void StateInitAnimation(Action endAnimation)
        {
            transform.localScale = Vector3.one;
            if(_currTween != null) _currTween.Kill();
            _currTween = transform.DOScale(0, animationDuration).SetEase(ease).From();
            
            endAnimation?.Invoke();
        }
        #endregion

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchStatte(BossAction.INIT);
        }

        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchStatte(BossAction.WALK);
        }
        
        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchStatte(BossAction.ATTACK);
        }
        #endregion

        #region STATE MACHINE
        public void SwitchStatte(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
        #endregion
    }
}