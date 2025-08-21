using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace SteveAdventure
{
    public sealed class EnemyVision : MonoBehaviour, IGameFixedUpdateListener
    {
        private const float VISION_OFFSET_MULTIPLIER = 2f;
        private const float SEARCH_INTERVAL = 0.1f;

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
        private float _lastSearchTime;
        private GameCycle _gameCycle;

        private void Start()
        {
            //GameCycleService.Instance?.AddListener(this);
        }

        private void OnDestroy()
        {
            //GameCycleService.Instance?.RemoveListener(this);
        }

        [Inject]
        public void Construct(GameCycle gameCycle)
        {
            Debug.Log("EnemyVision.Construct");
            _gameCycle = gameCycle;
            
        }

        private void OnEnable()
        {
            _gameCycle.AddListener(this);
        }

        public void OnGameFixedUpdate(float deltaTime)
        {
            if (Time.time - _lastSearchTime > SEARCH_INTERVAL)
            {
                FindTarget();
                _lastSearchTime = Time.time;
            }
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
            return _isTargetInRange;
        }

        public bool CanSeeTargetDirectly()
        {
            return _canSeeTarget;
        }

        public bool CanAttack()
        {
            return CanSeeTargetDirectly() &&
                   (_targetPosition - (Vector2) transform.position).magnitude < _attackRange;
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
            }
            else
            {
                if (Time.time - _lastTargetSeenTime > _targetLostDelay)
                {
                    _isTargetInRange = false;
                    _target = null;
                    _targetPosition = Vector2.zero;
                }
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