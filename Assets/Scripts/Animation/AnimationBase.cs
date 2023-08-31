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
        OPEN,
        CLOSE
    }

    public class AnimationBase : MonoBehaviour
    {
        public List<AnimationSetup> setups;
        public List<Animator> animators;

        public void PlayAnimationByType(AnimationType animationType)
        {
            var setup = setups.Find(i => i.animationType == animationType);
            if (setup != null)
            {
                foreach (var item in animators)
                {
                    item.SetTrigger(setup.trigger);
                }
            }
        }

        public void AnimationBool(AnimationType animationType,bool b)
        {
            var setup = setups.Find(i => i.animationType == animationType);
            if (setup != null)
            {
                foreach (var item in animators)
                {
                    item.SetBool(setup.trigger, b);
                }
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