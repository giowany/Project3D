using EBAC.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss = (BossBase)objs[0];
        }
    }


    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.StateInitAnimation(EndAnimation);
        }

        private void EndAnimation()
        {
            boss.SwitchStatte(BossAction.WALK);
        }
    }

    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.GoToRandomPoint(OnArrive);
        }

        private void OnArrive()
        {
            boss.SwitchStatte(BossAction.ATTACK);
        }

        public override void OnStateExit()
        {
            boss.StopCoroutine();
        }
    }

    public class BossStateAttack : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.StartAttack(EndAttack);
        }

        private void EndAttack()
        {
            boss.SwitchStatte(BossAction.WALK);
        }

        public override void OnStateExit()
        {
            boss.StopCoroutine();
            boss.StopShoot();
        }
    }

    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.transform.localScale = Vector3.one * .2f;
            boss.winGame.AnimationButtons();
        }
    }

}