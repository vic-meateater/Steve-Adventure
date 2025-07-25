using UnityEngine;
using UnityEngine.UI;

namespace SteveAdventure
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        
        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
        }
        
        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
        }

        private void OnPauseButtonClicked()
        {
            Debug.Log("Pause button clicked");
        }
    }
}
