using System;
using UnityEngine;

namespace SteveAdventure
{
    [RequireComponent(typeof(Mover), typeof(WaypointsMoveController))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _waitDuration;
        
        private Mover _mover;
        private WaypointsMoveController _moveController;
        private Collider2D _collider;
        private EnemyBrain _enemyBrain;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _moveController = GetComponent<WaypointsMoveController>();
            _collider = GetComponent<Collider2D>();
            _enemyBrain = new EnemyBrain(_mover, _moveController.WayPoints, _collider, _waitDuration);
        }

        private void FixedUpdate()
        {
            _enemyBrain.Update();
        }
    }
}