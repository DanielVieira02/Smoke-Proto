using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelStartTrigger : MonoBehaviour {

    [SerializeField] private UnityEvent OnLevelStart = new UnityEvent();

    private void Awake() {

        OnLevelStart?.Invoke();

    }
    
}
