using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace SteveAdventure
{
    public sealed class NewInputHandler : MonoBehaviour
    {
        public event Action OnSpacePressed;
        public event Action<Vector2> OnMoveInputChanged;
        public event Action OnInteractPressed;
        public event Action OnAttackPressed;

        [SerializeField] private InputActionAsset _inputActions;

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _interactAction;
        private InputAction _attackAction;

        private Vector2 _moveInput;

        private void Awake()
        {
            var playerActions = _inputActions.FindActionMap("Player");

            _moveAction = playerActions.FindAction("Move");
            _jumpAction = playerActions.FindAction("Jump");
            _interactAction = playerActions.FindAction("Interact");
            _attackAction = playerActions.FindAction("Attack");

            _jumpAction.performed += _ => OnSpacePressed?.Invoke();
            _interactAction.performed += _ => OnInteractPressed?.Invoke();
            _attackAction.performed += ctx =>
            {
                if (!IsPointerOverUI())
                    OnAttackPressed?.Invoke();
            };
        }

        private void OnEnable()
        {
            _moveAction.Enable();
            _jumpAction.Enable();
            _interactAction.Enable();
            _attackAction.Enable();

            _moveAction.performed += ctx => HandleMoveInputChanged(ctx.ReadValue<Vector2>());
            _moveAction.canceled += ctx => HandleMoveInputChanged(Vector2.zero);
        }

        private void OnDisable()
        {
            _moveAction.Disable();
            _jumpAction.Disable();
            _interactAction.Disable();
            _attackAction.Disable();
        }
        
        private void HandleMoveInputChanged(Vector2 moveInput)
        {
            OnMoveInputChanged?.Invoke(moveInput);
        }
        
        private bool IsPointerOverUI()
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }
    }
}