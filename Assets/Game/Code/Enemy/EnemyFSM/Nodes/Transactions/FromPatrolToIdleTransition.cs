using UnityEngine;

namespace SteveAdventure
{
    public sealed class FromPatrolToIdleTransition : Transition
    {
        public FromPatrolToIdleTransition(EnemyBrain brain) : base(brain)
        {
        }

        public override bool ShouldTransition()
        {
            if (Brain.GetCurrentState() is PatrolState patrolState)
            {
                return patrolState.WayPointReached();
            }

            return false;
        }

        public override void OnTransition()
        {
            Debug.Log("Transitioning from Patrol to Idle State");
            Brain.ChangeState<IdleState>();
        }
    }
}