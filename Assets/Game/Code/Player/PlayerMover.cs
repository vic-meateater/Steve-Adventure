using System.Collections;
using UnityEngine;

namespace SteveAdventure
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        private const float MOVE_SPEED_MULTIPLER = 50f;

        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _dashSpeed = 3f;
        [SerializeField] private float _dashDuration = 0.5f;

        private Rigidbody2D _rb2D;
        private float _playerSpeed;
        private float _directionX;
        private float _directionY;
        private bool _isDashing;

        private void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _moveSpeed *= MOVE_SPEED_MULTIPLER;
            _dashSpeed *= MOVE_SPEED_MULTIPLER;
            _playerSpeed = _moveSpeed;
        }

        public void Moving(Vector2 moveInput)
        {
            if (moveInput.sqrMagnitude > 0.01f)
            {
                _rb2D.linearVelocity = moveInput * (_playerSpeed * Time.fixedDeltaTime);
            }
            else
            {
                _rb2D.linearVelocity = Vector2.zero;
            }
        }

        public void Dashing()
        {
            if(!_isDashing)
                StartCoroutine(Dash());
        }

        private IEnumerator Dash()
        {
            Debug.Log("Dash activated");
            _isDashing = true;
            _playerSpeed = _dashSpeed;
            yield return new WaitForSeconds(_dashDuration);
            _isDashing = false;
            _playerSpeed = _moveSpeed;
            Debug.Log("Dash deactivated");
        }

    }
}