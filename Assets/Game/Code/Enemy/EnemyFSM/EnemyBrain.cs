using UnityEngine;

namespace SteveAdventure
{
    public class EnemyBrain : BrainFSM
    {
        public EnemyBrain(Mover mover, Transform[] waypoints, Collider2D collider, EnemyVision enemyVision, 
            AnimatorController animator, float waitDuration)
        {
            State patrolState = new PatrolState(this, mover, waypoints, collider, enemyVision, animator);
            State idleState = new IdleState(this, waitDuration);

            RegisterState(patrolState);
            patrolState.AddTransition(new FromPatrolToIdleTransition(this));

            RegisterState(idleState);
            idleState.AddTransition(new FromIdleToPatrolTransition(this));

            ChangeState<PatrolState>();
        }
    }
}