using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paladin.IA
{

    public class SineMovement : MonoBehaviour {

        [SerializeField] private float frequency = 1f;
        [SerializeField] private float velocity = 1f;

        private float timerCount = 0f;

        private void FixedUpdate() {

            timerCount += Time.fixedDeltaTime * velocity;

            Vector2 movementVector = Vector2.zero;
            movementVector.x = Mathf.Cos(timerCount) * frequency;
            transform.Translate(movementVector * Time.fixedDeltaTime);

        }
    }

}