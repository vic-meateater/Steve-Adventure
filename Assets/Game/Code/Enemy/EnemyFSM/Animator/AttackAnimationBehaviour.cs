using System.Collections.Generic;
using UnityEngine;

namespace SteveAdventure
{
    public class AttackAnimationBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var handler = animator.GetComponent<AnimationHandler>();
            handler?.StartDamageFrame("MeleeAttack");
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var handler = animator.GetComponent<AnimationHandler>();
            handler?.EndDamageFrame();
        }
    }
}