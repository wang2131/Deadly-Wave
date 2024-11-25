using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerState/Falling", fileName = "PlayerState_Falling")]
public class PlayerState_Falling : PlayerState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!controller.isGrounded && controller.canAirJump && input.isJump)
        {
            stateMachine.SwitchState(typeof(PlayerState_AirJump));
        }
        
        if (!controller.isGrounded && !controller.canAirJump && input.isJump)
        {
            input.SetJumpInputBufferTimer();
        }
        
        if (controller.isGrounded && input.HasJumpInputBuffer)
        {
            controller.canAirJump = true;
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        if (controller.isGrounded && !input.isMove)
        {
            controller.canAirJump = true;
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }

        if (controller.isGrounded && input.isMove)
        {
            controller.canAirJump = true;
            stateMachine.SwitchState(typeof(PlayerState_Move));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        controller.PlayerMove();
    }
}
