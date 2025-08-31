using UnityEngine;

namespace SteveAdventure
{
    public interface IAudioManager
    {
        public void PlaySound(AudioClip clip);
        public void PlayMusic(AudioClip clip);
    }
}