using UnityEngine;

namespace SteveAdventure
{
    public sealed class AnimatorController : MonoBehaviour
    {
        private static readonly int MoveX = Animator.StringToHash(nameof(MoveX));
        private static readonly int MoveY = Animator.StringToHash(nameof(MoveY));
        private static readonly int Speed = Animator.StringToHash(nameof(Speed));

        [SerializeField] private Animator _animator;

        public void MoveAnimation(Vector2 moveInput)
        {
            if (moveInput.magnitude > 1f)
                moveInput.Normalize();
            
            _animator.SetFloat(MoveX, moveInput.x);
            _animator.SetFloat(MoveY, moveInput.y);
            _animator.SetFloat(Speed, moveInput.magnitude);
        }
    }
}
