using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Paladin.Core.CombatSystem.Shooter {

    [CreateAssetMenu()]
    public class GlobalProjectileSettings : ScriptableObject {

        #region Variables & References

        public LayerMask PlayerProjectileHittableLayer { get { return m_playerProjectileHittableLayers; } }
        [Tooltip("Which layers the player projectiles can hit?")]
        [SerializeField] private LayerMask m_playerProjectileHittableLayers = new LayerMask();


        public LayerMask EnemyProjectileHittableLayer { get { return m_enemyProjectileHittableLayers; } }
        [Tooltip("Which layers the enemies projectiles can hit?")]
        [SerializeField] private LayerMask m_enemyProjectileHittableLayers = new LayerMask();

        #endregion


        #region Public Methods

        /// <summary>
        /// Return the LayerMask based on the projectile
        /// </summary>
        /// <param name="projectile">The projectile that contains the based tag</param>
        /// <returns></returns>
        public LayerMask GetLayerFilterOfProjectile(Projectile projectile) {

            switch (projectile.Tag) {
                case ProjectileTag.Player: return PlayerProjectileHittableLayer;
                case ProjectileTag.Enemy: return EnemyProjectileHittableLayer;
                default: return new LayerMask();

            }

        }

        #endregion

    }

    public enum ProjectileTag {
        Player,
        Enemy
    }

}