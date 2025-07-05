using UnityEngine;

namespace SteveAdventure
{
    public class PatrolState : State
    {
        private const int WAYPOINT_STEP = 1;

        private Mover _mover;
        private Transform[] _waypoints;
        private Collider2D _collider;
        private EnemyVision _enemyVision;
        private AnimatorController _animator;
        private float _wayPointReachedOffset = .2f;
        private int _currentWaypointIndex = 0;

        public PatrolState(EnemyBrain brain, Mover mover, Transform[] waypoints, Collider2D collider,
            EnemyVision enemyVision, AnimatorController animator) : base(brain)
        {
            _mover = mover;
            _waypoints = waypoints;
            _collider = collider;
            _enemyVision = enemyVision;
            _animator = animator;
        }

        public override void Enter()
        {
            Debug.Log("Enemy Start Patrol");
            ChangeWayPoint();
        }

        public override void Update()
        {
            WayPointsMover();
        }

        public override void Exit()
        {
            Debug.Log("Выход из состояния");
            _mover.Moving(Vector2.zero);
        }

        private void WayPointsMover()
        {
            Transform currentWaypoint = _waypoints[_currentWaypointIndex];
            Vector2 colliderCenter = _collider.bounds.center;
            Vector2 direction = (currentWaypoint.position - (Vector3) colliderCenter).normalized;
            
            _enemyVision.SetDirection(direction);
            _animator.MoveAnimation(direction);
            _mover.Moving(direction);
        }

        public void ChangeWayPoint()
        {
            if(WayPointReached())
                _currentWaypointIndex = (_currentWaypointIndex + WAYPOINT_STEP) % _waypoints.Length;
        }

        public bool WayPointReached()
        {
            float sqrDistance = (_collider.bounds.center - _waypoints[_currentWaypointIndex].position).sqrMagnitude;
            return sqrDistance < _wayPointReachedOffset;
        }
    }
}