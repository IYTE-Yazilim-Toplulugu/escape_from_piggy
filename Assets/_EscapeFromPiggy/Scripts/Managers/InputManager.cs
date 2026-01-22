// Create: Assets/_EscapeFromPiggy/Scripts/Managers/InputManager.cs
using UnityEngine;
using UnityEngine.InputSystem;

namespace EscapeFromPiggy.Managers
{
    /// <summary>
    /// Singleton that manages game input and provides easy access to input values
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        private PlayerInputActions _inputActions;

        // Public accessors for easy access
        public Vector2 MoveInput { get; private set; }
        public bool JumpPressed { get; private set; }
        public bool JumpHeld { get; private set; }
        public bool DashPressed { get; private set; }
        public bool InteractPressed { get; private set; }

        public bool GrabHeld { get; private set; }

        private void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject);

            // Initialize input actions
            _inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            // Subscribe to input events
            _inputActions.Player.Jump.started += OnJumpStarted;
            _inputActions.Player.Jump.canceled += OnJumpCanceled;
            _inputActions.Player.Grab.started += OnGrabStarted;
            _inputActions.Player.Grab.canceled += OnGrabCanceled;
            _inputActions.Player.Dash.started += OnDashStarted;
            _inputActions.Player.Interact.started += OnInteractStarted;
        }

        private void OnDisable()
        {
            // Unsubscribe to prevent memory leaks
            _inputActions.Player.Jump.started -= OnJumpStarted;
            _inputActions.Player.Jump.canceled -= OnJumpCanceled;
            _inputActions.Player.Grab.started -= OnGrabStarted;
            _inputActions.Player.Grab.canceled -= OnGrabCanceled;
            _inputActions.Player.Dash.started -= OnDashStarted;
            _inputActions.Player.Interact.started -= OnInteractStarted;

            _inputActions.Disable();
        }

        private void Update()
        {
            // Read continuous inputs
            MoveInput = _inputActions.Player.Move.ReadValue<Vector2>();
        }

        private void LateUpdate()
        {
            // Reset one-frame inputs at the end of the frame.
            // We use LateUpdate to ensure PlayerController has enough time
            // to read the input in its Update method before we reset it.
            if (JumpPressed) JumpPressed = false;
            if (DashPressed) DashPressed = false;
            if (InteractPressed) InteractPressed = false;
        }

        private void OnJumpStarted(InputAction.CallbackContext context)
        {
            JumpPressed = true;
            JumpHeld = true;
        }

        private void OnJumpCanceled(InputAction.CallbackContext context)
        {
            JumpHeld = false;
        }

        private void OnDashStarted(InputAction.CallbackContext context)
        {
            DashPressed = true;
        }

        private void OnInteractStarted(InputAction.CallbackContext context)
        {
            InteractPressed = true;
        }

        private void OnGrabStarted(InputAction.CallbackContext context)
        {
            GrabHeld = true;
        }

        private void OnGrabCanceled(InputAction.CallbackContext context)
        {
            GrabHeld = false;
        }

        // Utility method to enable/disable input (useful for pausing, cutscenes)
        public void SetPlayerInputEnabled(bool enabled)
        {
            if (enabled)
                _inputActions.Player.Enable();
            else
                _inputActions.Player.Disable();
        }
    }
}
