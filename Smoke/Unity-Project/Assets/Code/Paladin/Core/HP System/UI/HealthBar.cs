using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Paladin.HealthSystem.UI {

    public class HealthBar : MonoBehaviour {

        public enum HealthDataMethod {
            HealthHandler,
            ScriptableObject
        }

        #region Variables & References

        public HealthDataMethod healthDataMethod = HealthDataMethod.HealthHandler;

        [SerializeField] private HealthHandler healthHandler = null;
        [SerializeField] private HealthDataScriptableObject healthDataSO = null;
        [SerializeField] private Slider healthBarSlider = null;

        private HealthData healthData { get {

                switch (healthDataMethod) {
                    case HealthDataMethod.HealthHandler: return healthHandler.HealthData;
                    case HealthDataMethod.ScriptableObject: return healthDataSO.healthData;
                }

                return null; 
            } 
        }

        #endregion


        #region Unity Events

        private void OnEnable() {
            
            if(healthData != null) {

                healthData.OnHealthChanged += UpdateHealthBar;

            }

        }

        private void OnDisable() {

            if (healthData != null)
            {

                healthData.OnHealthChanged -= UpdateHealthBar;

            }

        }

        #endregion

        #region Public Methods

        public void UpdateHealthBar() {

            if (healthData == null) return;
            if (healthBarSlider == null) return;

            healthBarSlider.value = healthData.HealthPercentage;

        }

        #endregion

    }

}