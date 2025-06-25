using UnityEngine;

namespace SteveAdventure
{
    public sealed class PlayerAnimationController : MonoBehaviour
    {
        private static readonly int RunXAnimation = Animator.StringToHash("RunX");
        private static readonly int RunYAnimation = Animator.StringToHash("RunY");
        private static readonly int Speed = Animator.StringToHash("Speed");

        [SerializeField] private Animator _animator;

        public void MoveAnimation(Vector2 moveInput)
        {
            if (moveInput.magnitude > 1f)
                moveInput.Normalize();
            
            _animator.SetFloat(RunXAnimation, moveInput.x);
            _animator.SetFloat(RunYAnimation, moveInput.y);
            _animator.SetFloat(Speed, moveInput.magnitude);
        }
    }
}
