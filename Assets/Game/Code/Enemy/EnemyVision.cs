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

        private Vector2 _directionOffset = Vector2.right;
        private Vector2 _targetPosition;
        private bool _targetInRange;
        private bool _canSeeTarget = true;

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
            if (_targetInRange)
            {
                targetPosition = _targetPosition;
                return true;
            }

            targetPosition = Vector2.zero;
            return false;
        }

        public bool IsTargetInDetectionRange() => _targetInRange;

        public bool CanSeeTargetDirectly() => _targetInRange && _canSeeTarget;


        private void FindTarget()
        {
            Vector2 origin = GetLookAreaOrigin();
            Collider2D hit = Physics2D.OverlapBox(origin, _visionAreaSize, 0f, _playerLayer);

            if (hit != null)
            {
                Vector2 direction = (hit.transform.position - transform.position).normalized;
                RaycastHit2D visionHit = Physics2D.Raycast(
                    transform.position,
                    direction,
                    _visionAreaSize.x,
                    ~(1 << gameObject.layer));

                Debug.DrawLine(transform.position, visionHit.point,
                    visionHit.collider == hit ? Color.red : Color.yellow);

                _targetInRange = true;
                _targetPosition = hit.transform.position;
                _canSeeTarget = visionHit.collider == hit;
            }
            else
            {
                _canSeeTarget = false;
                _targetInRange = false;
                _targetPosition = Vector2.zero;
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