using System;
using UnityEngine;

namespace SteveAdventure
{
    [RequireComponent(typeof(Mover), typeof(Waypoints), typeof(AnimatorController))]
    [RequireComponent(typeof(Collider2D), typeof(EnemyVision))]
    public sealed class Enemy : MonoBehaviour
    {
        private Mover _mover;
        private Waypoints _waypoints;
        private Collider2D _collider;
        private EnemyVision _enemyVision;
        private AnimatorController _animatorController;
        private EnemyBrain _enemyBrain;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _waypoints = GetComponent<Waypoints>();
            _collider = GetComponent<Collider2D>();
            _enemyVision = GetComponent<EnemyVision>();
            _animatorController = GetComponent<AnimatorController>();
            _enemyBrain = new EnemyBrain(_mover, _waypoints.WayPoints, _collider, _enemyVision, _animatorController,
                _waypoints.WaitDuration);
        }

        private void FixedUpdate()
        {
            _enemyBrain.Update();
        }
    }
}