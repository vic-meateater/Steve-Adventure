using System;
using UnityEngine;

namespace SteveAdventure
{
    public class InputHandler : MonoBehaviour
    {
        public event Action OnSpacePressed;
        public event Action<Vector2> OnMoveInputChanged;
        public event Action OnActionPressed;
        
        private Vector2 _moveInput;

        private float _directionX;
        private float _directionY;

        private void Update()
        {
            _directionX = Input.GetAxis("Horizontal");
            _directionY = Input.GetAxis("Vertical");
            _moveInput = new Vector2(_directionX, _directionY);
            
            
            if (Input.GetKeyDown(KeyCode.Space))
                OnSpacePressed?.Invoke();
            
            if(Input.GetKeyDown(KeyCode.F))
                OnActionPressed?.Invoke();
        }

        private void FixedUpdate()
        {
            if(_moveInput.sqrMagnitude > 0f)
                OnMoveInputChanged?.Invoke(_moveInput);
        }
    }
}