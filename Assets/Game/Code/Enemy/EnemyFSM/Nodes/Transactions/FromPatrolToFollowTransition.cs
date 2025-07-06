namespace SteveAdventure
{
    public sealed class FromPatrolToFollowTransition : Transition
    {
        private readonly EnemyVision _enemyVision;
        public FromPatrolToFollowTransition(EnemyBrain brain, EnemyVision enemyVision) : base(brain)
        {
            _enemyVision = enemyVision;
        }
        public override bool ShouldTransition()
        {
            if (Brain.GetCurrentState() is PatrolState _)
            {
                return _enemyVision.IsTargetInDetectionRange();
            }
            return false;
        }
        public override void OnTransition()
        {
            Brain.ChangeState<FollowState>();
        }
    }
}