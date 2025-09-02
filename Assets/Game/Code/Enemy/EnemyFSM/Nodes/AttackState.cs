using UnityEngine;

namespace SteveAdventure
{ 
    public sealed class AttackState : State
    {
        private readonly Mover _mover;
        private readonly EnemyVision _enemyVision;
        private readonly AnimatorController _animatorController;
        private readonly AnimationHandler _animatorHandler;
        private readonly IEnemySounds _sounds;
        private readonly float _attackCooldown;
        private readonly float _damage;
        private IDamageable _damageable;
        private float _endTime;
        private bool _inAttackAnimation;
        private Vector2 _savedDirection;


        public AttackState(Mover mover, EnemyVision enemyVision, AnimatorController animatorController, float damage,
            float attackCooldown, AnimationHandler animationHandler, IEnemySounds enemySounds)

        {
            _mover = mover;
            _enemyVision = enemyVision;
            _animatorController = animatorController;
            _animatorHandler = animationHandler;
            _sounds = enemySounds;
            _attackCooldown = attackCooldown;
            _damage = damage;
        }

        public override void Enter()
        {
            _endTime = Time.time + _attackCooldown;
            _mover.Moving(Vector2.zero);
            _animatorController.MoveAnimation(Vector2.zero);
            _animatorHandler.MeleeAttackStart += OnMeleeAttackStart;
            _animatorHandler.AttackEnd += OnAttackEnd;
        }

        public override void Exit()
        {
            Debug.Log("Exiting Attack State");
            _animatorHandler.MeleeAttackStart -= OnMeleeAttackStart;
            _animatorHandler.AttackEnd -= OnAttackEnd;

            _inAttackAnimation = false;
        }

        public override void Update()
        {
            if (IsTimeOver() && _enemyVision.CanAttack() && !_inAttackAnimation)
            {
                Attack();
            }
        }

        public override void OnGameResume()
        {
            _endTime = Time.time + _attackCooldown;
            var zeroDirection = Vector2.zero;
            _mover.Moving(zeroDirection);
            _animatorController.MoveAnimation(zeroDirection);
        }

        private void OnMeleeAttackStart()
        {
            _sounds.PlayHitSound();
            if (_damageable != null)
            {
                _damageable.TakeDamage(_damage);
            }
            else
            {
                Debug.LogWarning("No damageable target found for melee attack.");
            }
        }

        private void OnAttackEnd()
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
                /*if(target.TryGetComponent(out IAnimator animator))
                    animator.HitAnimation();*/
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