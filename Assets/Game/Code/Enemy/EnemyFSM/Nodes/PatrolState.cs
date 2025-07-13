using System;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class PatrolState : State
    {
        private const int WAYPOINT_STEP = 1;

        private readonly Mover _mover;
        private readonly Transform[] _waypoints;
        private readonly Transform _enemyTransform;
        private readonly EnemyVision _enemyVision;
        private readonly AnimatorController _animatorController;
        private readonly float _wayPointReachedOffset = .1f;
        private int _currentWaypointIndex = 0;

        public PatrolState(Mover mover, Transform[] waypoints,
            EnemyVision enemyVision, AnimatorController animatorController, Transform enemyTransform)
        {
            _mover = mover;
            _waypoints = waypoints;
            _enemyTransform = enemyTransform;
            _enemyVision = enemyVision;
            _animatorController = animatorController;
        }

        public override void Enter()
        {
            Debug.Log("Enter to Patrol State");
            //WayPointsMover();
        }

        public override void Update()
        {
            if(WayPointReached())
                ChangeWayPoint();
            else
                WayPointsMover();
        }

        public override void Exit()
        {
            Debug.Log("Exit from Patrol State");
            _mover.Moving(Vector2.zero);
            _animatorController.MoveAnimation(Vector2.zero);
        }

        public bool WayPointReached()
        {
            //float sqrDistance = (_waypoints[_currentWaypointIndex].position - _collider.bounds.center).sqrMagnitude;
            float sqrDistance = (_waypoints[_currentWaypointIndex].position - _enemyTransform.position).sqrMagnitude;
            Debug.Log("Current Waypoint Index: " + _currentWaypointIndex);
            Debug.Log("Checking if waypoint is reached in Patrol State " +
                      (sqrDistance < _wayPointReachedOffset * _wayPointReachedOffset));
            return sqrDistance < _wayPointReachedOffset * _wayPointReachedOffset;
        }

        private void WayPointsMover()
        {
            Transform currentWaypoint = _waypoints[_currentWaypointIndex];
            Vector2 direction = (currentWaypoint.position - _enemyTransform.position).normalized;

            _enemyVision.SetVisionDirection(direction);
            _animatorController.MoveAnimation(direction);
            _mover.Moving(direction);
        }

        private void ChangeWayPoint()
        {
            //if (WayPointReached())
                _currentWaypointIndex = (_currentWaypointIndex + WAYPOINT_STEP) % _waypoints.Length;
        }
    }
}