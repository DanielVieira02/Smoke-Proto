using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paladin.Physics
{

    public class SinuousMovement : MonoBehaviour {

        protected enum Axis {
            Horizontal,
            Vertical
        }

        #region Variables & References

        /// <summary>
        /// The behaviour must execute the movement?
        /// </summary>
        [SerializeField] private bool m_movementEnabled = true;

        /// <summary>
        /// The behaviour must start the movement in the Awake event?
        /// </summary>
        [SerializeField] private bool m_startOnAwake = true;

        /// <summary>
        /// The target transform will move in the Y axis along with the Time variable?
        /// </summary>
        [SerializeField] private bool m_followTime = true;

        [Space(10)]

        /// <summary>
        /// The amplitude of the movement
        /// </summary>
        [SerializeField] private float m_amplitude = 1f;

        /// <summary>
        /// The frequency of the movement
        /// </summary>
        [SerializeField] private float m_velocity = 1f;

        [Space(10)]

        /// <summary>
        /// The transform that will move
        /// </summary>
        [SerializeField] private Transform m_movementTarget = null;
        public Transform movementTarget { get { return m_movementTarget; } set { m_movementTarget = value; } }

        /// <summary>
        /// The axis of the movement
        /// </summary>
        [SerializeField] private Axis m_axis;

        /// <summary>
        /// The initial position of the object that will move
        /// </summary>
        private Vector2 m_initialPosition = Vector2.zero;

        /// <summary>
        /// The current "T" variable of the sinuous movement
        /// </summary>
        private float m_currentTime = 0f;


        #endregion


        #region Unity Events

        private void Awake() {

            if (m_startOnAwake) StartMovement();

        }

        private void FixedUpdate() {

            if (!m_movementEnabled) return;

            if (m_movementTarget == null) {
                Debug.LogError("No transform target was assigned!");
                return;
            }

            m_currentTime += Time.deltaTime * m_velocity;

            switch (m_axis) {

                case Axis.Horizontal:
                    if (m_followTime)                    
                        m_movementTarget.position = m_initialPosition + new Vector2(Mathf.Cos(m_currentTime) * m_amplitude, m_currentTime);                    
                    else
                        m_movementTarget.position = new Vector2(Mathf.Cos(m_currentTime) * m_amplitude + m_initialPosition.x, m_movementTarget.position.y);                    
                    break;

                case Axis.Vertical:
                    if (m_followTime)
                        m_movementTarget.position = m_initialPosition + new Vector2(m_currentTime, Mathf.Sin(m_currentTime) * m_amplitude);
                    else
                        m_movementTarget.position = new Vector2(m_movementTarget.position.x, Mathf.Sin(m_currentTime) * m_amplitude + m_initialPosition.y);
                    break;

            }

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the time of executation from the Sinuous movement
        /// </summary>
        /// <param name="time">The new time value</param>
        public void SetSinusoidTime(float time) {

            m_currentTime = time;

        }

        /// <summary>
        /// Makes the behaviour start the movement based on the current point of the target
        /// </summary>
        /// <param name="resetTime">If it is true, the time will start in 0</param>
        public void StartMovement(bool resetTime = true) {

            if (resetTime) m_currentTime = 0f;

            m_movementEnabled = true;

            if(m_movementTarget == null) {
                Debug.LogError("No movement Transform was assigned!");
                return;
            }
            m_initialPosition = m_movementTarget.position;

        }

        /// <summary>
        /// Forces the behaviour to stop the movement
        /// </summary>
        public void StopMovement() {
            m_movementEnabled = false;
        }

        #endregion


    }

}