using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paladin.Physics
{

    public class SpinBehaviour : MonoBehaviour {

        [SerializeField] private Transform target = null;
        public float spinSpeed = 45f;

        #region Unity Events

        private void FixedUpdate() {

            if (target != null) target.Rotate(0f, 0f, spinSpeed * Time.fixedDeltaTime);

        }

        #endregion

    }

}