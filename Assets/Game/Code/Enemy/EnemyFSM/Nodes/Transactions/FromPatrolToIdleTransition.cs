namespace SteveAdventure
{
    public class FromPatrolToIdleTransition : Transition
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
            Brain.ChangeState<IdleState>();
        }
    }
}