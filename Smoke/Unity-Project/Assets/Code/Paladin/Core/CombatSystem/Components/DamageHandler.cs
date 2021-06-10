using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Paladin.Core.CombatSystem {

    public class DamageHandler : MonoBehaviour, IDamageable {

        #region Variables & References

        [System.Serializable]
        public class DamageEvent : UnityEvent<int> { }
        
        [Header("Events")]

        [SerializeField] private DamageEvent OnTakeDamage = new DamageEvent();

        #endregion


        #region Interface Methods

        public void TakeDamage() {
            TakeDamage(1);
        }

        public void TakeDamage(int damageAmount) {
            OnTakeDamage?.Invoke(1);
        }

        #endregion

    }


    public interface IDamageable {

        void TakeDamage();
        void TakeDamage(int damageAmount);

    }

}