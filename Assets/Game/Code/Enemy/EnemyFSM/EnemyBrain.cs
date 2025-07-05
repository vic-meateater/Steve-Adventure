using UnityEngine;

namespace SteveAdventure
{
    public class EnemyBrain : BrainFSM
    {
        public EnemyBrain(Mover mover, Transform[] waypoints, Collider2D collider, EnemyVision enemyVision, 
            AnimatorController animatorController, float waitDuration)
        {
            State patrolState = new PatrolState(this, mover, waypoints, collider, enemyVision, animatorController);
            State idleState = new IdleState(this, waitDuration);
            State followState = new FollowState(this, mover, enemyVision, animatorController);

            RegisterState(patrolState);
            patrolState.AddTransition(new FromPatrolToIdleTransition(this));
            patrolState.AddTransition(new FromPatrolToFollowTransition(this, enemyVision));

            RegisterState(idleState);
            idleState.AddTransition(new FromIdleToPatrolTransition(this));
            
            RegisterState(followState);
            followState.AddTransition(new FromFollowToPatrolTransition(this, enemyVision));

            ChangeState<PatrolState>();
        }
    }
}