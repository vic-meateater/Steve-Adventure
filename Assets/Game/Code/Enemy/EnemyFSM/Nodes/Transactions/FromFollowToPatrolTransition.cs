using UnityEngine;

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
                Debug.Log("Checking transition from Follow to Patrol State " + !_enemyVision.IsTargetInDetectionRange());
                return !_enemyVision.IsTargetInDetectionRange();
            }
            return false;
        }
        public override void OnTransition()
        {
            Debug.Log("Transitioning from Follow to Patrol State");
            Brain.ChangeState<PatrolState>();
        }
    }
}