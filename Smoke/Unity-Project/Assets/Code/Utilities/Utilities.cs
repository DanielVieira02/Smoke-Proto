using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{

    public class UtilitiesClass {

        public static float LookAt2D(Vector2 pointA, Vector2 pointB) {

            Vector2 difference = pointA - pointB;
            difference.Normalize();
            return Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        }
        public static Vector3 LookAt2D(Vector2 pointA, Vector2 pointB, Transform target)
        {

            Vector2 difference = pointA - pointB;
            difference.Normalize();

            float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            target.eulerAngles = new Vector3(target.eulerAngles.x, target.eulerAngles.y, angle);
            return target.eulerAngles;

        }

        public static bool LayerMaskContains(LayerMask layerMask, int layer) {

            return (layerMask == (layerMask | (1 << layer)));

        }

        public static bool FloatIsEqualToZero(float value) {
            return value >= -Mathf.Epsilon && value <= Mathf.Epsilon;
        }

        public static int SortNumberInRandomSpaces(float accumulatedChance, List<RandomSpace> randomSpaces) {

            int value = 0;
            float numberSorted = Random.Range(0f, accumulatedChance);

            foreach (RandomSpace randomSpace in randomSpaces)
            {
                
                if (randomSpace.IsSorted(numberSorted))
                {
                    value = randomSpaces.IndexOf(randomSpace);
                    break;
                }

            }

            return value;

        }

        public static bool DoesTagExist(string tag) {

            try {
                GameObject.FindGameObjectsWithTag(tag);
                return true;
            }
            catch {
                return false;
            }
        }

        public static bool PointIsOnViewport(Vector2 point, Camera camera) {

            Debug.Log(camera.WorldToViewportPoint(point));

            return true;

        }

    }

    public class RandomSpace {

        public int Number { get { return number; } }

        private int number = 0;
        private float min = 0f, max = 1f;

        public RandomSpace(int number, float min, float max) {

            this.number = number;
            this.min = min;
            this.max = max;

        }

        public bool IsSorted(float value) {

            return (value >= min && value < max);

        }

    }

}