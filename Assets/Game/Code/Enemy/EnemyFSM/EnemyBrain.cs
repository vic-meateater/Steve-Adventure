using UnityEngine;

namespace SteveAdventure
{
    public sealed class EnemyBrain : BrainFSM
    {
        public EnemyBrain(Mover mover, Transform[] waypoints, EnemyVision enemyVision,
            AnimatorController animatorController, float waitDuration, float damage, float attackCooldown,
            Transform enemyTransform, Collider2D collider)
        {
            var idleState = new IdleState(waitDuration);
            var patrolState = new PatrolState(mover, waypoints, collider, enemyVision, animatorController, enemyTransform);
            var followState = new FollowState(mover, enemyVision, animatorController);

            AddState(patrolState);
            AddState(idleState);
            AddState(followState);

            patrolState.AddTransition(new Transition(() => patrolState.WayPointReached(), idleState));
            patrolState.AddTransition(new Transition(() => enemyVision.IsTargetInDetectionRange(), followState));
            followState.AddTransition(new Transition(() => followState.ShouldStopFollowing(), patrolState));
            idleState.AddTransition(new Transition(() => idleState.IsTimeOver(), patrolState));

            SetInitialState(idleState);
        }
    }
}