namespace SteveAdventure
{
    public sealed class FromAttackToFollowTransition : Transition
    {
        private readonly EnemyVision _enemyVision;
        public FromAttackToFollowTransition(EnemyBrain brain, EnemyVision enemyVision) : base(brain)
        {
            _enemyVision = enemyVision;
        }
        public override bool ShouldTransition()
        {
            if (Brain.GetCurrentState() is AttackState _)
            {
                return !_enemyVision.CanAttack();
            }
            return false;
        }
        public override void OnTransition()
        {
            Brain.ChangeState<FollowState>();
        }
    }
}