using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// HANDLES SWITCH AND RUNNING OF STATE MACHINES
public class StateManager : MonoBehaviour
{
    [SerializeField] private State defaultState;
    private State _currentState;

    private void Start()
    {
        _currentState = defaultState;
    }
    // Update is called once per frame
    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        Debug.Log("CURRENT STATE: " + _currentState);
        // Check if current state is null, if not play the state
        State nextState = _currentState?.PlayCurrentState();
        
        if (nextState != null)
        {
            // Switch to sub-state
            SwitchStates(nextState);
        }
    }

    private void SwitchStates(State nextState)
    {
        _currentState = nextState;
    }
}
