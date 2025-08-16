using R3;
using UnityEngine;
using UnityEngine.UI;

namespace SteveAdventure
{
    public class PlayerUIView : MonoBehaviour
    {
        [SerializeField] private Slider _hpBar;

        private IHealthViewModel _healthViewModel;

        public void Show(IHealthViewModel healthViewModel)
        {
            _healthViewModel = healthViewModel;

            _healthViewModel.CurrentHealth.Subscribe(health => _hpBar.value = health).AddTo(this);
            gameObject.SetActive(true);
        }
    }
}