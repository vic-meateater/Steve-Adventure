using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SteveAdventure
{
    public sealed class EnemyVision : MonoBehaviour
    {
        private const float VISION_OFFSET_MULTIPLIER = 2f;

        [SerializeField] private Vector2 _visionAreaSize;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _targetLostDelay = .5f;
        
        private Vector2 _directionOffset = Vector2.down;
        private Vector2 _targetPosition;
        private bool _isTargetInRange;
        private bool _canSeeTarget;
        private GameObject _target;
        private float _lastTargetSeenTime;

        private void FixedUpdate()
        {
            FindTarget();
        }

        public void SetVisionDirection(Vector2 direction)
        {
            if (direction.sqrMagnitude > 0.1f)
                _directionOffset = direction;
        }

        public bool TryGetTargetPosition(out Vector2 targetPosition)
        {
            if (_isTargetInRange)
            {
                targetPosition = _targetPosition;
                return true;
            }

            targetPosition = Vector2.zero;
            return false;
        }

        public bool TryGetTargetInAttackRange(out GameObject target)
        {
            if (_target)
            {
                target = _target;
                return true;
            }

            target = null;
            return false;
        }

        public bool IsTargetInDetectionRange()
        {
            Debug.Log($"IsTargetInDetectionRange: {_isTargetInRange}");
            return _isTargetInRange;
        }

        public bool CanSeeTargetDirectly()
        {
            Debug.Log($"CanSeeTargetDirectly: {_canSeeTarget}");
            return _canSeeTarget;
        }

        public bool CanAttack()
        {
            return CanSeeTargetDirectly() &&
                   (_targetPosition - (Vector2)transform.position).magnitude < _attackRange;
        }

        private void FindTarget()
        {
            Vector2 origin = GetLookAreaOrigin();
            Collider2D hit = Physics2D.OverlapBox(origin, _visionAreaSize, 0f, _playerLayer);

            if (hit)
            {
                Vector2 direction = (hit.transform.position - transform.position).normalized;
                RaycastHit2D visionHit = Physics2D.Raycast(
                    transform.position,
                    direction,
                    _visionAreaSize.x,
                    ~(1 << gameObject.layer));

                _canSeeTarget = visionHit.collider == hit;

                if (_canSeeTarget)
                {
                    _isTargetInRange = true;
                    _targetPosition = hit.transform.position;
                    _target = hit.transform.gameObject;
                    _lastTargetSeenTime = Time.time;
                }
                
                Debug.DrawLine(transform.position, visionHit.point,
                    visionHit.collider == hit ? Color.red : Color.yellow);
                
                Debug.Log($"Target in range positive: {_isTargetInRange}");
                Debug.Log($"Can see target positive: {_canSeeTarget}");
            }
            else
            {
                if (Time.time - _lastTargetSeenTime > _targetLostDelay)
                {
                    _isTargetInRange = false;
                    _target = null;
                    _targetPosition = Vector2.zero;
                }
                Debug.Log($"Target in range negative: {_isTargetInRange}");
                Debug.Log($"Can see target negative: {_canSeeTarget}");
            }

        }

        private Vector2 GetLookAreaOrigin()
        {
            float originX = transform.position.x + _visionAreaSize.x / VISION_OFFSET_MULTIPLIER * _directionOffset.x;
            float originY = transform.position.y + _visionAreaSize.y / VISION_OFFSET_MULTIPLIER * _directionOffset.y;

            return new Vector2(originX, originY);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(GetLookAreaOrigin(), _visionAreaSize);
        }
    }
}