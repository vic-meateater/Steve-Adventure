using System;
using UnityEngine;

namespace SteveAdventure
{
    public class AnimationHandler : MonoBehaviour
    {
        public event Action OnStartAttackFrame;
        public event Action OnEndAttackFrame;
        
        public void StartDamageFrame(string attackType)
        {
            if (attackType != "MeleeAttack") return;
            Debug.Log("Start Damage Frame Triggered for Melee Attack");
            OnStartAttackFrame?.Invoke();
            
        }

        public void EndDamageFrame()
        {
            Debug.Log("End Damage Frame Triggered");
            OnEndAttackFrame?.Invoke();
        }

    }
}