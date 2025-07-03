using System;
using UnityEngine;

namespace SteveAdventure
{
    [RequireComponent(typeof(Mover), typeof(WaypointsMoveController))]
    public class Enemy : MonoBehaviour
    {
        private EnemyStateMachine _stateMachine;
        private Mover _mover;
        private WaypointsMoveController _moveController;
        private Collider2D _collider;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _moveController = GetComponent<WaypointsMoveController>();
            _collider = GetComponent<Collider2D>();
            _stateMachine = new EnemyStateMachine(_mover, _moveController.WayPoints, _collider);
        }

        private void FixedUpdate()
        {
            _stateMachine.Update();
        }
    }
}