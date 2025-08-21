using System;
using UnityEngine;

namespace SteveAdventure
{
    public class AnimationHandler : MonoBehaviour
    {
        public event Action MeleeAttackStart;
        public event Action AttackEnd;
        public event Action<bool> HitAnimationStart;
        public event Action<bool> HitAnimationEnd;
        
        
        public void StartDamageFrame(string attackType)
        {
            if (attackType != "MeleeAttack") return;
            MeleeAttackStart?.Invoke();
            
        }

        public void EndDamageFrame()
        {
            Debug.Log("End Damage Frame Triggered");
            AttackEnd?.Invoke();
        }

        public void HitAnimationFrameStart()
        {
            HitAnimationStart?.Invoke(true);
        }
        
        public void HitAnimationFrameEnd()
        {
            HitAnimationEnd?.Invoke(true);
        }
    }
}