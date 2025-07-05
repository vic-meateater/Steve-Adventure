namespace SteveAdventure
{
    public class FromPatrolToFollowTransition : Transition
    {
        private readonly EnemyVision _enemyVision;
        public FromPatrolToFollowTransition(EnemyBrain brain, EnemyVision enemyVision) : base(brain)
        {
            _enemyVision = enemyVision;
        }
        public override bool ShouldTransition()
        {
            if (Brain.GetCurrentState() is PatrolState patrolState)
            {
                return _enemyVision.TargetInRage;
            }
            return false;
        }
        public override void OnTransition()
        {
            Brain.ChangeState<FollowState>();
        }
    }
}