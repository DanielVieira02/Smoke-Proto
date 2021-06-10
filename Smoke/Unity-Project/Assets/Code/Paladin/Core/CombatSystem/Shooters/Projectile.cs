using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utilities;

namespace Paladin.Core.CombatSystem.Shooter {

    /// <summary>
    /// Class that represents the instance of a projectile
    /// </summary>
    public class Projectile : MonoBehaviour {

        #region Movement Struct

        [System.Serializable]
        public struct Movement {

            /// <summary>
            /// The speed of movement
            /// </summary>
            public float Speed { get { return m_speed; } set { m_speed = value; } }
            [SerializeField] private float m_speed;

            /// <summary>
            /// The direction that the object will take
            /// </summary>
            public float Direction { get { return m_speed; } set{ m_speed = value; } }
            [SerializeField] private float m_direction;

        }

        #endregion

        #region State Enum

        protected enum State {
            Stop,
            Move
        }

        #endregion


        #region Variables & References

        [Header("Global Settings")]

        /// <summary>
        /// The global settings of that bulelt
        /// </summary>
        [SerializeField] private GlobalProjectileSettings m_globalProjectileSettings = null;

        [Space(20)]

        /// <summary>
        /// The struct that defines the movement of the projectile
        /// </summary>
        [SerializeField] private Movement m_movement;


        /// <summary>
        /// The tag of this projectile. It defines the collision settings for this instance
        /// </summary>
        public ProjectileTag Tag { get { return m_projectileTag; } }
        /// <summary>
        /// The tag of this projectile. It defines the collision settings for this instance
        /// </summary>
        [SerializeField] private ProjectileTag m_projectileTag;


        #region Non-serialize variables

        /// <summary>
        /// The current state of the projectile
        /// </summary>
        private State state = State.Stop;

        #endregion

        #endregion


        #region Unity Events

        private void Awake() {

            StartMovement();
        
        }

        private void Update() {

            #region States treatment
            switch (state) {

                #region Move
                case State.Move:
                    transform.Translate(Vector2.right * m_movement.Speed * Time.deltaTime);
                    break;
                #endregion

            }
            #endregion
        
        }

        #region Collision Events

        private void OnCollisionEnter2D(Collision2D collision) {

            LayerMask collisionFilter = m_globalProjectileSettings.GetLayerFilterOfProjectile(this);

            if(UtilitiesClass.LayerMaskContains(collisionFilter, collision.gameObject.layer)) {

                IDamageable target = collision.gameObject.GetComponent<IDamageable>();
                if (target != null) DealDamage(target);

            }

        }

        private void OnTriggerEnter2D(Collider2D collision) {

            LayerMask collisionFilter = m_globalProjectileSettings.GetLayerFilterOfProjectile(this);

            if (UtilitiesClass.LayerMaskContains(collisionFilter, collision.gameObject.layer)) {

                IDamageable target = collision.gameObject.GetComponent<IDamageable>();
                if (target != null) DealDamage(target);

            }

        }

        #endregion


        #endregion

        #region Private Methods

        private void DealDamage(IDamageable target) {
            
            if (target != null) target.TakeDamage(1);

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Makes the projectile starts it movement
        /// </summary>
        public void StartMovement() {

            state = State.Move;

        }

        /// <summary>
        /// Makes the projectile stop
        /// </summary>
        public void StopMovement() {
            state = State.Stop;
        }

        /// <summary>
        /// Destroy that instance of projectile
        /// </summary>
        public void Destroy() {

            if(BulletSpawner.Current != null) {
                BulletSpawner.Current.DestroyProjectile(this);
            } else {
                Debug.LogError("Something goes wrong!");
            }

        }

        #endregion

    }


}