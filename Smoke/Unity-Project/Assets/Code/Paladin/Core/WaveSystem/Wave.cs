using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Paladin.Core.WaveSystem {


    public class Wave : MonoBehaviour {

        protected enum CallFinishWaveWhen {
            AllEnemiesSpawned,
            AllEnemiesGetDisabled
        }
        
        #region Variables & References

        [SerializeField] private Transform[] enemies = null;
        [SerializeField] private float intervalBetweenSpawn = 0f;

        [SerializeField] private CallFinishWaveWhen callFinishWaveWhen;

        #region Events

        public UnityEvent OnWaveStart = new UnityEvent();
        public UnityEvent OnWaveFinish = new UnityEvent();

        #endregion

        #endregion


        #region Unity Events

        private void Awake() {

            SetupEnemies();

        }

        #endregion

        #region Public Methods

        public void StartWave() {

            OnWaveStart?.Invoke();
            StartCoroutine(Routine_SpawnEnemies());

        }

        #endregion

        #region Private Methods

        private void SetupEnemies() {

            foreach (Transform enemy in enemies) {

                enemy.gameObject.SetActive(false);

            }

        }

        #endregion


        #region Coroutines

        private IEnumerator Routine_SpawnEnemies() {

            foreach(Transform enemy in enemies) {

                enemy.gameObject.SetActive(true);

                yield return new WaitForSeconds(intervalBetweenSpawn);

            }

            switch (callFinishWaveWhen) {
                case CallFinishWaveWhen.AllEnemiesSpawned:
                    OnWaveFinish?.Invoke();
                    break;
            }

        }

        #endregion

    }

}

