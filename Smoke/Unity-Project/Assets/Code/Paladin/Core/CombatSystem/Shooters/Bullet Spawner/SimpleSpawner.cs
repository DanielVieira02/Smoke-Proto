using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paladin.Core.CombatSystem.Shooter {

    public class SimpleSpawner : MonoBehaviour, IProjectileSpawner {

        #region IProjectileSpawner Implementation

        public GameObject Instantiate(Transform transform, Projectile projectile)
        {

            try
            {
                return Instantiate(projectile.gameObject, transform.position, transform.rotation);
            }
            catch (System.Exception exception)
            {
                Debug.LogException(exception);
                return null;
            }

        }

        public void Destroy(Projectile projectile)
        {

            try
            {
                Destroy(projectile.gameObject);
            }
            catch (System.Exception exception)
            {
                Debug.LogException(exception);
            }

        }

        #endregion

    }

}