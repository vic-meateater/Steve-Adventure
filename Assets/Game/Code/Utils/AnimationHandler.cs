using System;
using UnityEngine;

namespace SteveAdventure
{
    public class AnimationHandler : MonoBehaviour
    {
        public event Action MeleeAttackStart;
        public event Action EndAttack;
        
        public void StartDamageFrame(string attackType)
        {
            if (attackType != "MeleeAttack") return;
            Debug.Log("Start Damage Frame Triggered for Melee Attack");
            MeleeAttackStart?.Invoke();
            
        }

        public void EndDamageFrame()
        {
            Debug.Log("End Damage Frame Triggered");
            EndAttack?.Invoke();
        }

    }
}