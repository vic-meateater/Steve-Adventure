using UnityEngine;

namespace SteveAdventure
{
    [RequireComponent(typeof(InputHandler), typeof(PlayerMover), typeof(PlayerAnimationController))]
    public sealed class Player : MonoBehaviour
    {
        private InputHandler _inputHandler;
        private PlayerMover _playerMover;
        private PlayerAnimationController _playerAnimation;
        private CollisionHandler _collisionHandler;

        private void Start()
        {
            _inputHandler = GetComponent<InputHandler>();
            _inputHandler.OnSpacePressed += OnSpacePressedHandler;
            _inputHandler.OnMoveInputChanged += OnMoveInputHandler;
            _inputHandler.OnActionPressed += OnActionPressedHandler;

            _playerMover = GetComponent<PlayerMover>();
            _playerAnimation = GetComponent<PlayerAnimationController>();
            _collisionHandler = GetComponent<CollisionHandler>();
        }

        private void OnDestroy()
        {
            _inputHandler.OnSpacePressed -= OnSpacePressedHandler;
            _inputHandler.OnMoveInputChanged -= OnMoveInputHandler;
            _inputHandler.OnActionPressed -= OnActionPressedHandler;
        }

        private void OnActionPressedHandler()
        {
            if (_collisionHandler.CanInteract)
                _collisionHandler.TryInteract();
        }


        private void OnSpacePressedHandler()
        {
            _playerMover.Dashing();
        }

        private void OnMoveInputHandler(Vector2 moveInput)
        {
            _playerMover.Moving(moveInput);
            _playerAnimation.MoveAnimation(moveInput);
        }
    }
}