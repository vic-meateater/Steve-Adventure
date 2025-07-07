using UnityEngine;

namespace SteveAdventure
{
    public sealed class AnimatorController : MonoBehaviour
    {
        private static readonly int MoveX = Animator.StringToHash(nameof(MoveX));
        private static readonly int MoveY = Animator.StringToHash(nameof(MoveY));
        private static readonly int Speed = Animator.StringToHash(nameof(Speed));
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");

        [SerializeField] private Animator _animator;
        
        private Vector2 _lastDirection = Vector2.down;

        public void MoveAnimation(Vector2 moveInput)
        {
            Vector2 directionToUse = _lastDirection; //

            if (moveInput.magnitude > 0.01f)
            {
                directionToUse = moveInput.normalized;
                _lastDirection = directionToUse;
            }

            _animator.SetFloat(MoveX, directionToUse.x);
            _animator.SetFloat(MoveY, directionToUse.y);
            _animator.SetFloat(Speed, moveInput.magnitude);
        }
        
        public void AttackAnimation()
        {
            _animator.SetTrigger(AttackTrigger);
        }
    }
}
