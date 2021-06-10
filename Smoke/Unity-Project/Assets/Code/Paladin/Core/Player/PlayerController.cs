using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Paladin.Player {

    public class PlayerController : MonoBehaviour {

        [System.Serializable] public class MovementEvent : UnityEvent<Vector2> { }


        #region Variables & References

        [SerializeField] private InputReader playerInputReader = null;
        [SerializeField] private float movementSpeed = 3f;
        [SerializeField] private GameObject playerBody = null;

        [Header("Input Events")]

        [SerializeField] private UnityEvent OnAttack = new UnityEvent();
        [SerializeField] private MovementEvent OnMove = new MovementEvent();

        private Rigidbody2D _rigidbody2D = null;

        private Vector2 _movementVector = Vector2.zero;

        #endregion


        #region Unity Events

        private void Start() {

            if(playerInputReader != null) {

                playerInputReader.OnMoveAction += Move;
                playerInputReader.OnAttackAction += Attack;
            }

            _rigidbody2D = GetComponent<Rigidbody2D>();

        }

        private void OnDisable() {
            
            if (playerInputReader != null) {

                playerInputReader.OnMoveAction -= Move;
                playerInputReader.OnAttackAction -= Attack;

            }

        }

        private void FixedUpdate() {

            if(_rigidbody2D != null)
                _rigidbody2D.velocity = _movementVector * movementSpeed;

        }

        #endregion

        #region Public Methods


        /// <summary>
        /// Function used to move the Player. This function also call the OnMove event
        /// </summary>
        /// <param name="direction">The direction that the player will move</param>
        public void Move(Vector2 direction) {

            _movementVector = direction;

            if (direction.magnitude > 0f)
                OnMove?.Invoke(direction);

        }

        public void Attack() {

            OnAttack?.Invoke();

        }

        public void LookAtDirection(Vector2 direction) {

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            playerBody.transform.eulerAngles = new Vector3(0f, 0f, angle);

        }

        #endregion


    }


}