using UnityEngine;

namespace EscapeFromPiggy.Utilities
{
    /// <summary>
    /// Utility component for detecting ground and walls.
    /// Can be used as a standalone component or inherited.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class DetectGround : MonoBehaviour
    {
        [Header("Ground Check")]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.9f, 0.1f);
        [SerializeField] private Vector2 _groundCheckOffset = new Vector2(0f, -0.5f);

        [Header("Wall Check")]
        [SerializeField] private float _wallCheckDistance = 0.6f;

        // Public properties for external access
        public bool IsGrounded { get; private set; }
        public bool IsTouchingWall { get; private set; }

        private void FixedUpdate()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            // Ground check
            Vector2 position = (Vector2)transform.position + _groundCheckOffset;
            IsGrounded = Physics2D.OverlapBox(position, _groundCheckSize, 0f, _groundLayer);

            // Wall check - detect wall in facing direction
            float direction = Mathf.Sign(transform.localScale.x);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, _wallCheckDistance, _groundLayer);
            IsTouchingWall = hit.collider != null;
        }

        private void OnDrawGizmosSelected()
        {
            // Visualize ground check in editor
            Gizmos.color = IsGrounded ? Color.green : Color.red;
            Vector2 position = (Vector2)transform.position + _groundCheckOffset;
            Gizmos.DrawWireCube(position, _groundCheckSize);

            // Visualize wall check
            Gizmos.color = IsTouchingWall ? Color.yellow : Color.blue;
            float direction = Mathf.Sign(transform.localScale.x);
            Vector3 wallCheckEnd = transform.position + Vector3.right * direction * _wallCheckDistance;
            Gizmos.DrawLine(transform.position, wallCheckEnd);
        }
    }
}
