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
            _musicSlider.onValueChanged.AddListener(_viewModel.OnMusicVolumeChanged);
            _soundSlider.onValueChanged.AddListener(_viewModel.OnSoundVolumeChanged);
            _musicToggle.onValueChanged.AddListener(_viewModel.OnMusicToggleChanged);
            _soundToggle.onValueChanged.AddListener(_viewModel.OnSoundToggleChanged);
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnBackButtonClicked()
        {
            Hide();
        }

        private void OnDisable()
        {
            _musicSlider.onValueChanged.RemoveListener(_viewModel.OnMusicVolumeChanged);
            _soundSlider.onValueChanged.RemoveListener(_viewModel.OnSoundVolumeChanged);
            _musicToggle.onValueChanged.RemoveListener(_viewModel.OnMusicToggleChanged);
            _soundToggle.onValueChanged.RemoveListener(_viewModel.OnSoundToggleChanged);
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
        }
    }
}