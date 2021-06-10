using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Paladin.HealthSystem {

    [CreateAssetMenu(menuName = "HP System/HP Data")]
    public class HealthDataScriptableObject : ScriptableObject {

        #region Variables & References

        public HealthData healthData;

        #endregion


        #region Unity Events

        private void OnEnable() {

            if(healthData != null)
                healthData.Setup();

        }

        #endregion



    }

    [System.Serializable]
    public class HealthData {

        #region Variables & References

        #region Public Flags

        public float HealthPercentage { get { return (float)_currentHealth / (float)_maxHealth; } }
        public bool IsAlive { get { return (_currentHealth > 0); } }

        #endregion

        #region Events

        public UnityAction OnHealthChanged = delegate { };

        #endregion

        [SerializeField] private int _maxHealth = 3;
        [SerializeField] private int _initialHealth = 3;

        private int _currentHealth = 0;

        [SerializeField] private bool setupOnEnable = true;

        #endregion


        #region Public Methods

        public void Setup()
        {

            if (setupOnEnable)
            {
                _currentHealth = _initialHealth;
                OnHealthChanged?.Invoke();
            }

        }


        public void IncreaseHealth(int amount)
        {

            if (amount <= 0) return;

            _currentHealth += amount;
            if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;

            OnHealthChanged?.Invoke();

        }

        public void DecreaseHealth(int amount)
        {

            if (amount <= 0) return;

            _currentHealth -= amount;
            if (_currentHealth < 0) _currentHealth = 0;

            OnHealthChanged?.Invoke();

        }

        /// <summary>
        /// Deal damage and returns if the Health is equal to zero after the operation
        /// </summary>
        /// <param name="amount">The amount of damage taken</param>
        /// <returns></returns>
        public bool DieAfterDamage(int amount)
        {

            DecreaseHealth(amount);
            return (_currentHealth == 0);

        }

        #endregion

    }

}