using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Paladin.Core.CombatSystem.AI.StateMachine {

    public class EnemyStateMachine : MonoBehaviour, IAIRoutine {


        protected State currentState = null;
        protected Enemy enemy = null;

        protected bool isActive = false;

        public virtual void StartIA(Enemy enemy) {

            if (enemy == null) return;

            isActive = true;
            this.enemy = enemy;

        }


        public void SetState(State state) {

            currentState = state;
            if (currentState != null && isActive) currentState.Start(enemy);

        }

        public void StopStateMachine() {

            SetState(null);
            isActive = false;
            StopAllCoroutines();

            enemy.StopAllCoroutines();

        }


    }

    public abstract class State {

        protected EnemyStateMachine stateMachine = null;

        public UnityAction OnStateEnter = delegate { };
        public UnityAction OnStateExit = delegate { };

        public abstract void Start(Enemy enemy);

    }

}