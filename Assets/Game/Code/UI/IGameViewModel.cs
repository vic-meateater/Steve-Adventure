namespace SteveAdventure
{
    public interface IGameViewModel : IViewModel
    {
        public IGamePausedViewModel GamePausedViewModel { get; }
    }
}