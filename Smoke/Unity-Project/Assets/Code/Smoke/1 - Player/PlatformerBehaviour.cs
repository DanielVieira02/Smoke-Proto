using Paladin.Core.Player;
using UnityEngine;

public class PlatformerBehaviour : MovementBehaviour {

    [SerializeField] private float movementSpeed = 3f;

    private Rigidbody2D m_rigidbody2d = null;
    private Vector2 m_movementVector = Vector2.zero;

    #region Unity Events

    private void Start()
    {
        m_rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (m_rigidbody2d != null)
        {
            m_rigidbody2d.velocity = m_movementVector * movementSpeed + new Vector2(0f, m_rigidbody2d.velocity.y);
        }
    }

    #endregion

    #region Public Methods

    public override void Move(Vector2 movementVector)
    {
        m_movementVector = movementVector;
    }

    #endregion

}
