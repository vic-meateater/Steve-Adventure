using Zenject;

namespace SteveAdventure.Helpers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InputHandlerInstaller.Install(Container);
            MoveControllerInstaller.Install(Container);
        }
    }
}