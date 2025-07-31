using System;
using UnityEngine;

namespace SteveAdventure
{
    [RequireComponent(typeof(Mover), typeof(Waypoints), typeof(AnimatorController))]
    [RequireComponent(typeof(Collider2D), typeof(EnemyVision), typeof(HealthComponent))]
    public sealed class Enemy : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private float _damage = 10f;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private AnimationHandler _animationHandler;
        
        private Mover _mover;
        private Waypoints _waypoints;
        private EnemyVision _enemyVision;
        private AnimatorController _animatorController;
        private HealthComponent _health;
        private EnemyBrain _enemyBrain;
        private Transform _enemyTransform;
        private Collider2D _collider;


        private void Start()
        {
            _mover = GetComponent<Mover>();
            _waypoints = GetComponent<Waypoints>();
            _enemyVision = GetComponent<EnemyVision>();
            _animatorController = GetComponent<AnimatorController>();
            _health = GetComponent<HealthComponent>();
            _enemyTransform = transform;
            _collider = GetComponent<Collider2D>();
            _enemyBrain = new EnemyBrain(_mover, _waypoints.WayPoints, _enemyVision, _animatorController,
                _waypoints.WaitDuration, _damage, _attackCooldown, _enemyTransform, _collider, _animationHandler);
            
            GameCycleService.Instance?.AddListener(this);
        }

        private void OnDestroy()
        {
            GameCycleService.Instance?.RemoveListener(this);
        }

        public void OnGameFixedUpdate(float fixedDeltaTime)
        {
            _enemyBrain.Update();
        }
    }
}