using System;
using UnityEngine;

namespace SteveAdventure
{
    [RequireComponent(typeof(InputHandler), typeof(Mover), typeof(AnimatorController))]
    [RequireComponent(typeof(PlayerVision), typeof(CollisionHandler))]
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private AnimationHandler _animationHandler;
        
        private InputHandler _inputHandler;
        private Mover _mover;
        private AnimatorController _animatorController;
        private CollisionHandler _collisionHandler;
        private PlayerVision _playerVision;
        private PlayerAttackController _playerAttackController;

        private void Start()
        {
            _inputHandler = GetComponent<InputHandler>();
            _inputHandler.OnSpacePressed += OnSpacePressedHandler;
            _inputHandler.OnMoveInputChanged += OnMoveInputHandler;
            _inputHandler.OnInteractPressed += OnInteractPressedHandler;
            _inputHandler.OnAttackPressed += OnAttackPressedHandler;

            _mover = GetComponent<Mover>();
            _animatorController = GetComponent<AnimatorController>();
            _collisionHandler = GetComponent<CollisionHandler>();
            _playerVision = GetComponent<PlayerVision>();
            
            _playerAttackController = new PlayerAttackController(_animationHandler, _animatorController, _playerVision);
        }

        private void OnDestroy()
        {
            _inputHandler.OnSpacePressed -= OnSpacePressedHandler;
            _inputHandler.OnMoveInputChanged -= OnMoveInputHandler;
            _inputHandler.OnInteractPressed -= OnInteractPressedHandler;
            _inputHandler.OnAttackPressed -= OnAttackPressedHandler;
        }
        private void OnAttackPressedHandler()
        {
            _playerAttackController.AttackRequest();
        }

        private void OnInteractPressedHandler()
        {
            if (_collisionHandler.CanInteract)
                _collisionHandler.TryInteract();
        }

        private void OnSpacePressedHandler()
        {
            _mover.Dashing();
        }

        private void OnMoveInputHandler(Vector2 moveInput)
        {
            _mover.Moving(moveInput);
            _animatorController.MoveAnimation(moveInput);
        }
    }
}