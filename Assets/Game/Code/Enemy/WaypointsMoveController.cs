using UnityEngine;

namespace SteveAdventure
{
    public class WaypointsController : MonoBehaviour
    {
        [SerializeField] private Transform[] _waypoints;
        private Mover _mover;
        private int _currentWaypointIndex = 0;
        private Collider2D _collider;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _collider = GetComponent<Collider2D>();
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

            var wayPointReached = Vector2.Distance(colliderCenter, currentWaypoint.position) < 0.2f;

            if (wayPointReached)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            }
        }
    }
}