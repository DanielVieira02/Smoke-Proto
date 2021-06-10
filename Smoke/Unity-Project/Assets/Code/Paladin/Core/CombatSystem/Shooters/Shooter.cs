using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paladin.Core.CombatSystem.Shooter {

    public class Shooter : MonoBehaviour {

        #region Variables & References

        /// <summary>
        /// The origin point of the shoot
        /// </summary>
        [SerializeField] private Transform m_shootPoint;

        /// <summary>
        /// The prefab of the projectile
        /// </summary>
        [SerializeField] private Projectile m_projectilePrefab;

        #endregion


        #region Gizmos

        private void OnDrawGizmos() {
            
            if(m_shootPoint != null) {

                Gizmos.color = Color.white;
                Gizmos.DrawLine(m_shootPoint.position, m_shootPoint.position + m_shootPoint.right);

            }

        }

        #endregion

        #region Public Methods

        public void Shoot() {

            if(m_shootPoint != null && m_shootPoint != null) BulletSpawner.Current.InstantiateProjectile(m_shootPoint, m_projectilePrefab);
        
        }

        #endregion

    }

}