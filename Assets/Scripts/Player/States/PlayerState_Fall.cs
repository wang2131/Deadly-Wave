using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerState/Fall", fileName = "PlayerState_Fall")]
public class PlayerState_Fall : PlayerState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        controller.canAirJump = true;
    }

    public override void LogicUpdate()
    {
        if(!controller.isGrounded && controller.canAirJump && input.isJump)
        {
            stateMachine.SwitchState(typeof(PlayerState_AirJump));
        }
        

        if (controller.isGrounded)
        {
            if (input.isMove)
            {
                stateMachine.SwitchState(typeof(PlayerState_Move));
            }
            else
            {
                stateMachine.SwitchState(typeof(PlayerState_Move));
            }
        }
        
        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        controller.PlayerMove();
    }
}
