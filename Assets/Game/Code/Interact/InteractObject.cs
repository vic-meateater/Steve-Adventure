using System;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class InteractObject : MonoBehaviour, IInteractable
    {
        public event Action<InteractObject> OnInteracted; 
        public void Interact()
        {
            OnInteracted?.Invoke(this);
        }
    }
}