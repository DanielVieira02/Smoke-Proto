using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Paladin.Core.Player {

    public class PlayerController : MonoBehaviour {

        [System.Serializable] public class MovementEvent : UnityEvent<Vector2> { }
        [System.Serializable] public class InputActionEvent : UnityEvent<InputAction.CallbackContext> { }

        #region Variables & References

        [SerializeField] private InputReader playerInputReader = null;
        [SerializeField] private MovementBehaviour movementBehaviour = null;

        [Header("Input Events")]

        [SerializeField] private InputActionEvent OnAction = new InputActionEvent();
        [SerializeField] private MovementEvent OnMove = new MovementEvent();

        #endregion


        #region Unity Events

        private void Start() {
            if(playerInputReader != null) {
                playerInputReader.OnMoveAction += Move;
                playerInputReader.OnAttackAction += Action;
            }
        }

        private void OnDisable() {            
            if (playerInputReader != null) {
                playerInputReader.OnMoveAction -= Move;
                playerInputReader.OnAttackAction -= Action;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Function used to move the Player. This function also call the OnMove event
        /// </summary>
        /// <param name="direction">The direction that the player will move</param>
        public void Move(Vector2 direction) {
            OnMove?.Invoke(direction);
            movementBehaviour.Move(direction);
        }

        public void Action(InputAction.CallbackContext action) {
            OnAction?.Invoke(action);
        }

        #endregion


    }


}