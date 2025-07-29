namespace SteveAdventure
{
    public interface IGameListener
    {
    }

    public interface IGameStartListener : IGameListener
    {
        void OnGameStart();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnGamePause();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnGameResume();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnGameFinish();
    }

    public interface IGameUpdateListener : IGameListener
    {
        void OnGameUpdate(float deltaTime);
    }

    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnGameFixedUpdate(float fixedDeltaTime);
    }
}