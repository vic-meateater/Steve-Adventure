using R3;
using UnityEngine;
using UnityEngine.UI;

namespace SteveAdventure
{
    public class EnemyUIView : MonoBehaviour
    {
        [SerializeField] private Slider _hpSlider;

        private IHealthViewModel _healthViewModel;

        public void Init(IHealthViewModel healthViewModel)
        {
            _healthViewModel = healthViewModel;
            
            _hpSlider.maxValue = _healthViewModel.MaxHealth.CurrentValue;
            _hpSlider.value = _healthViewModel.CurrentHealth.CurrentValue;

            _healthViewModel.CurrentHealth.Subscribe(OnHealthChanged).AddTo(this);
            gameObject.SetActive(true);
        }
        
        private void OnHealthChanged(float health)
        {
            _hpSlider.value = health;
        }
    }
}