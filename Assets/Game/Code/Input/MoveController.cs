using Zenject;

namespace SteveAdventure
{
    public sealed class MoveController :
        IGamePauseListener,
        IGameResumeListener,
        IGameFinishListener
    {
        private InputHandler _inputHandler;
        private Player _player;

        [Inject]
        public void Construct(InputHandler inputHandler, Player player)
        {
            _inputHandler = inputHandler;
            _player = player;
            
            Subscribes();
        }

        public void OnGamePause()
        {
            UnSubscribes();
        }


        public void OnGameResume()
        {
            Subscribes();
        }

        public void OnGameFinish()
        {
            UnSubscribes();
        }

        private void Subscribes()
        {
            _inputHandler.MoveInputChanged += _player.OnMoveInputChanged;
            _inputHandler.AttackPressed += _player.OnAttackPressed;
            _inputHandler.InteractPressed += _player.OnInteractPressed;
            _inputHandler.SpacePressed += _player.OnSpacePressed;
        }

        private void UnSubscribes()
        {
            _inputHandler.MoveInputChanged -= _player.OnMoveInputChanged;
            _inputHandler.AttackPressed -= _player.OnAttackPressed;
            _inputHandler.InteractPressed -= _player.OnInteractPressed;
            _inputHandler.SpacePressed -= _player.OnSpacePressed;
        }
    }
}