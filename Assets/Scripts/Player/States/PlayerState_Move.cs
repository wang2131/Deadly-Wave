using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/PlayerState/Move", fileName = "PlayerState_Move")]
public class PlayerState_Move : PlayerState
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
        if (controller.isGrounded && !input.isMove && !input.isJump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
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
        controller.PlayerMove();
    }
}
