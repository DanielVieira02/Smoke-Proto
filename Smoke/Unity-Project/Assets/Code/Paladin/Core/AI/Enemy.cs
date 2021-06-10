using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Paladin.Core.CombatSystem.AI {

    public interface IAIRoutine {

        void StartIA(Enemy enemy);

    }


    public class Enemy : MonoBehaviour {

        #region AttackPattern Class

        [System.Serializable]
        public class AttackPattern
        {

            public string tag = "";
            public UnityEvent OnRaisePattern = new UnityEvent();

        }

        #endregion


        #region Variables & References

        public Object AIHandler = null;
        private IAIRoutine AIRoutine = null;

        [SerializeField] private List<AttackPattern> attackPatterns = new List<AttackPattern>();
        private Dictionary<string, UnityEvent> m_attackPatterns = new Dictionary<string, UnityEvent>();
        
        #endregion


        #region Unity Events

        private void Start()
        {

            #region IA Setup

            if ((AIHandler as IAIRoutine) != null) AIRoutine = (AIHandler as IAIRoutine);
            if (AIRoutine != null) AIRoutine.StartIA(this);

            #endregion

            #region Attack Patterns Setup

            foreach (AttackPattern attack in attackPatterns)
            {

                m_attackPatterns.Add(attack.tag, attack.OnRaisePattern);

            }

            #endregion

        }

        #endregion

        #region Public Methods

        public void RaisePatternAttack(string attackTag)
        {

            if (m_attackPatterns.ContainsKey(attackTag))
            {
                m_attackPatterns[attackTag]?.Invoke();
            }
            else
            {
                Debug.LogWarning(name + " does not contains a pattern attack called " + attackTag);
            }

        }

        #endregion

    }

}