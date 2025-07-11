using System;
using System.Collections.Generic;
using UnityEngine;

namespace SteveAdventure
{
    public sealed class PlayerAttackController : IDisposable
    {
        private AnimationHandler _animationHandler;
        private readonly AnimatorController _animatorController;
        private readonly PlayerVision _playerVision;
        private readonly float _damage;

        public PlayerAttackController(AnimationHandler animationHandler, AnimatorController animatorController,
            PlayerVision playerVision, float damage)
        {
            _animationHandler = animationHandler;
            _animatorController = animatorController;
            _playerVision = playerVision;
            _damage = damage;

            _animationHandler.OnStartAttackFrame += OnStartAttackAction;
            _animationHandler.OnEndAttackFrame += OnEndAttackAction;
        }

        public void AttackRequest()
        {
            _animatorController.AttackAnimation();
        }

        private void OnStartAttackAction()
        {
            if (_playerVision.TryGetTargets(out List<GameObject> targets))
            {
                foreach (var target in targets)
                {
                    if(target.TryGetComponent<IDamageable>(out var damageable))
                        damageable.TakeDamage(_damage);
                }
            }
            else
            {
                Debug.LogWarning("No targets found for attack.");
            }
        }

        private void OnEndAttackAction()
        {
        }

        public void Dispose()
        {
            if (_animationHandler)
            {
                _animationHandler.OnStartAttackFrame -= OnStartAttackAction;
                _animationHandler.OnEndAttackFrame -= OnEndAttackAction;
                _animationHandler = null;
            }
        }
    }
}