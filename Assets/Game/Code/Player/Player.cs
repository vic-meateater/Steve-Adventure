using UnityEngine;

namespace SteveAdventure
{
    [RequireComponent(typeof(InputHandler), typeof(Mover), typeof(AnimatorController))]
    public sealed class Player : MonoBehaviour
    {
        private InputHandler _inputHandler;
        private Mover _mover;
        private AnimatorController _animator;
        private CollisionHandler _collisionHandler;

        private void Start()
        {
            _inputHandler = GetComponent<InputHandler>();
            _inputHandler.OnSpacePressed += OnSpacePressedHandler;
            _inputHandler.OnMoveInputChanged += OnMoveInputHandler;
            _inputHandler.OnActionPressed += OnActionPressedHandler;

            _mover = GetComponent<Mover>();
            _animator = GetComponent<AnimatorController>();
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
            _mover.Dashing();
        }

        private void OnMoveInputHandler(Vector2 moveInput)
        {
            _mover.Moving(moveInput);
            _animator.MoveAnimation(moveInput);
        }
    }
}