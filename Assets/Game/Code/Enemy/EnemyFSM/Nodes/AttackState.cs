using UnityEngine;

namespace SteveAdventure
{
    public sealed class AttackState : State
    {
        private readonly EnemyVision _enemyVision;
        private readonly AnimatorController _animatorController;
        private readonly Mover _mover;
        private readonly float _attackCooldown;
        private readonly float _damage;
        private float _endTime;


        public AttackState(EnemyBrain brain, EnemyVision enemyVision, float damage, float attackCooldown,
            AnimatorController animatorController, Mover mover) : base(brain)
        {
            _enemyVision = enemyVision;
            _damage = damage;
            _attackCooldown = attackCooldown;
            _animatorController = animatorController;
            _mover = mover;
        }

        public override void Enter()
        {
            Debug.Log("Enter to Attack State");
            _endTime = Time.time + _attackCooldown;
            _mover.Moving(Vector2.zero);
        }

        public override void Update()
        {
            if (IsTimeOver() && _enemyVision.CanAttack())
            {
                Attack();
            }
        }

        private void Attack()
        {
            _endTime = Time.time + _attackCooldown;

            if (_enemyVision.TryGetTargetInAttackRange(out var target))
            {
                if (target.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.TakeDamage(_damage);
                    _animatorController.AttackAnimation();
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