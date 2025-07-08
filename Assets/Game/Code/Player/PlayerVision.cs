using System.Collections.Generic;
using UnityEngine;

namespace SteveAdventure
{
    public class PlayerVision : MonoBehaviour
    {
        private const float VISION_OFFSET_MULTIPLIER = 2f;

        [SerializeField] private Vector2 _visionAreaSize;
        [SerializeField] private LayerMask _targetLayer;

        private InputHandler _inputHandler;
        private Vector2 _directionOffset = Vector2.down;
        private readonly List<Transform> _visibleTargets = new();

        private void Start()
        {
            _inputHandler = GetComponent<InputHandler>();
            _inputHandler.OnMoveInputChanged += OnMoveInputChangedHandler;
        }

        private void OnDestroy()
        {
            if (_inputHandler != null)
            {
                _inputHandler.OnMoveInputChanged -= OnMoveInputChangedHandler;
            }
        }

        private void OnMoveInputChangedHandler(Vector2 direction)
        {
            if (direction.sqrMagnitude > 0.1f)
            {
                _directionOffset = direction.normalized;
            }
        }

        private void FixedUpdate()
        {
            FindTargets();
        }

        public void SetVisionDirection(Vector2 direction)
        {
            if (direction.sqrMagnitude > 0.1f)
                _directionOffset = direction.normalized;
        }
        
        public bool TryGetTargets(out List<Transform> targets)
        {
            targets = new List<Transform>();
            if (_visibleTargets.Count > 0)
            {
                targets = _visibleTargets;
                return true;
            }
            return false;
        }

        private void FindTargets()
        {
            Vector2 origin = GetLookAreaOrigin();
            Collider2D[] hits = Physics2D.OverlapBoxAll(origin, _visionAreaSize, 0f, _targetLayer);

            if (hits != null)
            {
                _visibleTargets.Clear();
                for (var i = 0; i < hits.Length; i++)
                {
                    var hit = hits[i];
                    Vector2 direction = (hit.transform.position - transform.position).normalized;

                    RaycastHit2D visionHit = Physics2D.Raycast(
                        transform.position,
                        direction,
                        _visionAreaSize.x,
                        ~(1 << gameObject.layer));

                    if (visionHit.collider != null && visionHit.collider == hit)
                    {
                        _visibleTargets.Add(hit.transform);

                        Debug.DrawLine(transform.position, visionHit.point,
                            visionHit.collider == hit ? Color.blue : Color.magenta);
                    }
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