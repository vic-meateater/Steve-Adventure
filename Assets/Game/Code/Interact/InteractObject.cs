using System;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class InteractObject : MonoBehaviour, IInteractable
    {
        private static readonly int TurnOnTrigger = Animator.StringToHash("TurnOn");
        [SerializeField] private Animator _animator;

        public void Interact()
        {
            _animator.SetTrigger(TurnOnTrigger);
        }
    }
}