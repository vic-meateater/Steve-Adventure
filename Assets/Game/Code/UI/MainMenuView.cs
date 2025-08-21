using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SteveAdventure
{
    public class MainMenuView : MonoBehaviour
    {
        private const int MAIN_SCENE_INDEX = 1;
        
        [SerializeField] private Button _playButton;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
        }
        
        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            SceneManager.LoadScene(MAIN_SCENE_INDEX);
        }
    }
}