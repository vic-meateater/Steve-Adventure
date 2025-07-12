using UnityEngine;

namespace SteveAdventure
{
    public sealed class EnemyBrain : BrainFSM
    {
        public EnemyBrain(Mover mover, Transform[] waypoints, Collider2D collider, EnemyVision enemyVision, 
            AnimatorController animatorController, float waitDuration, float damage, float attackCooldown) 
        {
            State patrolState = new PatrolState(this, mover, waypoints, collider, enemyVision, animatorController);
            State idleState = new IdleState(this, waitDuration);
            State followState = new FollowState(this, mover, enemyVision, animatorController);
            State attackState = new AttackState(this, enemyVision, damage, attackCooldown, animatorController, mover);

            RegisterState(patrolState);
            patrolState.AddTransition(new FromPatrolToFollowTransition(this, enemyVision));
            patrolState.AddTransition(new FromPatrolToIdleTransition(this));

            RegisterState(idleState);
            idleState.AddTransition(new FromIdleToPatrolTransition(this));
            
            RegisterState(followState);
            followState.AddTransition(new FromFollowToPatrolTransition(this, enemyVision));
            followState.AddTransition(new FromFollowToAttackTransition(this, enemyVision));
            
            RegisterState(attackState);
            attackState.AddTransition(new FromFollowToAttackTransition(this, enemyVision));
            attackState.AddTransition(new FromAttackToFollowTransition(this, enemyVision));
            

            ChangeState<PatrolState>();
        }
    }
}