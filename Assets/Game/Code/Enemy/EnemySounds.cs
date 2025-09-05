using UnityEngine;

namespace SteveAdventure
{
    public sealed class EnemySounds : IEnemySounds
    {
        private readonly EnemyConfig _enemyConfig;
        private readonly IAudioManager _audioManager;
        
        private float _nextFootstepTime;

        public EnemySounds(EnemyConfig enemyConfig, IAudioManager audioManager)
        {
            _enemyConfig = enemyConfig;
            _audioManager = audioManager;
        }

        public void PlayFootstepSound()
        {
            if (_enemyConfig.FootstepSounds.Capacity == 0) return;
            int randomIndex = Random.Range(0, _enemyConfig.FootstepSounds.Capacity);
            if (_nextFootstepTime < Time.time)
            {
                _nextFootstepTime = _enemyConfig.FootstepSounds[randomIndex].length + Time.time;
                _audioManager.PlaySound(_enemyConfig.FootstepSounds[randomIndex]);
            }
        }

        public void PlayHitSound()
        {
            _audioManager.PlaySound(_enemyConfig.HitSound);
        }

        public void PlayPainSound()
        {
            _audioManager.PlaySound(_enemyConfig.PainSound);
        }

        public void DeathSound()
        {
            _audioManager.PlaySound(_enemyConfig.DeathSound);
        }
    }

    public interface IEnemySounds
    {
        void PlayFootstepSound();
        void PlayHitSound();
        void PlayPainSound();
        void DeathSound();
    }
}