using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SteveAdventure
{
    public class PlayerUIView : MonoBehaviour
    {
        [SerializeField] private Slider _hpBar;
        [SerializeField] private TMP_Text _hpText;

        private IHealthViewModel _healthViewModel;

        public void Init(IHealthViewModel healthViewModel)
        {
            _healthViewModel = healthViewModel;

            _healthViewModel.CurrentHealth.Subscribe(OnHealthChanged).AddTo(this);
           // _healthViewModel.CurrentHealth.Subscribe(health => _hpText.text = health.ToString()).AddTo(this);
            gameObject.SetActive(true);
        }
        
        private void OnEnable()
        {
            _hpBar.maxValue = _healthViewModel.MaxHealth.CurrentValue;
            _hpBar.value = _healthViewModel.CurrentHealth.CurrentValue;
            _hpText.text = _healthViewModel.HealthText;
        }

        private void OnHealthChanged(float health)
        {
            _hpBar.value = health;
            _hpText.text = _healthViewModel.HealthText;
        }
    }
}