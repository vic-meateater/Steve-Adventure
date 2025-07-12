using UnityEngine;

namespace SteveAdventure
{
    public sealed class FromIdleToPatrolTransition : Transition
    {
        public FromIdleToPatrolTransition(EnemyBrain brain) : base(brain)
        {
        }

        public override bool ShouldTransition()
        {
            if (Brain.GetCurrentState() is IdleState idleState)
            {
                return idleState.IsTimeOver();
            }

            return false;
        }

        public override void OnTransition()
        {
            Debug.Log("Transitioning from Idle to Patrol State");
            Brain.ChangeState<PatrolState>();
        }
    }
}