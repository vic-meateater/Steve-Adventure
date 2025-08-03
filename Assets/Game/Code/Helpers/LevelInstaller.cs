using Zenject;

namespace SteveAdventure.Helpers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            MoveControllerInstaller.Install(Container);
            InputHandlerInstaller.Install(Container);
        }
    }
}