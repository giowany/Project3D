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
        ATTACK
    }

    public class BossBase : MonoBehaviour
    {
        public float speed = 5f;
        public List<Transform> waypoints;

        [Header("Animation")]
        public float animationDuration = .5f;
        public Ease ease = Ease.OutBack;

        private StateMachine<BossAction> stateMachine;
        private Tween _currTween;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
        }

        #region
        public void GoToRandomPoint()
        {
            StartCoroutine(GoToPointCoroutine(waypoints[Random.Range(0, waypoints.Count)]));
        }

        IEnumerator GoToPointCoroutine(Transform t)
        {
            while(Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
        }
        #endregion

        #region ANIMATION
        public void StateInitAnimation()
        {
            transform.localScale = Vector3.one;
            if(_currTween == null) _currTween.Kill();
            _currTween = transform.DOScale(0, animationDuration).SetEase(ease).From();
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
        #endregion

        #region STATE MACHINE
        public void SwitchStatte(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
        #endregion
    }
}