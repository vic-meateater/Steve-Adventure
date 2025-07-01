using UnityEngine;

namespace SteveAdventure
{
    public class WaypointsMoveController : MonoBehaviour
    {
        private const int WAYPOINT_STEP = 1;
        
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private float _wayPointReachedOffset = .2f;
        
        private Mover _mover;
        private AnimatorController _animator;
        private int _currentWaypointIndex = 0;
        private Collider2D _collider;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _collider = GetComponent<Collider2D>();
            _animator = GetComponent<AnimatorController>();
        }

        private void Update()
        {
            WayPointsMover();
        }

        private void WayPointsMover()
        {
            Transform currentWaypoint = _waypoints[_currentWaypointIndex];
            Vector2 colliderCenter = _collider.bounds.center;
            Vector2 direction = (currentWaypoint.position - (Vector3)colliderCenter).normalized;
            
            _mover.Moving(direction);
            _animator.MoveAnimation(direction);

            float distance = Vector2.Distance(colliderCenter, currentWaypoint.position);

            if (distance < _wayPointReachedOffset)
            {
                _currentWaypointIndex = (_currentWaypointIndex + WAYPOINT_STEP) % _waypoints.Length;
            }
        }
    }
}