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
        [SerializeField] private int _maxJumps = 2;
        [SerializeField] private float _jumpCutMultiplier = 0.4f;
        [SerializeField] private float _baseGravityScale = 5f;
        [SerializeField] private float _fallGravityMultiplier = 2.5f;
        [SerializeField] private float _coyoteTime = 0.1f;
        [SerializeField] private float _jumpBufferTime = 0.1f;

        [Header("Dash")]
        [SerializeField] private float _dashSpeed = 15f;
        [SerializeField] private float _dashDuration = 0.15f;
        [SerializeField] private int _maxDashCharges = 2;

        [Header("Wall")]
        [SerializeField] private float _wallSlideSpeed = 2f;
        [SerializeField] private float _fastWallSlideSpeed = 10f;
        [SerializeField] private float _wallClimbSpeed = 5f;
        [SerializeField] private float _wallJumpForce = 15f;
        [SerializeField] private Vector2 _wallJumpAngle = new Vector2(1f, 1.5f);
        [SerializeField] private float _wallJumpLockDuration = 0.2f;
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
        private int _jumpChargesRemaining;
        private bool _isDashing;
        private bool _isWallJumpLocking;

        // Timers
        private float _coyoteTimeCounter;
        private float _jumpBufferCounter;
        private float _dashTimeCounter;
        private float _wallJumpLockCounter;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            _dashChargesRemaining = _maxDashCharges;
            _jumpChargesRemaining = _maxJumps;
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

            // Wall jump lock countdown
            if (_wallJumpLockCounter > 0f)
            {
                _wallJumpLockCounter -= Time.fixedDeltaTime;
                if (_wallJumpLockCounter <= 0f) _isWallJumpLocking = false;
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
            bool canJump = _coyoteTimeCounter > 0f || _jumpChargesRemaining > 0;
            bool isWallJump = _isTouchingWall && !_isGrounded;

            if (_jumpBufferCounter > 0f)
            {
                if (isWallJump)
                {
                    PerformWallJump();
                }
                else if (canJump)
                {
                    PerformJump();
                }
            }

            // Variable jump height
            if (!InputManager.Instance.JumpHeld && _velocity.y > 0f && !_isWallJumpLocking)
            {
                _velocity.y *= _jumpCutMultiplier;
            }

            // Gravity Control (Skip if sliding to avoid conflict)
            if (!_isWallSliding && !_isWallGrabbing && !_isDashing && !_isWallJumpLocking)
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

        private void PerformJump()
        {
            _velocity.y = _jumpForce;
            _jumpBufferCounter = 0f;
            _coyoteTimeCounter = 0f;
            _jumpChargesRemaining--;
        }

        private void PerformWallJump()
        {
            float facingDirection = Mathf.Sign(transform.localScale.x);
            Vector2 forceDir = new Vector2(-facingDirection * _wallJumpAngle.x, _wallJumpAngle.y).normalized;

            _velocity = forceDir * _wallJumpForce;

            // Immediate visual flip
            transform.localScale = new Vector3(-facingDirection, 1f, 1f);

            _isWallJumpLocking = true;
            _wallJumpLockCounter = _wallJumpLockDuration;
            _jumpBufferCounter = 0f;
        }

        private void HandleMovement()
        {
            // Prevent movement input during wall jump lock
            if (_isWallJumpLocking) return;

            // Reading directly from InputManager
            float inputX = InputManager.Instance.MoveInput.x;

            // If facing right
            if (inputX > 0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            // If facing left
            else if (inputX < -0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

            float targetSpeed = inputX * _moveSpeed;

            // Smooth acceleration/deceleration
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _deceleration;

            // Reduce air control
            if (!_isGrounded) accelRate *= 0.8f;

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

            // Reset charges when grounded
            // Check vertical velocity to prevent resetting immediately after jumping
            if (_isGrounded && !_isDashing && _rb.linearVelocity.y <= 0.1f)
            {
                _dashChargesRemaining = _maxDashCharges;
                _jumpChargesRemaining = _maxJumps;
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
                float inputY = InputManager.Instance.MoveInput.y;

                // Wall grab/climb
                if (InputManager.Instance.GrabHeld)
                {
                    _isWallGrabbing = true;
                    _rb.gravityScale = 0f;
                    _velocity.y = inputY * _wallClimbSpeed;
                    _velocity.x = 0f;
                }
                // Wall slide
                else if (_rb.linearVelocity.y < 0f || _isWallSliding)
                {
                    _isWallSliding = true;
                    _rb.gravityScale = 0f;

                    // Fast slide check
                    float currentSlideSpeed = (inputY < -0.1f) ? _fastWallSlideSpeed : _wallSlideSpeed;

                    // Smoothly interpolate to slide speed
                    _velocity.y = Mathf.MoveTowards(_velocity.y, -currentSlideSpeed, 50f * Time.fixedDeltaTime);
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
