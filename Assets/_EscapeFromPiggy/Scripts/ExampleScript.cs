using UnityEngine;

namespace EscapeFromPiggy
{
    /// <summary>
    /// Example script demonstrating coding conventions for Escape from Piggy project.
    /// Delete this file when you start development.
    /// </summary>
    public class ExampleScript : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 10f;

        [Header("References")]
        [SerializeField] private Rigidbody2D rb;

        #endregion

        #region Private Fields

        private bool _isGrounded;
        private float _horizontalInput;

        #endregion

        #region Constants

        private const float GRAVITY_SCALE = 2f;
        private const string GROUND_TAG = "Ground";

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            // Initialize components
            if (rb == null)
            {
                rb = GetComponent<Rigidbody2D>();
            }
        }

        private void Start()
        {
            // Setup after Awake
            InitializePlayer();
        }

        private void Update()
        {
            // Handle input
            HandleInput();
        }

        private void FixedUpdate()
        {
            // Handle physics
            HandleMovement();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Makes the character jump if grounded.
        /// </summary>
        public void Jump()
        {
            if (_isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                _isGrounded = false;
            }
        }

        #endregion

        #region Private Methods

        private void InitializePlayer()
        {
            // Initialization logic
            rb.gravityScale = GRAVITY_SCALE;
        }

        private void HandleInput()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        private void HandleMovement()
        {
            // Apply horizontal movement
            rb.linearVelocity = new Vector2(_horizontalInput * moveSpeed, rb.linearVelocity.y);
        }

        #endregion

        #region Collision Detection

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GROUND_TAG))
            {
                _isGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GROUND_TAG))
            {
                _isGrounded = false;
            }
        }

        #endregion
    }
}
