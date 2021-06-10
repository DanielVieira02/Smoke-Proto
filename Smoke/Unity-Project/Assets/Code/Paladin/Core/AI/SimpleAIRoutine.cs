using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paladin.Core.CombatSystem.AI {

    [CreateAssetMenu(menuName = "AI Routine/Simple")]
    public class SimpleAIRoutine : ScriptableObject, IAIRoutine {

        #region Variables & References

        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private float rotateSpeed = 10f;
        [SerializeField] private string attackTag = "";

        [SerializeField] private float attackAtEachSeconds = 1.5f;
        [SerializeField] private bool hasAnAttack = true;

        #endregion


        #region Interface Methods

        public void StartIA(Enemy enemy) {

            if (enemy == null) return;
            enemy.StartCoroutine(Routine(enemy));

        }

        #endregion

        #region Private Methods

        private IEnumerator Routine(Enemy enemy) {

            float timeAttackCounter = 0f;

            while (true) {

                #region Movement Logic

                enemy.transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
                enemy.transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);

                #endregion

                #region Attack Logic

                if (hasAnAttack)
                {

                    timeAttackCounter += Time.fixedDeltaTime;
                    if (timeAttackCounter > attackAtEachSeconds)
                    {
                        timeAttackCounter = 0f;
                        enemy.RaisePatternAttack(attackTag);
                    }
                
                }

                #endregion

                yield return new WaitForFixedUpdate();

            }
        }

        #endregion

    }

}