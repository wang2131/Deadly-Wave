using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    public override void Enter()
    {
        base.Enter();
    }

    public  override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (controller.isGrounded && input.isMove)
        {
            stateMachine.SwitchState(typeof(PlayerState_Move));
        }

        if (controller.isGrounded && input.isJump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        
        if (controller.isGrounded && input.isSlid)
        {
            stateMachine.SwitchState(typeof(PlayerState_Sliding));
        }

        if (controller.isGrounded && input.isBackStep)
        {
            stateMachine.SwitchState(typeof(PlayerState_BackStep));
        }
        
        if (!controller.isGrounded)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        controller._rigidbody.velocity = Vector2.zero;
    }
}
