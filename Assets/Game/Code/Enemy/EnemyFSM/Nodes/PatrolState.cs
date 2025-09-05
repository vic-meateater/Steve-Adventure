using System;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class PatrolState : State
    {
        private const int WAYPOINT_STEP = 1;

        private readonly Mover _mover;
        private readonly Vector3[] _waypoints;
        private readonly Collider2D _collider;
        private readonly EnemyVision _enemyVision;
        private readonly AnimatorController _animatorController;
        private readonly float _wayPointReachedOffset = .1f;
        private readonly IEnemySounds _sounds;
        private int _currentWaypointIndex = 0;
        private bool _waypointShouldChange = false;
        private Vector2 _savedDirection;

        public PatrolState(Mover mover, Vector3[] waypoints, Collider2D collider,
            EnemyVision enemyVision, AnimatorController animatorController,
            IEnemySounds enemySounds)
        {
            _mover = mover;
            _waypoints = waypoints;
            _collider = collider;
            _enemyVision = enemyVision;
            _animatorController = animatorController;
            _sounds = enemySounds;
        }

        public override void Enter()
        {
            Debug.Log("Entering Patrol State");
            if (_waypointShouldChange)
            {
                ChangeWayPoint();
                _waypointShouldChange = false;
            }
            else
            {
                FindNearestWaypoint();
            }
        }

        public override void Update()
        {
            WayPointsMover();
        }

        public override void OnGamePause()
        {
            Debug.Log("Game Paused, stopping movement in Patrol State");
            ResetMovement();
        }

        public override void OnGameOver()
        {
            Debug.Log("Game Over, stopping movement in Patrol State");
            ResetMovement();
        }

        public override void OnGameResume()
        {
            _mover.Moving(_savedDirection);
            _animatorController.MoveAnimation(_savedDirection);
        }

        public override void Exit()
        {
            _mover.Moving(Vector2.zero);
            _animatorController.MoveAnimation(Vector2.zero);
        }

        public bool WayPointReached()
        {
            float sqrDistance = (_waypoints[_currentWaypointIndex] - _collider.bounds.center).sqrMagnitude;
            bool reached = sqrDistance < _wayPointReachedOffset * _wayPointReachedOffset;

            if (reached && !_waypointShouldChange)
            {
                _waypointShouldChange = true;
            }

            return reached;
        }

        private void WayPointsMover()
        {
            Vector3 currentWaypoint = _waypoints[_currentWaypointIndex];
            Vector2 direction = (currentWaypoint - _collider.bounds.center).normalized;

            _savedDirection = direction;
            _enemyVision.SetVisionDirection(direction);
            _animatorController.MoveAnimation(direction);
            _mover.Moving(direction);
            
            if(direction.magnitude > 0.1f)
                _sounds.PlayFootstepSound();
        }

        private void ChangeWayPoint()
        {
            if (WayPointReached())
                _currentWaypointIndex = (_currentWaypointIndex + WAYPOINT_STEP) % _waypoints.Length;
        }

        private void FindNearestWaypoint()
        {
            if (_waypoints.Length == 0) return;

            float minDistance = float.MaxValue;
            int nearestIndex = 0;

            Vector2 currentPosition = _collider.bounds.center;

            for (int i = 0; i < _waypoints.Length; i++)
            {
                float distance = Vector2.SqrMagnitude(currentPosition - (Vector2) _waypoints[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestIndex = i;
                }
            }

            _currentWaypointIndex = nearestIndex;
        }

        private void ResetMovement()
        {
            var zeroDirection = Vector2.zero;
            _mover.Moving(zeroDirection);
            _animatorController.MoveAnimation(zeroDirection);
        }
    }
}