using System;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class InteractToggleObject : MonoBehaviour, IInteractable
    {
        private static readonly int ActivatedTrigger = Animator.StringToHash("Activated");
        private static readonly int DeactivatedTrigger = Animator.StringToHash("Deactivated");

        public event Action<InteractToggleObject> OnStateChanged;
        public bool IsActivated => _isActivated;
        
        [SerializeField] private Animator _animator;

        private bool _isActivated;

        public void Interact()
        {
            _isActivated = !_isActivated;
            if (_animator)
                _animator.SetTrigger(_isActivated ? ActivatedTrigger : DeactivatedTrigger);
            OnStateChanged?.Invoke(this);
        }
    }
}