using UnityEngine;

namespace SteveAdventure
{
    public sealed class Waypoints : MonoBehaviour
    {
        public Transform[] WayPoints => _waypoints;
        public float WaitDuration => _waitDuration;

        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private float _waitDuration;
        
        
    }
}