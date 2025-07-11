namespace SteveAdventure
{
    public sealed class FromFollowToAttackTransition : Transition
    {
        private readonly EnemyVision _enemyVision;
        public FromFollowToAttackTransition(EnemyBrain brain, EnemyVision enemyVision) : base(brain)
        {
            _enemyVision = enemyVision;
        }
        public override bool ShouldTransition()
        {
            if (Brain.GetCurrentState() is FollowState _)
            {
                return _enemyVision.CanAttack();
            }
            return false;
        }
        public override void OnTransition()
        {
            Brain.ChangeState<AttackState>();
        }
    }
}