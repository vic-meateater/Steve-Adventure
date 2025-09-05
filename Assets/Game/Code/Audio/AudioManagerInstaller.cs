using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public class AudioManagerInstaller : MonoInstaller
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioClip _defaultMusic;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AudioManager>()
                .AsSingle()
                .WithArguments(_musicSource, _soundSource, _defaultMusic)
                .NonLazy();
        }
    }
}