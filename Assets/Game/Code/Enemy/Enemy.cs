using System;
using UnityEngine;

namespace SteveAdventure
{
    [RequireComponent(typeof(Mover), typeof(Waypoints), typeof(AnimatorController))]
    [RequireComponent(typeof(Collider2D), typeof(EnemyVision))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _waitDuration;

        private Mover _mover;
        private Waypoints _waypoints; // переделать в конфиг
        private Collider2D _collider;
        private EnemyVision _enemyVision;
        private AnimatorController _animator;
        private EnemyBrain _enemyBrain;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _waypoints = GetComponent<Waypoints>();
            _collider = GetComponent<Collider2D>();
            _enemyVision = GetComponent<EnemyVision>();
            _animator = GetComponent<AnimatorController>();
            _enemyBrain = new EnemyBrain(_mover, _waypoints.WayPoints, _collider, _enemyVision, _animator,
                _waitDuration);
        }

        private void FixedUpdate()
        {
            _enemyBrain.Update();
        }
    }
}