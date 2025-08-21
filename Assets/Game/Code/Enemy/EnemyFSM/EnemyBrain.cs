using UnityEngine;

namespace SteveAdventure
{
    public sealed class EnemyBrain : BrainFSM
    {
        private readonly EnemyVision _enemyVision;
        private bool _canAttack;
        private bool _targetInRange;

        public EnemyBrain(Mover mover, Vector3[] waypoints, EnemyVision enemyVision,
            AnimatorController animatorController, float waitDuration, float damage, float attackCooldown,
            Transform enemyTransform, Collider2D collider, AnimationHandler animationHandler)
        {
            _enemyVision = enemyVision;
            
            var idleState = new IdleState(waitDuration);
            var patrolState = new PatrolState(mover, waypoints, collider, enemyVision, animatorController, enemyTransform);
            var followState = new FollowState(mover, enemyVision, animatorController);
            var attackState = new AttackState(mover, enemyVision, animatorController, damage, attackCooldown, animationHandler);
            var hitState = new HitState(animationHandler);

            AddState(patrolState);
            AddState(idleState);
            AddState(followState);
            AddState(attackState);
            AddState(hitState);

            patrolState.AddTransition(new Transition(() => patrolState.WayPointReached(), idleState));
            patrolState.AddTransition(new Transition(() => _targetInRange, followState));
            
            followState.AddTransition(new Transition(() => _canAttack, attackState));
            followState.AddTransition(new Transition(() => followState.ShouldStopFollowing(), patrolState));
            
            attackState.AddTransition(new Transition(() => attackState.ShouldExitAttackState(), patrolState));
            
           // hitState.AddTransition(new Transition(,hitState));
            
            idleState.AddTransition(new Transition(() => idleState.IsTimeOver(), patrolState));
            idleState.AddTransition(new Transition(() => _targetInRange, followState));

            SetInitialState(idleState);
        }

        public override void Update()
        {
            _canAttack = _enemyVision.CanAttack();
            _targetInRange = _enemyVision.IsTargetInDetectionRange();
            
            base.Update();
        }
    }
}