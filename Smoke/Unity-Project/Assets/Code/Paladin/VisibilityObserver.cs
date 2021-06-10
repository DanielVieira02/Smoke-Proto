using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Paladin.Core {

    public class VisibilityObserver : MonoBehaviour {

        #region State Enum

        protected enum State {
            Unknown,
            Visible,
            Invisible
        }

        #endregion

        #region Variables & References

        [SerializeField] private UnityEvent OnBecameVisible = new UnityEvent();
        [SerializeField] private UnityEvent OnBecameInvisible = new UnityEvent();

        [SerializeField] private bool m_callEventOnStart = false;
        [SerializeField] private bool m_persistentObserver = false;
        [SerializeField] private float m_observeChangeAtEachSeconds = 0.1f;

        private Camera m_camera = null;
        private State state = State.Unknown;

        #endregion


        #region Unity Events

        private void Start() {
            m_camera = Camera.main;
            
            state = CalculeState();
            if (m_callEventOnStart) CallRespectiveEvent();

            StartCoroutine(ObserveChange());
        }

        #endregion

        #region Private Methods

        private void CallRespectiveEvent() {
            switch (state)
            {
                case State.Visible: OnBecameVisible?.Invoke(); break;
                case State.Invisible: OnBecameInvisible?.Invoke(); break;
            }

        }

        private State CalculeState() {

            if(m_camera == null) {
                Debug.LogError("There's no camera in the scene");
                return State.Unknown;
            }

            Vector2 currentPosition = transform.position;

            Rect cameraView = new Rect(
                -(m_camera.orthographicSize * m_camera.aspect) - m_camera.transform.position.x,
                -m_camera.orthographicSize - m_camera.transform.position.y,
                m_camera.orthographicSize * 2f * m_camera.aspect,
                m_camera.orthographicSize * 2f);

            return cameraView.Contains(currentPosition) ? State.Visible : State.Invisible;

        }

        private IEnumerator ObserveChange() {

            State state = this.state;

            while (state == this.state) { 
                yield return new WaitForSeconds(m_observeChangeAtEachSeconds);
                state = CalculeState();
            }

            this.state = state;

            CallRespectiveEvent();

            if (m_persistentObserver && gameObject.activeSelf) StartCoroutine(ObserveChange());

            yield return null;

        }

        #endregion

        #region Public Methods

        public void StartObserver() {
            StopCoroutine(ObserveChange());
            StartCoroutine(ObserveChange());
        }

        #endregion

    }

}