using System;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    [RequireComponent(typeof(Mover), typeof(Waypoints), typeof(AnimatorController))]
    [RequireComponent(typeof(Collider2D), typeof(EnemyVision), typeof(HealthComponent))]
    public sealed class Enemy : MonoBehaviour, IGameFixedUpdateListener, IPoolable<EnemyConfig, IMemoryPool>
    {
        [SerializeField] private AnimationHandler _animationHandler;

        private Mover _mover;
        private Waypoints _waypoints;
        private EnemyVision _enemyVision;
        private AnimatorController _animatorController;
        private HealthComponent _health;
        private EnemyBrain _enemyBrain;
        private Transform _enemyTransform;
        private Collider2D _collider;
        private GameCycle _gameCycle;
        private float _attackCooldown = 1f;
        private float _damage = 10f;

        private IMemoryPool _pool;
        private EnemyConfig _config;
        private bool _isInitialized;


        private void Start()
        {
            _mover = GetComponent<Mover>();
            _waypoints = GetComponent<Waypoints>();
            _enemyVision = GetComponent<EnemyVision>();
            _animatorController = GetComponent<AnimatorController>();
            _health = GetComponent<HealthComponent>();
            _enemyTransform = transform;
            _collider = GetComponent<Collider2D>();

            _enemyBrain = new EnemyBrain(_mover, _config.EnemySpawnConfig.Waypoints, _enemyVision, _animatorController,
                _waypoints.WaitDuration, _damage, _attackCooldown, _enemyTransform, _collider, _animationHandler);
        }

        [Inject]
        public void Construct(GameCycle gameCycle)
        {
            _gameCycle = gameCycle;
            //_enemyVision = enemyVision;
        }

        private void Initialize(EnemyConfig config)
        {
            _config = config;
            _damage = _config.Damage;
            _attackCooldown = _config.AttackCooldown;


            _isInitialized = true;
        }


        void IGameFixedUpdateListener.OnGameFixedUpdate(float fixedDeltaTime)
        {
            if (_isInitialized)
                _enemyBrain.Update();
        }

        public void OnSpawned(EnemyConfig config, IMemoryPool pool)
        {
            _pool = pool;
            transform.position = config.EnemySpawnConfig.SpawnPoint;
            Initialize(config);

            _gameCycle.AddListener(this);
        }

        public void OnDespawned()
        {
            _pool = null;
            _isInitialized = false;
            _enemyBrain = null;
            _gameCycle.RemoveListener(this);
        }


        public void ReturnToPool()
        {
            _pool?.Despawn(this);
        }

        private void OnDeath()
        {
            _gameCycle?.RemoveListener(this);
            ReturnToPool();
        }
    }
}