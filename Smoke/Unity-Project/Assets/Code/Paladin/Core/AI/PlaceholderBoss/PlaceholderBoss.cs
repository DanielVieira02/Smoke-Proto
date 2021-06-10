using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paladin.Core.CombatSystem.AI.StateMachine
{

    public class PlaceholderBoss : EnemyStateMachine, IAIRoutine {
        
        new public void StartIA(Enemy enemy) {

            if (enemy == null) return;

            this.enemy = enemy;
            isActive = true;

            currentState = new IdlePlaceholderBossState(this);
            currentState.Start(this.enemy);

        }

    }

}