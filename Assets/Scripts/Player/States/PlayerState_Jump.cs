using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerState/Jump", fileName = "PlayerState_Jump")]
public class PlayerState_Jump : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        controller.PlayerJump();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {   
        //二段跳
        if(!controller.isGrounded && controller.canAirJump && input.isJump)
        {
            stateMachine.SwitchState(typeof(PlayerState_AirJump));
        }
        
        //下落
        if(isAnimationFinished)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        
        // //空闲
        // if (controller.isGrounded && !input.isMove)
        // {
        //     stateMachine.SwitchState(typeof(PlayerState_Idle));
        // }
        //
        // //移动
        // if (controller.isGrounded && input.isMove)
        // {
        //     stateMachine.SwitchState(typeof(PlayerState_Move));
        // }
        
    }

    public override void PhysicUpdate()
    {
        controller.PlayerMove();
    }
}
