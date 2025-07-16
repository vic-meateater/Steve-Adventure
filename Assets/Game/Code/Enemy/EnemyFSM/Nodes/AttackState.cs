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
        }

        public override void Update()
        {
            if (IsTimeOver() && _enemyVision.CanAttack())
            {
                Attack();
            }
        }
        
        public bool ShouldExitAttackState()
        {
            return !_enemyVision.CanAttack();
        }

        private void Attack()
        {
            _endTime = Time.time + _attackCooldown;

            if (_enemyVision.TryGetTargetInAttackRange(out var target))
            {
                if (target.TryGetComponent(out IDamageable damageable))
                {
                    _animatorController.AttackAnimation();
                    damageable.TakeDamage(_damage);
                    Debug.Log($"Attacked {target.name} for {_damage} damage.");
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