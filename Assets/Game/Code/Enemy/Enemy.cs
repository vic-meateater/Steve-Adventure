using System;
using R3;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    [RequireComponent(typeof(Mover), typeof(AnimatorController))]
    [RequireComponent(typeof(Collider2D), typeof(EnemyVision))]
    public sealed class Enemy :
        MonoBehaviour,
        IDamageable,
        IGameFixedUpdateListener,
        IPoolable<EnemyConfig, IMemoryPool>
    {
        private event Action<float> _onDeadRespawnRequested;

        [SerializeField] private AnimationHandler _animationHandler;

        private Mover _mover;
        private EnemyVision _enemyVision;
        private AnimatorController _animatorController;
        private EnemyBrain _enemyBrain;
        private Transform _enemyTransform;
        private Collider2D _collider;
        private GameCycle _gameCycle;
        private float _attackCooldown = 1f;
        private float _damage = 10f;

        private IMemoryPool _pool;
        private EnemyConfig _config;
        private bool _isInitialized;

        private IHealthViewModel _healthViewModel;
        private IFactory<CharacterConfig, IHealthViewModel> _healthFactory;
        private EnemySoundsFactory _soundsFactory;
        private IEnemySounds _sounds;
        private EnemyUIView _enemyUIView;

        private DisposableBag _disposables;


        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _enemyVision = GetComponent<EnemyVision>();
            _animatorController = GetComponent<AnimatorController>();
            _enemyTransform = transform;
            _collider = GetComponent<Collider2D>();
        }

        [Inject]
        public void Construct(
            GameCycle gameCycle,
            IFactory<CharacterConfig, IHealthViewModel> healthFactory,
            EnemySoundsFactory soundsFactory,
            EnemyUIView enemyUIView)
        {
            _gameCycle = gameCycle;
            _healthFactory = healthFactory;
            _soundsFactory = soundsFactory;
            _enemyUIView = enemyUIView;
        }

        private void Initialize(EnemyConfig config)
        {
            _config = config;
            _damage = _config.Damage;
            _attackCooldown = _config.AttackCooldown;
            _healthViewModel = _healthFactory.Create(config);
            _sounds = _soundsFactory.Create(config);
            
            _enemyUIView.Init(_healthViewModel);

            _enemyBrain = new EnemyBrain(_mover, _config.Waypoints, _enemyVision, _animatorController,
                _config.WaitDuration, _damage, _attackCooldown, _enemyTransform, _collider, _animationHandler, _sounds);

            _healthViewModel.IsDead
                .Where(dead => dead)
                .Subscribe(_ =>
                {
                    _sounds.DeathSound();
                    _onDeadRespawnRequested?.Invoke(_config.RespawnDuration);
                    _pool.Despawn(this);
                })
                .AddTo(ref _disposables);

            _isInitialized = true;
        }

        public void SetOnDiedCallback(Action<float> callback)
        {
            _onDeadRespawnRequested = callback;
        }

        void IGameFixedUpdateListener.OnGameFixedUpdate(float fixedDeltaTime)
        {
            if (_isInitialized)
                _enemyBrain.Update();
        }

        public void OnSpawned(EnemyConfig config, IMemoryPool pool)
        {
            _pool = pool;
            transform.position = config.SpawnPoint;
            Initialize(config);

            _gameCycle.AddListener(this);
        }

        public void OnDespawned()
        {
            _pool = null;
            _isInitialized = false;
            _enemyBrain = null;
            _healthViewModel = null;
            _onDeadRespawnRequested = null;
            _gameCycle.RemoveListener(this);
            _disposables.Dispose();
        }

        public void TakeDamage(float damage)
        {
            _healthViewModel.TakeDamage(damage);
            _sounds.PlayPainSound();
        }
    }
}