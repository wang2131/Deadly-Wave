using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{ 
    protected IState currentState;

    protected Dictionary<System.Type, IState> stateTable;

    public void SwitchState(IState state)
    {
        currentState = state;
    }

    public void SwitchState(System.Type state)
    {
        currentState.Exit();
        SwitchState(stateTable[state]);
        currentState.Enter();
    }

    private void Update()
    {
        currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        currentState.PhysicUpdate();
    }
}
