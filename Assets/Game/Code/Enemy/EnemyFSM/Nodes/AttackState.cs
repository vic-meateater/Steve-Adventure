using UnityEngine;

namespace SteveAdventure
{
    public sealed class AttackState : State
    {
        private readonly Mover _mover;
        private readonly EnemyVision _enemyVision;
        private readonly AnimatorController _animatorController;
        private readonly AnimationHandler _animatorHandler;
        private readonly float _attackCooldown;
        private readonly float _damage;
        private float _endTime;
        private IDamageable _damageable;
        private bool _inAttackAnimation;


        public AttackState(Mover mover, EnemyVision enemyVision, AnimatorController animatorController, float damage,
            float attackCooldown, AnimationHandler animationHandler)

        {
            _mover = mover;
            _enemyVision = enemyVision;
            _animatorController = animatorController;
            _animatorHandler = animationHandler;
            _attackCooldown = attackCooldown;
            _damage = damage;
        }

        public override void Enter()
        {
            Debug.Log("Enter to Attack State");
            _endTime = Time.time + _attackCooldown;
            _mover.Moving(Vector2.zero);
            _animatorController.MoveAnimation(Vector2.zero);
            _animatorHandler.MeleeAttackStart += OnMeleeAttackStart;
            _animatorHandler.EndAttack += OnEndAttack;
        }

        public override void Exit()
        {
            _animatorHandler.MeleeAttackStart -= OnMeleeAttackStart;
            _animatorHandler.EndAttack -= OnEndAttack;

            _inAttackAnimation = false;
        }

        public override void Update()
        {
            if (IsTimeOver() && _enemyVision.CanAttack() && !_inAttackAnimation)
            {
                Attack();
            }
        }

        private void OnMeleeAttackStart()
        {
            if (_damageable != null)
            {
                _damageable.TakeDamage(_damage);
            }
            else
            {
                Debug.LogWarning("No damageable target found for melee attack.");
            }
        }

        private void OnEndAttack()
        {
            _inAttackAnimation = false;
            _damageable = null;
            _endTime = Time.time + _attackCooldown;
        }

        public bool ShouldExitAttackState()
        {
            return !_enemyVision.CanAttack();
        }

        private void Attack()
        {
            if (_enemyVision.TryGetTargetInAttackRange(out var target))
            {
                if (target.TryGetComponent(out IDamageable damageable))
                {
                    _damageable = damageable;
                    _animatorController.AttackAnimation();
                    _inAttackAnimation = true;
                }
            }
            else
            {
                Debug.LogWarning("No targets found for attack.");
            }
        }

        private bool IsTimeOver()
        {
            return Time.time >= _endTime;
        }
    }
}