using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerState/Sliding", fileName = "PlayerState_Sliding")]
public class PlayerState_Sliding : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        controller.canAttacked = false;
        controller.Slid();
    }

    public override void Exit()
    {
        base.Exit();
        controller.canAttacked = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (controller.isSlidFinished)
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
