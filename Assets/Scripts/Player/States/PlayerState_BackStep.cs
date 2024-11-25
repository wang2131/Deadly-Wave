using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerState/BackStep", fileName = "PlayerState_BackStep")]
public class PlayerState_BackStep : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        controller.canAttacked = false;
        controller.BackStep();
    }

    public override void Exit()
    {
        base.Exit();
        controller.canAttacked = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!controller.isBackStepFinished && input.isJump)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        
        if (controller.isBackStepFinished)
        {
            if (input.isMove)
            {
                stateMachine.SwitchState(typeof(PlayerState_Move));
            }
            else
            {
                stateMachine.SwitchState(typeof(PlayerState_Idle));
            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
