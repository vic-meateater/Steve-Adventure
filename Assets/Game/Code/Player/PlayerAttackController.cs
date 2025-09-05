using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public sealed class PlayerAttackController : IDisposable
    {
        private AnimationHandler _animationHandler;
        private readonly AnimatorController _animatorController;
        private readonly PlayerVision _playerVision;
        private readonly float _damage;
        private IPlayerSounds _sounds;
        private IAudioManager _audioManager;

        public PlayerAttackController(AnimationHandler animationHandler, AnimatorController animatorController,
            PlayerVision playerVision, float damage, IPlayerSounds sounds)
        {
            _animationHandler = animationHandler;
            _animatorController = animatorController;
            _playerVision = playerVision;
            _damage = damage;
            _sounds = sounds;

            _animationHandler.MeleeAttackStart += OnMeleeAttackStart;
            _animationHandler.AttackEnd += OnAttackEnd;
        }

        public void AttackRequest()
        {
            _animatorController.AttackAnimation();
        }

        private void OnMeleeAttackStart()
        {
            _sounds.PlayHitSound();
            if (_playerVision.TryGetTargets(out List<GameObject> targets))
            {
                foreach (var target in targets)
                {
                    if (target.TryGetComponent(out IDamageable damageable))
                    {
                        Debug.LogWarning("Attacked target: " + target.name + " with damage: " + _damage);
                        damageable.TakeDamage(_damage);
                    }

                    if (target.TryGetComponent(out IAnimator animator))
                        animator.HitAnimation();
                }
            }
            else
            {
                Debug.LogWarning("No targets found for attack.");
            }
        }

        private void OnAttackEnd()
        {
        }

        public void Dispose()
        {
            if (_animationHandler)
            {
                _animationHandler.MeleeAttackStart -= OnMeleeAttackStart;
                _animationHandler.AttackEnd -= OnAttackEnd;
                _animationHandler = null;
            }
        }
    }
}