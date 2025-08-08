using Zenject;

namespace SteveAdventure.Helpers
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InputHandlerInstaller.Install(Container);
            MoveControllerInstaller.Install(Container);
            
            GameViewInstaller.Install(Container);
            
            NonMonoClassesInstaller();
        }

        private void NonMonoClassesInstaller()
        {
            Container.Bind<CameraController>().AsSingle().NonLazy();
            Container.Bind<CharacterConfig>().AsSingle().NonLazy();
        }
    }
}