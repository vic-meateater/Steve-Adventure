using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace SteveAdventure
{
    public sealed class InputHandler : IGameUpdateListener, IGameFixedUpdateListener
    {
        public event Action SpacePressed;
        public event Action<Vector2> MoveInputChanged;
        public event Action InteractPressed;
        public event Action AttackPressed;
        
        private Vector2 _moveInput;

        private float _directionX;
        private float _directionY;
        
        [Inject]
        public void Construct()
        {
            Debug.Log("InputHandler constructed");
            GameCycleService.Instance?.AddListener(this);
        }

        
        private bool IsPointerOverUI()
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }

        public void OnGameUpdate(float deltaTime)
        {
            _directionX = Input.GetAxis("Horizontal");
            _directionY = Input.GetAxis("Vertical");
            _moveInput = new Vector2(_directionX, _directionY);

            if (Input.GetKeyDown(KeyCode.Space))
                SpacePressed?.Invoke();

            if(Input.GetKeyDown(KeyCode.F))
                InteractPressed?.Invoke();
            
            if(Input.GetKeyDown(KeyCode.Mouse0) && !IsPointerOverUI())
                AttackPressed?.Invoke();
        }

        public void OnGameFixedUpdate(float fixedDeltaTime)
        {
            Debug.LogWarning("InputHandler magnitude: " + _moveInput.sqrMagnitude);
            if(_moveInput.sqrMagnitude > 0f)
                MoveInputChanged?.Invoke(_moveInput);
        }
    }
}