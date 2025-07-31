using UnityEngine;
using UnityEngine.UI;

namespace SteveAdventure
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _playButton;
        
        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(() => GameCycleService.Instance?.PauseGame());
            _playButton.onClick.AddListener(() => GameCycleService.Instance?.ResumeGame());
        }
        
        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(() => GameCycleService.Instance?.PauseGame());
            _playButton.onClick.RemoveListener(() => GameCycleService.Instance?.ResumeGame());
        }
    }
}
