using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paladin.Core.CombatSystem.Shooter
{

    public class BulletSpawner : MonoBehaviour {

        #region Variables & References

        #region Singleton

        public static BulletSpawner Current {
            get
            {
                return current;
            }
        }
        private static BulletSpawner current;

        #endregion

        [SerializeField] private Object spawnerObject = null;
        private IProjectileSpawner m_projectileSpawner = null;

        #endregion

        #region Unity Events

        private void Awake() {
            current = this;
            if (spawnerObject as IProjectileSpawner != null) m_projectileSpawner = spawnerObject as IProjectileSpawner;
        }

        #endregion

        #region Public Methods

        public GameObject InstantiateProjectile(Transform transform, Projectile prefab) {

            if (m_projectileSpawner != null) return m_projectileSpawner.Instantiate(transform, prefab);
            return null;

        }

        public void DestroyProjectile(Projectile projectile) {

            try {
                m_projectileSpawner.Destroy(projectile);
            }
            catch(System.Exception exception) {
                Debug.LogException(exception);
            }

        }

        #endregion

    }

    public interface IProjectileSpawner {
        GameObject Instantiate(Transform transform, Projectile prefab);
        void Destroy(Projectile projectile);

    }

}