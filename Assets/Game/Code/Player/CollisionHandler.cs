using UnityEngine;

namespace SteveAdventure
{
    public sealed class CollisionHandler : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.TryGetComponent(out IInteractable interactable))
                interactable.Interact();
        }
    }
}