using UnityEngine;

namespace SteveAdventure
{
    public sealed class FollowState : State
    {
        private const float TARGET_REACHED_OFFSET = 0.7f;
        
        private readonly Mover _mover;
        private readonly EnemyVision _enemyVision;
        private readonly AnimatorController _animatorController;

        public FollowState(EnemyBrain brain, Mover mover, EnemyVision enemyVision,
            AnimatorController animatorController) : base(brain)
        {
            _mover = mover;
            _enemyVision = enemyVision;
            _animatorController = animatorController;
        }

        public override void Enter()
        {
            Debug.Log("Enter to Follow State");
        }

        public override void Update()
        {
            FollowTarget();
        }

        private bool WayPointReached(Vector2 targetPosition)
        {
            float sqrDistance = (targetPosition - (Vector2)_mover.transform.position).sqrMagnitude;
            return sqrDistance < TARGET_REACHED_OFFSET * TARGET_REACHED_OFFSET;
        }   

        private void FollowTarget()
        {
            if (_enemyVision.TryGetTargetPosition(out Vector2 targetPosition) && _enemyVision.CanSeeTargetDirectly())
            {
                Vector2 direction = (targetPosition - (Vector2) _mover.transform.position).normalized;
                _mover.Moving(direction);
                _enemyVision.SetVisionDirection(direction);
                _animatorController.MoveAnimation(direction);
                if (WayPointReached(targetPosition))
                {
                    Debug.Log("Target reached");
                    _mover.Moving(Vector2.zero);
                }
            }
            else
            {
                _mover.Moving(Vector2.zero);
            }
        }
    }
}