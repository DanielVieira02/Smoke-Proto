using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paladin.Core.CombatSystem.AI.StateMachine {


    public class IdlePlaceholderBossState : State {

        #region Variables & References

        private List<State> states = new List<State>(); 
        private Vector2 initialPosition = Vector2.zero;

        private float magnitude = 10f;

        #endregion

        #region Constructors

        public IdlePlaceholderBossState(EnemyStateMachine stateMachine) {

            this.stateMachine = stateMachine;

            states.Add(new TripleMultiShootPlaceholderBossState(stateMachine));
            states.Add(new TripleShootPlaceholderBossState(stateMachine));
            states.Add(new TacklePlaceholderBossState(stateMachine));

        }

        #endregion


        #region Public Methods

        public override void Start(Enemy enemy) {

            initialPosition = enemy.transform.position;
            OnStateEnter?.Invoke();
            enemy.StartCoroutine(Idle(enemy));

        }

        #endregion

        #region Private Methods

        private IEnumerator Idle(Enemy enemy) {

            float timeCounter = 0f;
            float direction = Random.Range(-1f, 1f) > 0 ? 1f : -1f;

            #region Sinuous move

            while (timeCounter < 3f) {

                timeCounter += Time.fixedDeltaTime;

                Vector2 offset = (Mathf.Sin(timeCounter * 2f * direction) * enemy.transform.up) * magnitude;
                enemy.transform.position = initialPosition + offset;

                yield return new WaitForFixedUpdate();
            }

            #endregion

            #region Return to start point

            while (Vector2.Distance(enemy.transform.position, initialPosition) > 0f) {

                enemy.transform.position = Vector2.Lerp(enemy.transform.position, initialPosition, 15f * Time.fixedDeltaTime);

                if (((Vector2)enemy.transform.position - initialPosition).magnitude < 0.1f) {
                    enemy.transform.position = initialPosition;
                    break;
                }

                yield return new WaitForFixedUpdate();

            }

            #endregion

            yield return new WaitForSeconds(0.5f);

            stateMachine.SetState(GetNextState());
        }

        private State GetNextState() {

            if(states.Count > 0) return states[Random.Range(0, states.Count)];
            return null;

        }

        #endregion

    }

    public class TripleShootPlaceholderBossState : State {

        #region Attack Settings

        int timesToShoot = 7;
        float distanceFromPreviousPoint = 3f;

        #endregion

        private Vector2 initialPosition = Vector2.zero;

        public TripleShootPlaceholderBossState (EnemyStateMachine stateMachine) {

            this.stateMachine = stateMachine;

        }

        public override void Start(Enemy enemy) {

            OnStateEnter?.Invoke();

            initialPosition = enemy.transform.position;
            enemy.StartCoroutine(Attack(enemy));

        }


        private IEnumerator Attack(Enemy enemy) {

            float direction = Random.Range(-1f, 1f) > 0 ? 1f : -1f;

            for (int index = 0; index < timesToShoot; index++) {

                enemy.RaisePatternAttack("Triple Shoot");
                Vector3 nextPoint = enemy.transform.position + (enemy.transform.up * distanceFromPreviousPoint * direction);

                while (Vector2.Distance(enemy.transform.position, nextPoint) > 0f) {
                    
                    enemy.transform.position = Vector2.Lerp(enemy.transform.position, nextPoint, 15f * Time.deltaTime);

                    if ((enemy.transform.position - nextPoint).magnitude < 0.1f) {
                        enemy.transform.position = nextPoint;
                        break;
                    }
                    
                    yield return new WaitForFixedUpdate();
                }

            }

            while (Vector2.Distance(enemy.transform.position, initialPosition) > 0f) {

                enemy.transform.position = Vector2.Lerp(enemy.transform.position, initialPosition, 2f * Time.fixedDeltaTime);

                if (((Vector2)enemy.transform.position - initialPosition).magnitude < 0.1f) {
                    enemy.transform.position = initialPosition;
                    break;
                }

                yield return new WaitForFixedUpdate();

            }

            stateMachine.SetState(new IdlePlaceholderBossState(stateMachine));

        }
    }

    public class TripleMultiShootPlaceholderBossState : State {

        #region Attacks Properties

        float attackDuration = 3f;
        float intervalBetweenShoots = 0.15f;

        float magnitude = 7.5f;

        #endregion

        private bool attackFinished = false;
        private Vector2 initialPosition;

        public TripleMultiShootPlaceholderBossState(EnemyStateMachine stateMachine) {

            this.stateMachine = stateMachine;

        }

        public override void Start(Enemy enemy) {
            OnStateEnter?.Invoke();
            initialPosition = enemy.transform.position;
            enemy.StartCoroutine(Move(enemy));

        }


        private IEnumerator Attack(Enemy enemy) {

            attackFinished = false;

            for (int index = 0; index < (attackDuration / intervalBetweenShoots); index++) {
                enemy.RaisePatternAttack("Triple Multi Shoot");
                yield return new WaitForSeconds(intervalBetweenShoots);
            }

            attackFinished = true;

        }

        private IEnumerator Move(Enemy enemy) {

            float timeCounter = 0f;
            float direction = Random.Range(-1f, 1f) > 0 ? 1f : -1f;
            
            enemy.StartCoroutine(Attack(enemy));

            while (!attackFinished) {

                timeCounter += Time.fixedDeltaTime;

                Vector2 offset = (Mathf.Sin(timeCounter * 2f * direction) * enemy.transform.up) * magnitude;

                enemy.transform.position = initialPosition + offset;

                yield return new WaitForFixedUpdate();
            }

            while (Vector2.Distance(enemy.transform.position, initialPosition) > 0f) {

                enemy.transform.position = Vector2.Lerp(enemy.transform.position, initialPosition, 15f * Time.fixedDeltaTime);

                if (((Vector2)enemy.transform.position - initialPosition).magnitude < 0.1f) {
                    enemy.transform.position = initialPosition;
                    break;
                }

                yield return new WaitForFixedUpdate();

            }

            stateMachine.SetState(new IdlePlaceholderBossState(stateMachine));

        }
    }

    public class TacklePlaceholderBossState : State {

        Vector2 initialPosition = Vector2.zero;
        float radius = 0f;
        public TacklePlaceholderBossState(EnemyStateMachine stateMachine) {
            this.stateMachine = stateMachine;
        }

        public override void Start(Enemy enemy) {

            initialPosition = enemy.transform.position;
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null) radius = Vector2.Distance(initialPosition, player.transform.position) / 2f;

            enemy.StartCoroutine(Tackle(enemy));

        }

        private IEnumerator Tackle(Enemy enemy) {

            float y = 0f;
            Vector2 center = new Vector2(initialPosition.x - radius, initialPosition.y);

            while(y < 360f) {

                y += Time.fixedDeltaTime * 120f;
                Vector2 offset = new Vector2(radius * Mathf.Cos(y * Mathf.Deg2Rad), radius * Mathf.Sin(y * Mathf.Deg2Rad));
                enemy.transform.position = center + offset;
                yield return new WaitForFixedUpdate();
            }

            enemy.transform.position = initialPosition;
            stateMachine.SetState(new IdlePlaceholderBossState(stateMachine));

        }

    }

}