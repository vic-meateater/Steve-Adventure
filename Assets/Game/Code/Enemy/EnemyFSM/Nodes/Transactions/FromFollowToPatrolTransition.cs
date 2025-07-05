namespace SteveAdventure
{
    public class FromFollowToPatrolTransition : Transition
    {
        private readonly EnemyVision _enemyVision;
        public FromFollowToPatrolTransition(EnemyBrain brain, EnemyVision enemyVision) : base(brain)
        {
            _enemyVision = enemyVision;
        }
        public override bool ShouldTransition()
        {
            if (Brain.GetCurrentState() is FollowState followState)
            {
                return !_enemyVision.TargetInRage;
            }
            return false;
        }
        public override void OnTransition()
        {
            Brain.ChangeState<PatrolState>();
        }
    }
}