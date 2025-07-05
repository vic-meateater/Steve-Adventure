using System;
using System.Collections;
using UnityEngine;

namespace SteveAdventure
{
    public class Waypoints : MonoBehaviour
    {
        public Transform[] WayPoints => _waypoints;
        
        [SerializeField] private Transform[] _waypoints;
        

    }
}