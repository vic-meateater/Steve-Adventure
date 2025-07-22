using UnityEngine;

namespace SteveAdventure
{
    public sealed class AnimatorController : MonoBehaviour, IAnimator
    {
        private readonly int _hit = Animator.StringToHash("Hit");
        private readonly int _moveX = Animator.StringToHash("MoveX");
        private readonly int _moveY = Animator.StringToHash("MoveY");
        private readonly int _speed = Animator.StringToHash("Speed");
        private readonly int _attackTrigger = Animator.StringToHash("Attack");
        private Vector2 _moveInput;
        

        [SerializeField] private Animator _animator;
        
        private Vector2 _lastDirection = Vector2.down;

        public void MoveAnimation(Vector2 moveInput)
        {
            _moveInput = moveInput;
            Vector2 directionToUse = _lastDirection; 

            if (_moveInput.magnitude > 0.01f)
            {
                directionToUse = _moveInput.normalized;
                _lastDirection = directionToUse;
            }

            _animator.SetFloat(_moveX, directionToUse.x);
            _animator.SetFloat(_moveY, directionToUse.y);
            _animator.SetFloat(_speed, _moveInput.magnitude);
        }
        
        public void AttackAnimation()
        {
            _animator.SetFloat(_speed, _moveInput.magnitude);
            _animator.SetTrigger(_attackTrigger);
        }

        public void HitAnimation()
        {
            _animator.SetTrigger(_hit);
        }
    }

    public interface IAnimator
    {
        public void MoveAnimation(Vector2 moveInput);
        public void AttackAnimation();
        public void HitAnimation();
    } 
}
