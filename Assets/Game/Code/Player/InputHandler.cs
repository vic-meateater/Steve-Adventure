using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SteveAdventure
{
    public sealed class InputHandler : MonoBehaviour, IGameUpdateListener, IGameFixedUpdateListener
    {
        public event Action SpacePressed;
        public event Action<Vector2> MoveInputChanged;
        public event Action InteractPressed;
        public event Action AttackPressed;
        
        private Vector2 _moveInput;

        private float _directionX;
        private float _directionY;

        // private void Update()
        // {
        //     _directionX = Input.GetAxis("Horizontal");
        //     _directionY = Input.GetAxis("Vertical");
        //     _moveInput = new Vector2(_directionX, _directionY);
        //     
        //     
        //     if (Input.GetKeyDown(KeyCode.Space))
        //         OnSpacePressed?.Invoke();
        //     
        //     if(Input.GetKeyDown(KeyCode.F))
        //         OnInteractPressed?.Invoke();
        //     
        //     if(Input.GetKeyDown(KeyCode.Mouse0) && !IsPointerOverUI())
        //         OnAttackPressed?.Invoke();
        // }

        // private void FixedUpdate()
        // {
        //     if(_moveInput.sqrMagnitude > 0f)
        //         OnMoveInputChanged?.Invoke(_moveInput);
        // }
        
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
            if(_moveInput.sqrMagnitude > 0f)
                MoveInputChanged?.Invoke(_moveInput);
        }
    }
}