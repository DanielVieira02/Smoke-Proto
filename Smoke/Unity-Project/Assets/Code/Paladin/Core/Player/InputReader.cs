using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Paladin.Core.Player {

    [CreateAssetMenu(menuName = "Input/Input Reader")]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions {


        #region Variables & References

        #region Events

        [HideInInspector] public UnityAction<InputAction.CallbackContext> OnAttackAction = delegate { };
        [HideInInspector] public UnityAction<Vector2> OnMoveAction = delegate { };

        #endregion


        private GameInput gameActions;

        #endregion


        #region Unity Events

        private void OnEnable() {
            
            if(gameActions == null) {

                gameActions = new GameInput();
                gameActions.Gameplay.SetCallbacks(this);

            }

            EnableGameplayInput();

        }

        private void OnDisable() {

            DisableInput();
            
        }

        #endregion


        #region Input Action Events

        public void OnAttack(InputAction.CallbackContext context) {

            if (OnAttackAction != null) OnAttackAction.Invoke(context);

        }

        public void OnMove(InputAction.CallbackContext context) {

            if (OnMoveAction != null) OnMoveAction.Invoke(context.ReadValue<Vector2>());

        }

        #endregion

        #region Private Methods

        private void DisableInput() {

            gameActions.Disable();

        }

        private void EnableGameplayInput() {

            gameActions.Enable();

        }

        #endregion

    }

}