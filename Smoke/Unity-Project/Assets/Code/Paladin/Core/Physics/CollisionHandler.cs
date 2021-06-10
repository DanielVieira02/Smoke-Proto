using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Utilities;

namespace Paladin.Core.Physics {

    public class CollisionHandler : MonoBehaviour {

        #region Variables & Parameters

        [SerializeField] private LayerMask collisionFilter = new LayerMask();

        [Space(10)]

        [SerializeField] private UnityEvent OnCollisionEnter = new UnityEvent(), OnTriggerEnter = new UnityEvent();

        #endregion


        #region Unity Events

        private void OnCollisionEnter2D(Collision2D collision) {

            bool isFilterValid = false;
            isFilterValid = UtilitiesClass.LayerMaskContains(collisionFilter, collision.gameObject.layer);

            if (isFilterValid)
                OnCollisionEnter?.Invoke();

        }

        private void OnTriggerEnter2D(Collider2D collision) {

            bool isFilterValid = false;
            isFilterValid = UtilitiesClass.LayerMaskContains(collisionFilter, collision.gameObject.layer);

            if (isFilterValid)
                OnTriggerEnter?.Invoke();

        }

        #endregion

        #region Public Methods

        public void SetLayerMask(LayerMask layerFilter) {

            collisionFilter = layerFilter;

        }

        #endregion

    }

}