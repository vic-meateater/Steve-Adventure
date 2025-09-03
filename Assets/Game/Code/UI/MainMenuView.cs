using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace SteveAdventure
{
    public class MainMenuView : MonoBehaviour
    {
        private const int MAIN_SCENE_INDEX = 1;

        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private SettingsView _settingsView;

        private IFactory<ISettingsViewModel> _settingsViewModelFactory;
        private ISettingsViewModel _settingsViewModel;
       
        [Inject]
        private void Construct(IFactory<ISettingsViewModel> factory)
        {
            _settingsViewModelFactory = factory;
        }

        private void Awake()
        {
            _settingsViewModel ??= _settingsViewModelFactory.Create();
            _settingsView.Init(_settingsViewModel);
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        }

        private void OnSettingsButtonClicked()
        {
            _settingsView.Show();
        }


        private void OnPlayButtonClicked()
        {
            SceneManager.LoadScene(MAIN_SCENE_INDEX);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        }
    }
}