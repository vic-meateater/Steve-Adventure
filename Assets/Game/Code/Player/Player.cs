using UnityEngine;

namespace SteveAdventure
{
    [RequireComponent(typeof(Mover), typeof(AnimatorController))]
    [RequireComponent(typeof(PlayerVision), typeof(CollisionHandler), typeof(HealthComponent))]
    public sealed class Player : MonoBehaviour, IGamePauseListener, IGameResumeListener
    {
        [SerializeField] private AnimationHandler _animationHandler;
        [SerializeField] private float _damage = 10f;

        private Mover _mover;
        private AnimatorController _animatorController;
        private CollisionHandler _collisionHandler;
        private PlayerVision _playerVision;
        private PlayerAttackController _playerAttackController;
        private HealthComponent _health;
        private Vector2 _savedDirection;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _animatorController = GetComponent<AnimatorController>();
            _collisionHandler = GetComponent<CollisionHandler>();
            _playerVision = GetComponent<PlayerVision>();

            _playerAttackController =
                new PlayerAttackController(_animationHandler, _animatorController, _playerVision, _damage);
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
            
            Debug.Log("Player direction: " + _savedDirection);
            
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
    }
}