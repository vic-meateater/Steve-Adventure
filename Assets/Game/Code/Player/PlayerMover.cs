using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const float MOVE_SPEED_MULTIPLER = 50f;
    
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _dashSpeed = 3f;
    [SerializeField] private float _dashDuration = 0.5f;
    
    private Rigidbody2D _rb2D;
    private Vector2 _moveInput;
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

    private void Update()
    {
        _directionX = Input.GetAxis("Horizontal");
        _directionY = Input.GetAxis("Vertical");
        _moveInput = new Vector2(_directionX, _directionY);
        
        if (Input.GetKeyDown(KeyCode.Space) && !_isDashing && _moveInput != Vector2.zero)
            StartCoroutine(Dash());
    }

    private void FixedUpdate()
    {
        _rb2D.linearVelocity = _moveInput * (_playerSpeed * Time.fixedDeltaTime);
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