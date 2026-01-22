using UnityEngine;
using EscapeFromPiggy.Managers;

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
        [SerializeField] private Transform _wallCheck;
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private float _wallCheckRadius = 0.2f;

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
        private bool _isWallGrabbing;
        private bool _isWallSliding;
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
                HandleWall();
                HandleJump();
                HandleMovement();
            }

            ApplyVelocity();
        }

        private void HandleInput()
        {
            // Jump (with Buffer system)
            if (InputManager.Instance.JumpPressed)
            {
                _jumpBufferCounter = _jumpBufferTime;
            }
            else
            {
                _jumpBufferCounter -= Time.deltaTime;
            }

            // Dash input
            if (InputManager.Instance.DashPressed && _dashChargesRemaining > 0 && !_isDashing)
            {
                StartDash();
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
            // Don't process jump/gravity if holding the wall
            if (_isWallGrabbing) return;

            // Sync with physics velocity only if not sliding
            if (!_isWallSliding)
            {
                _velocity.y = _rb.linearVelocity.y;
            }

            // Jump execution
            bool canJump = _coyoteTimeCounter > 0f;

            if (_jumpBufferCounter > 0f && canJump)
            {
                _velocity.y = _jumpForce;
                _jumpBufferCounter = 0f;
                _coyoteTimeCounter = 0f;
            }

            // Variable jump height
            if (!InputManager.Instance.JumpHeld && _velocity.y > 0f)
            {
                _velocity.y *= _jumpCutMultiplier;
            }

            // Gravity Control (Skip if sliding to avoid conflict)
            if (!_isWallSliding)
            {
                if (_rb.linearVelocity.y < 0f)
                {
                    _rb.gravityScale = _baseGravityScale * _fallGravityMultiplier;
                }
                else
                {
                    _rb.gravityScale = _baseGravityScale;
                }
            }
        }

        private void HandleMovement()
        {
            // Reading directly from InputManager
            float inputX = InputManager.Instance.MoveInput.x;


            // If facing right
            if (inputX > 0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f); // Sağa bak
            }
            // If facing left
            else if (inputX < -0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f); // Sola bak
            }
            // ------------------------------------------

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
            Vector2 inputDir = InputManager.Instance.MoveInput;

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
            if (_isGrounded && !_isDashing)
            {
                _dashChargesRemaining = _maxDashCharges;
            }

            // Wall check
            _isTouchingWall = Physics2D.OverlapCircle(_wallCheck.position, _wallCheckRadius, _wallLayer);
        }

        private void HandleWall()
        {
            // Reset wall states
            _isWallGrabbing = false;
            _isWallSliding = false;

            // Check wall interaction only when airborne
            if (_isTouchingWall && !_isGrounded)
            {
                // Wall unstick logic (cancel if moving away from wall)
                float inputX = InputManager.Instance.MoveInput.x;
                float facingDirection = Mathf.Sign(transform.localScale.x);

                if (Mathf.Abs(inputX) > 0.1f && Mathf.Sign(inputX) != facingDirection)
                {
                    return;
                }

                // Wall grab
                if (InputManager.Instance.GrabHeld)
                {
                    _isWallGrabbing = true;
                    _rb.gravityScale = 0f;
                    _velocity = Vector2.zero;
                }
                // Wall slide
                else if (_rb.linearVelocity.y < 0f)
                {
                    _isWallSliding = true;

                    // Clamp slide speed
                    if (_velocity.y < -_wallSlideSpeed)
                    {
                        _velocity.y = -_wallSlideSpeed;
                    }
                }
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
