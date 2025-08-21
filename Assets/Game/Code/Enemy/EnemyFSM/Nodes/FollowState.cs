using System;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class FollowState : State
    {
        private const float TARGET_REACHED_OFFSET = 0.7f;
        private const float TARGET_LOST_DELAY = 0.5f;

        private readonly Mover _mover;
        private readonly EnemyVision _enemyVision;
        private readonly AnimatorController _animatorController;
        private float _lastTargetSeenTime;
        private Vector2 _savedDirection;

        public FollowState(Mover mover, EnemyVision enemyVision,
            AnimatorController animatorController)
        {
            _mover = mover;
            _enemyVision = enemyVision;
            _animatorController = animatorController;
        }

        public override void Enter()
        {
            Debug.Log("Entering Follow State");
            _lastTargetSeenTime = Time.time;
        }

        public override void Update()
        {
            FollowTarget();
        }

        public override void OnGamePause()
        {
            Debug.Log("Game Paused, stopping movement in Follow State");
            ResetMovement();
        }


        public override void OnGameOver()
        {
            ResetMovement();
        }

        public override void OnGameResume()
        {
            _mover.Moving(_savedDirection);
            _animatorController.MoveAnimation(_savedDirection);
        }

        public bool ShouldStopFollowing()
        {
            if (_enemyVision.IsTargetInDetectionRange())
            {
                _lastTargetSeenTime = Time.time;
                return false;
            }

            return Time.time - _lastTargetSeenTime > TARGET_LOST_DELAY;
        }

        private bool TargetReached(Vector2 targetPosition)
        {
            float sqrDistance = (targetPosition - (Vector2) _mover.transform.position).sqrMagnitude;
            var targetReached = (sqrDistance < TARGET_REACHED_OFFSET * TARGET_REACHED_OFFSET);
            if (targetReached)
            {
                _mover.Moving(Vector2.zero);
                _animatorController.MoveAnimation(Vector2.zero);
            }

            return targetReached;
        }

        private void FollowTarget()
        {
            if (_enemyVision.TryGetTargetPosition(out Vector2 targetPosition) && _enemyVision.CanSeeTargetDirectly())
            {
                if (!TargetReached(targetPosition))
                {
                    Vector2 direction = (targetPosition - (Vector2) _mover.transform.position).normalized;
                    _savedDirection = direction;
                    _mover.Moving(direction);
                    _enemyVision.SetVisionDirection(direction);
                    _animatorController.MoveAnimation(direction);
                }
            }
            else
            {
                _mover.Moving(Vector2.zero);
            }
        }
        
        private void ResetMovement()
        {
            var directionZero = Vector2.zero;
            _mover.Moving(directionZero);
            _animatorController.MoveAnimation(directionZero);
        }
    }
}