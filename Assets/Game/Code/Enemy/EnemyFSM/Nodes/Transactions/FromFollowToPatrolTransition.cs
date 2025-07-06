namespace SteveAdventure
{
    public sealed class FromFollowToPatrolTransition : Transition
    {
        private readonly EnemyVision _enemyVision;
        public FromFollowToPatrolTransition(EnemyBrain brain, EnemyVision enemyVision) : base(brain)
        {
            _enemyVision = enemyVision;
        }
        public override bool ShouldTransition()
        {
            if (Brain.GetCurrentState() is FollowState _)
            {
                return !_enemyVision.IsTargetInDetectionRange();
            }
            return false;
        }
        public override void OnTransition()
        {
            Brain.ChangeState<PatrolState>();
        }
    }
}