using Zenject;

namespace SteveAdventure
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            GamePausedViewInstaller.Install(Container);
            GameOverViewInstaller.Install(Container);
            SettingsViewInstaller.Install(Container);
            GameViewInstaller.Install(Container);
        }
    }
}