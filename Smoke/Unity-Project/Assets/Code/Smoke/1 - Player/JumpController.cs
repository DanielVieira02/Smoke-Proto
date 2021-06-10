using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Smoke.Player
{
    public class JumpController : MonoBehaviour
    {
        #region Variables & References

        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float distanceFromGround = 0.5f;
        [SerializeField] private float groundCheckTimeInterval = 0.1f;
        [SerializeField] private float maxTimeJumping = 0.5f;

        [SerializeField] private LayerMask groundLayer;

        [SerializeField] private Transform groundCheck = null;

        private Rigidbody2D m_rigidbody2D = null;
        private float jumpCounter = 0f;
        private bool isGrounded = false;
        private bool isJumping = false;
        #endregion

        #region Unity Events

        private void Awake()
        {
            m_rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (isJumping && !isGrounded)
            {
                m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, jumpForce);
                jumpCounter += Time.deltaTime;
                if (jumpCounter >= maxTimeJumping)
                    isJumping = false;
            } 
        }

        private void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(groundCheck.position, groundCheck.position + (Vector3.down * distanceFromGround));
            }
        }

        private void OnEnable()
        {
            StartCoroutine(CheckGround(groundCheckTimeInterval));
        }

        #endregion

        #region Private Methods
        private IEnumerator CheckGround(float timeInterval)
        {
            while (enabled)
            {
                var hit = Physics2D.LinecastAll(groundCheck.position, groundCheck.position + (Vector3.down * distanceFromGround), groundLayer);
                isGrounded = hit.Length > 0;

                yield return new WaitForSeconds(timeInterval);
            }
        }

        #endregion

        #region Public Methods

        public void Jump(InputAction.CallbackContext action)
        {
            switch (action.phase)
            {
                case InputActionPhase.Performed:
                    if (isGrounded)
                    {
                        m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, jumpForce);
                        isJumping = true;
                        jumpCounter = 0f;
                    }
                    break;
                case InputActionPhase.Canceled:
                    isJumping = false;
                    break;
            }
        }

        #endregion

    }
}