using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class HealthView : MonoBehaviour
    {
        [SerializeField] private float _maxHealth = 100f;

        public HealthModel Model { get; private set; }

        [Inject]
        public void Construct()
        {
            Model = new HealthModel(_maxHealth);
        }
    }
}