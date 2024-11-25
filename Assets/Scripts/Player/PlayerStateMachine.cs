using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    private PlayerInput input;

    private PlayerController controller;

    private Animator animator;
    
    [SerializeField]
    private PlayerState[] states;

    private void Awake()
    {
        stateTable = new Dictionary<Type, IState>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        
        foreach(PlayerState state in states)
        {
            state.Initialize(animator, controller, input, this);
            stateTable.Add(state.GetType(), state);
        }
    }

    private void Start()
    {
        currentState = stateTable[typeof(PlayerState_Idle)];
    }
}
