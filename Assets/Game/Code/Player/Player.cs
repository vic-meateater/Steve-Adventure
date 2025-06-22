using UnityEngine;

namespace SteveAdventure
{
    [RequireComponent(typeof(InputHandler), typeof(PlayerMover))]
    public class Player : MonoBehaviour
    {
        private InputHandler _inputHandler;
        private PlayerMover _playerMover;

        private void Start()
        {
            _inputHandler = GetComponent<InputHandler>();
            _inputHandler.OnSpacePressed += OnSpacePressedHandler;
            _inputHandler.OnMoveInputChanged += OnMoveInputHandler;

            _playerMover = GetComponent<PlayerMover>();
        }

        private void OnDestroy()
        {
            _inputHandler.OnSpacePressed -= OnSpacePressedHandler;
            _inputHandler.OnMoveInputChanged -= OnMoveInputHandler;
        }

        private void OnSpacePressedHandler()
        {
            _playerMover.Dashing();
        }

        private void OnMoveInputHandler(Vector2 moveInput)
        {
            _playerMover.Moving(moveInput);
        }
    }
}