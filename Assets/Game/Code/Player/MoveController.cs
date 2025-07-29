using UnityEngine;

namespace SteveAdventure
{
    public sealed class MoveController : MonoBehaviour,
        IGameStartListener,
        IGamePauseListener,
        IGameResumeListener,
        IGameFinishListener
    {
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private Player _player;

        public void OnGameStart()
        {
            _inputHandler.MoveInputChanged += _player.OnMoveInputChanged;
            _inputHandler.AttackPressed += _player.OnAttackPressed;
            _inputHandler.InteractPressed += _player.OnInteractPressed;
            _inputHandler.SpacePressed += _player.OnSpacePressed;
        }

        public void OnGamePause()
        {
            _inputHandler.MoveInputChanged -= _player.OnMoveInputChanged;
            _inputHandler.AttackPressed -= _player.OnAttackPressed;
            _inputHandler.InteractPressed -= _player.OnInteractPressed;
            _inputHandler.SpacePressed -= _player.OnSpacePressed;
        }

        public void OnGameResume()
        {
            _inputHandler.MoveInputChanged += _player.OnMoveInputChanged;
            _inputHandler.AttackPressed += _player.OnAttackPressed;
            _inputHandler.InteractPressed += _player.OnInteractPressed;
            _inputHandler.SpacePressed += _player.OnSpacePressed;
        }

        public void OnGameFinish()
        {
            _inputHandler.MoveInputChanged -= _player.OnMoveInputChanged;
            _inputHandler.AttackPressed -= _player.OnAttackPressed;
            _inputHandler.InteractPressed -= _player.OnInteractPressed;
            _inputHandler.SpacePressed -= _player.OnSpacePressed;
        }
    }
}