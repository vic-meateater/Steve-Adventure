namespace SteveAdventure
{
    public static class GameCycleService
    {
        public static GameCycle Instance { get; private set; }

        public static void Register(GameCycle gameCycle)
        {
            Instance = gameCycle;
        }

        public static void Unregister()
        {
            Instance = null;
        }
    }
}