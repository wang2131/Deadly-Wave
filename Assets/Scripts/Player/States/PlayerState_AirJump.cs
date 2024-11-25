using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerState/AirJump", fileName = "PlayerState_AirJump")]
public class PlayerState_AirJump : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        controller.canAirJump = false;
        controller.PlayerJump();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        controller.PlayerMove();
    }
}
