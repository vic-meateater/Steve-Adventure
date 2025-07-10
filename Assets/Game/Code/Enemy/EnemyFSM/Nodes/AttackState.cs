using UnityEngine;

namespace SteveAdventure
{
    public sealed class AttackState : State
    {
        private readonly EnemyVision _enemyVision;
        private readonly float _waitDuration;
        private readonly float _damage;
        private float _endTime;
        

        public AttackState(EnemyBrain brain, EnemyVision enemyVision, float damage) : base(brain)
        {
            _enemyVision = enemyVision;
            _damage = damage;
        }

        public override void Enter()
        {
            Debug.Log("Enter to Attack State");
            _endTime = Time.time + _waitDuration;
        }

        public override void Update()
        {
            if (IsTimeOver())
            {
                Attack();
            }
        }

        private void Attack()
        {
            if (_enemyVision.TryGetTargetInAttackRange(out var target))
            {
                if (target.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.TakeDamage(_damage);
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