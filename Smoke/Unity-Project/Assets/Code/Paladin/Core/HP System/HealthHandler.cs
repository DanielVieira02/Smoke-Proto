using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Paladin.HealthSystem {

    public class HealthHandler : MonoBehaviour {

        public enum HealthDataMethod { Local, ScriptableObject }

        #region Variables & References

        public HealthDataMethod healthDataMethod = HealthDataMethod.Local;

        [SerializeField] private HealthData healthData = null;
        [SerializeField] private HealthDataScriptableObject healthDataSO = null;

        private HealthData _healthData { get {

                switch (healthDataMethod) {
                    case HealthDataMethod.Local: return healthData;
                    case HealthDataMethod.ScriptableObject: return healthDataSO.healthData;
                    default: return null;
                }

            } 
        }

        [Header("Events")]

        [SerializeField] private UnityEvent OnDeath = new UnityEvent();

        public HealthData HealthData { get { return _healthData; } }

        #endregion


        #region Unity Events

        private void OnEnable() {
            _healthData.Setup();
        }

        #endregion

        #region Public Methods

        public void DamageWasTaken(int damageAmount) {

            if(_healthData != null) {

                if (_healthData.DieAfterDamage(damageAmount))
                    OnDeath?.Invoke();

            }

        }

        #endregion


    }

}