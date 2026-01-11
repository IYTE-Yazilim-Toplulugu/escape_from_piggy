using UnityEngine;

namespace EscapeFromPiggy.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _acceleration = 50f;
        [SerializeField] private float _deceleration = 50f;

        [Header("Jump")]
        [SerializeField] private float _jumpForce = 15f;
        [SerializeField] private float _jumpCutMultiplier = 0.4f;
        [SerializeField] private float _baseGravityScale = 5f;
        [SerializeField] private float _fallGravityMultiplier = 2.5f;
        [SerializeField] private float _coyoteTime = 0.1f;
        [SerializeField] private float _jumpBufferTime = 0.1f;

        [Header("Dash")]
        [SerializeField] private float _dashSpeed = 15f;
        [SerializeField] private float _dashDuration = 0.15f;
        [SerializeField] private int _maxDashCharges = 1;

        [Header("Wall")]
        [SerializeField] private float _wallSlideSpeed = 2f;
        [SerializeField] private float _wallJumpForce = 10f;
        [SerializeField] private Vector2 _wallJumpDirection = new Vector2(1f, 1.5f);

        [Header("Ground Check")]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.9f, 0.1f);
        [SerializeField] private Vector2 _groundCheckOffset = new Vector2(0f, -0.5f);

        private Rigidbody2D _rb;
        private BoxCollider2D _collider;

        // State
        private Vector2 _velocity;
        private bool _isGrounded;
        private bool _isTouchingWall;
        private int _dashChargesRemaining;
        private bool _isDashing;

        // Timers
        private float _coyoteTimeCounter;
        private float _jumpBufferCounter;
        private float _dashTimeCounter;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            _dashChargesRemaining = _maxDashCharges;
        }

        private void Update()
        {
            HandleInput();
            UpdateTimers();
            UpdateState();
        }

        private void FixedUpdate()
        {
            if (_isDashing)
            {
                HandleDash();
            }
            else
            {
                HandleMovement();
                HandleJump();
                HandleWall();
            }

            ApplyVelocity();
        }

        private void HandleInput()
        {
            // Jump buffer - remember jump input for short time
            if (Input.GetButtonDown("Jump"))
            {
                _jumpBufferCounter = _jumpBufferTime;
            }

            // Dash input
            if (Input.GetButtonDown("Dash") && _dashChargesRemaining > 0 && !_isDashing)
            {
                StartDash();
            }

            // Wall jump
            if (Input.GetButtonDown("Jump") && _isTouchingWall && !_isGrounded)
            {
                float direction = -Mathf.Sign(transform.localScale.x);
                _velocity = new Vector2(direction * _wallJumpDirection.x, _wallJumpDirection.y) * _wallJumpForce;

                // Flip character
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        private void UpdateTimers()
        {
            // Coyote time - grace period after leaving ground
            if (_isGrounded)
            {
                _coyoteTimeCounter = _coyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }
            
            // Jump buffer countdown
            if (_jumpBufferCounter > 0f)
            {
                _jumpBufferCounter -= Time.deltaTime;
            }
        }

        private void HandleJump()
        {
            // Sync with physics velocity first
            _velocity.y = _rb.linearVelocity.y;
            
            // Can jump if: on ground OR coyote time active
            bool canJump = _coyoteTimeCounter > 0f;

            // Execute jump if buffered input exists
            if (_jumpBufferCounter > 0f && canJump)
            {
                _velocity.y = _jumpForce;
                _jumpBufferCounter = 0f;
                _coyoteTimeCounter = 0f;
            }

            // Variable jump height - cut velocity if button released
            if (Input.GetButtonUp("Jump") && _rb.linearVelocity.y > 0f)
            {
                _velocity.y = _rb.linearVelocity.y * _jumpCutMultiplier;
            }

            // Faster falling for better game feel
            if (_rb.linearVelocity.y < 0f)
            {
                _rb.gravityScale = _baseGravityScale * _fallGravityMultiplier;
            }
            else
            {
                _rb.gravityScale = _baseGravityScale;
            }
        }

        private void HandleMovement()
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float targetSpeed = inputX * _moveSpeed;

            // Smooth acceleration/deceleration
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _deceleration;

            _velocity.x = Mathf.MoveTowards(_velocity.x, targetSpeed, accelRate * Time.fixedDeltaTime);
        }

        private void StartDash()
        {
            _isDashing = true;
            _dashTimeCounter = _dashDuration;
            _dashChargesRemaining--;

            // Get dash direction (8-directional)
            Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // If no input, dash in facing direction
            if (inputDir.magnitude < 0.01f)
            {
                inputDir = Vector2.right * Mathf.Sign(transform.localScale.x);
            }

            _velocity = inputDir.normalized * _dashSpeed;

            // Disable gravity during dash
            _rb.gravityScale = 0f;
        }

        private void HandleDash()
        {
            _dashTimeCounter -= Time.fixedDeltaTime;

            if (_dashTimeCounter <= 0f)
            {
                _isDashing = false;
                _rb.gravityScale = _baseGravityScale;
                // Preserve some momentum
                _velocity *= 0.5f;
            }
        }

        private void ApplyVelocity()
        {
            _rb.linearVelocity = _velocity;
        }

        private void UpdateState()
        {
            // Ground check
            Vector2 position = (Vector2)transform.position + _groundCheckOffset;
            _isGrounded = Physics2D.OverlapBox(position, _groundCheckSize, 0f, _groundLayer);

            // Reset dash charges when grounded
            if (_isGrounded)
            {
                _dashChargesRemaining = _maxDashCharges;
            }

            // Wall check
            float direction = Mathf.Sign(transform.localScale.x);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, 0.6f, _groundLayer);
            _isTouchingWall = hit.collider != null;
        }

        private void HandleWall()
        {
            if (_isTouchingWall && !_isGrounded && _velocity.y < 0f)
            {
                // Wall slide
                _velocity.y = Mathf.Max(_velocity.y, -_wallSlideSpeed);
            }
        }

        private void OnDrawGizmosSelected()
        {
            // Visualize ground check in editor
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Vector2 position = (Vector2)transform.position + _groundCheckOffset;
            Gizmos.DrawWireCube(position, _groundCheckSize);
        }
    }
}
