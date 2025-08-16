using R3;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    [RequireComponent(typeof(Mover), typeof(AnimatorController))]
    [RequireComponent(typeof(PlayerVision), typeof(CollisionHandler))]
    public sealed class Player : 
        MonoBehaviour,
        IDamageable, 
        IGamePauseListener, 
        IGameResumeListener, 
        IGameOverListener
    {
        [SerializeField] private AnimationHandler _animationHandler;
        private float _damage;

        private Mover _mover;
        private AnimatorController _animatorController;
        private CollisionHandler _collisionHandler;
        private PlayerVision _playerVision;
        private PlayerAttackController _playerAttackController;
        private Vector2 _savedDirection;
        
        private IHealthViewModel _healthViewModel;

        [Inject]
        public void Construct(
            PlayerConfig playerConfig, 
            IFactory<PlayerConfig, IHealthViewModel> healthFactory,
            PlayerUIView playerUIView
            )
        {
            _healthViewModel = healthFactory.Create(playerConfig);
            _damage = playerConfig.Damage;
            _mover = GetComponent<Mover>();
            _animatorController = GetComponent<AnimatorController>();
            _collisionHandler = GetComponent<CollisionHandler>();
            _playerVision = GetComponent<PlayerVision>();

            _playerAttackController =
                new PlayerAttackController(_animationHandler, _animatorController, _playerVision, _damage);

            playerUIView.Show(_healthViewModel);
            _healthViewModel.IsDead.Subscribe(OnDeath).AddTo(this);
        }

        public void OnAttackPressed()
        {
            _playerAttackController.AttackRequest();
        }

        public void OnInteractPressed()
        {
            if (_collisionHandler.CanInteract)
                _collisionHandler.TryInteract();
        }

        public void OnSpacePressed()
        {
            _mover.Dashing();
        }

        public void OnMoveInputChanged(Vector2 direction)
        {
            _savedDirection = direction;
            
            if(direction == Vector2.zero)
                Debug.Log("Player move input zero: " + direction.sqrMagnitude);
            
            _mover.Moving(direction);
            _animatorController.MoveAnimation(direction);
        }

        public void OnGamePause()
        {
            var moveInputZero = Vector2.zero;
            _mover.Moving(moveInputZero);
            _animatorController.MoveAnimation(moveInputZero);
        }

        public void OnGameResume()
        {
            _mover.Moving(_savedDirection);
            _animatorController.MoveAnimation(_savedDirection);
        }
        
        
        public void OnGameOver()
        {
            Debug.Log("Game Over triggered, stopping player movement.");
            var moveInputZero = Vector2.zero;
            _mover.Moving(moveInputZero);
            _animatorController.MoveAnimation(moveInputZero);
        }

        public void TakeDamage(float damage)
        {
            _healthViewModel.TakeDamage(damage);
        }

        private void OnDeath(bool isDead)
        {
            if (isDead)
            {
                Debug.Log("Player is Dead");
                GameCycleService.Instance?.GameOver();
            }
        }

    }
}