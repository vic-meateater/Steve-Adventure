using UnityEngine;

namespace SteveAdventure
{
    public sealed class InteractObject : MonoBehaviour, IInteractable
    {
        private static readonly int ActivatedTrigger = Animator.StringToHash("Activated");
        private static readonly int DeactivatedTrigger = Animator.StringToHash("Deactivated");

        [SerializeField] private Animator _animator;

        private bool _isActivated;

        public void Interact()
        {
            _isActivated = !_isActivated;
            if (_animator)
                _animator.SetTrigger(_isActivated ? ActivatedTrigger : DeactivatedTrigger);
        }
    }
}