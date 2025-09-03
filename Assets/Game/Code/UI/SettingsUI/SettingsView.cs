using System;
using UnityEngine;
using UnityEngine.UI;

namespace SteveAdventure
{
    public sealed class SettingsView : MonoBehaviour, ISettingsView
    {
        [SerializeField] private Slider _musicSlider;           
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundToggle;
        [SerializeField] private Button _backButton;

        private ISettingsViewModel _viewModel;
        
        public void Init(ISettingsViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnBackButtonClicked()
        {
            Hide();
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
        }
    }
}