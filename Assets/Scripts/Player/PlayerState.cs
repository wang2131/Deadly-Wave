using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerState : ScriptableObject, IState
{
    [SerializeField]
    protected string stateName;
    protected int stateHash;
    protected Animator animator;
    protected PlayerInput input;
    protected PlayerController controller;
    protected PlayerStateMachine stateMachine;
    protected float stateStartTime;
    protected float animateDuration => Time.time - stateStartTime;
    protected bool isAnimationFinished => animateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;
    
    public void Initialize(Animator animator, PlayerController controller, PlayerInput input, PlayerStateMachine stateMachine)
    {
        this.animator = animator;
        this.controller = controller;
        this.input = input;
        this.stateMachine = stateMachine;
    }

    private void Awake()
    {
        stateHash = Animator.StringToHash(stateName) & Int32.MaxValue;
    }

    public virtual void Enter()
    {
        //animator.Play(stateHash);
        animator.Play(stateName);
        stateStartTime = Time.time;
        //Debug.Log("Played "+ stateName);
    }

    public virtual void Exit()
    {
        
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicUpdate()
    {
        
    }
    
    
    
}
