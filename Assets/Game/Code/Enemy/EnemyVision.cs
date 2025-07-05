using UnityEngine;

namespace SteveAdventure
{
    public class EnemyVision : MonoBehaviour
    {
        private const float VISION_OFFSET_MULTIPLIER = 2f;

        [SerializeField] private Vector2 _visionAreaSize;
        [SerializeField] private LayerMask _targetLayer;

        private Vector2 _directionOffset = Vector2.right;

        private void FixedUpdate()
        {
            Vector2 origin = GetLookAreaOrigin();
            Collider2D hit = Physics2D.OverlapBox(origin, _visionAreaSize, 0f, _targetLayer);
            
            if (hit != null)
            {
                Vector2 direction = (hit.transform.position - transform.position).normalized;
                RaycastHit2D visionHit = Physics2D.Raycast(
                    transform.position, 
                    direction, 
                    _visionAreaSize.x,
                    ~(1 << gameObject.layer));

                Debug.DrawLine(transform.position, visionHit.point,
                    visionHit.collider == hit ? Color.red : Color.magenta);
            }
        }

        private Vector2 GetLookAreaOrigin()
        {
            float originX = transform.position.x + _visionAreaSize.x / VISION_OFFSET_MULTIPLIER * _directionOffset.x;
            float originY = transform.position.y + _visionAreaSize.y / VISION_OFFSET_MULTIPLIER * _directionOffset.y;

            return new Vector2(originX, originY);
        }

        public void SetDirection(Vector2 direction)
        {
            if (direction.sqrMagnitude > 0.1f)
                _directionOffset = direction;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(GetLookAreaOrigin(), _visionAreaSize);
        }
    }
}