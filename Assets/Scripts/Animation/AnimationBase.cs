using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Animation
{
    public enum AnimationType
    {
        NONE,
        IDLE,
        RUN,
        ATTACK,
        DEATH,
        SEMMEXER
    }

    public class AnimationBase : MonoBehaviour
    {
        public List<AnimationSetup> setups;
        public Animator animator;

        public void PlayAnimationByType(AnimationType animationType)
        {
            var setup = setups.Find(i => i.animationType == animationType);
            if (setup != null)
            {
                animator.SetTrigger(setup.trigger);
            }
        }
    }

    [System.Serializable]
    public class AnimationSetup
    {
        public AnimationType animationType;
        public string trigger;
    }
}