using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// HANDLES SWITCH AND RUNNING OF STATE MACHINES
public class StateManager : MonoBehaviour
{
    [SerializeField] private State defaultState;
    private State _currentState;
    private State _deadState;
    private StateManager _thisComponent;

    private void Start()
    {
        _currentState = defaultState;
        _thisComponent = this;
    }
    // Update is called once per frame
    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        //Debug.Log("CURRENT STATE: " + _currentState);
        if (_currentState == _deadState)
        {
            _thisComponent.enabled = false;
            Debug.Break();
        }
        else
        {
            // Check if current state is null, if not play the state
            State nextState = _currentState?.PlayCurrentState();
        
            if (nextState != null)
            {
                // Switch to sub-state
                SwitchStates(nextState);
            }
        }
    }

    private void SwitchStates(State nextState)
    {
        _currentState = nextState;
    }
}
