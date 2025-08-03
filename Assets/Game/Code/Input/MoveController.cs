using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class MoveController : 
        IGameStartListener,
        IGamePauseListener,
        IGameResumeListener,
        IGameFinishListener
    {
        private InputHandler _inputHandler;
        private Player _player;

        [Inject]
        public void Construct(InputHandler inputHandler, Player player)
        {
            Debug.Log("MoveController Constructed");
            _inputHandler = inputHandler;
            _player = player;

            _inputHandler.MoveInputChanged += OnMoveInputChanged;
            _inputHandler.AttackPressed += _player.OnAttackPressed;
            _inputHandler.InteractPressed += _player.OnInteractPressed;
            _inputHandler.SpacePressed += _player.OnSpacePressed;
        }

        private void OnMoveInputChanged(Vector2 direction)
        {
            Debug.Log("MoveController OnMoveInputChanged: " + direction);
            _player.OnMoveInputChanged(direction);
        }

        public void OnGameStart()
        {
            //
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