using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paladin.AI {

    public class Aimer : MonoBehaviour {

        #region Variables & References

        [SerializeField] private string aimTag = "";
        [SerializeField] private float searchRadius = 5f;
        [SerializeField] private float angleOffset = 90f;

        [SerializeField] private Transform rotateBody = null;
        [SerializeField] private float rotateSpeed = 15f;

        private Transform target = null;

        #endregion


        private void Start() {
            StartCoroutine(SearchTarget());
        }


        private IEnumerator SearchTarget() {

            while (target == null) {

                GameObject potentialTarget = null;
                float currentDistance = searchRadius;

                foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag(aimTag)) {

                    if(Vector2.Distance(gameObject.transform.position, rotateBody.position) < currentDistance) {
                        currentDistance = Vector2.Distance(gameObject.transform.position, rotateBody.position);
                        potentialTarget = gameObject;
                    }

                }

                if (potentialTarget != null) {
                    target = potentialTarget.transform;
                    StartCoroutine(AimTarget());
                    break;
                }

                yield return new WaitForSeconds(0.5f);

            }


        }

        private IEnumerator AimTarget() {

            while(target != null){

                if(Vector2.Distance(rotateBody.position, target.position) > searchRadius) {
                    target = null;
                    StartCoroutine(SearchTarget());
                    break;
                }
                
                Vector2 difference = rotateBody.position - target.position;
                difference.Normalize();
                float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //The angle between the aimer and the target
                float newAngle = Mathf.LerpAngle(rotateBody.eulerAngles.z, angle + angleOffset, rotateSpeed * Time.fixedDeltaTime); //The new angle that the aimer will assume

                rotateBody.eulerAngles = new Vector3(0f, 0f, newAngle);

                yield return new WaitForFixedUpdate();

            }

        }

    }

}