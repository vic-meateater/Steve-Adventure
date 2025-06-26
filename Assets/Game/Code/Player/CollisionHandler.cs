using System;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class CollisionHandler : MonoBehaviour
    {
        private IInteractable _currentTarget;

        public bool CanInteract => _currentTarget != null;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IInteractable interactable))
            {
                _currentTarget = interactable;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IInteractable interactable))
            {
                if (_currentTarget == interactable)
                    _currentTarget = null;
            }
        }
        
        public void TryInteract()
        {
            if (_currentTarget is { } interactable)
                interactable.Interact();
        }
    }
}