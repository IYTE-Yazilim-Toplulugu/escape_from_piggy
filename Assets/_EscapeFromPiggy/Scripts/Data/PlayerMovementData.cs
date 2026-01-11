using UnityEngine;

namespace EscapeFromPiggy.Data
{
    [CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Escape From Piggy/Player Movement Data")]
    public class PlayerMovementData : ScriptableObject
    {
        [Header("Movement")]
        public float moveSpeed = 5f;
        public float acceleration = 50f;
        public float deceleration = 50f;

        [Header("Jump")]
        public float _jumpForce = 15f;
        public float _jumpCutMultiplier = 0.4f;
        public float _baseGravityScale = 5f;
        public float _fallGravityMultiplier = 2.5f;
        public float _coyoteTime = 0.1f;
        public float _jumpBufferTime = 0.1f;


        [Header("Dash")]
        public float dashSpeed = 15f;
        public float dashDuration = 0.15f;
        public int maxDashCharges = 1;

        [Header("Wall")]
        public float wallSlideSpeed = 2f;
        public float wallJumpForce = 10f;
        public Vector2 wallJumpDirection = new Vector2(1f, 1.5f);

        [Header("Ground Check")]
        public Vector2 groundCheckSize = new Vector2(0.9f, 0.1f);
        public Vector2 groundCheckOffset = new Vector2(0f, -0.5f);
    }
}
