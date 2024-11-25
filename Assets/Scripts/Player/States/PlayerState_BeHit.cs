using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerState/BeHit", fileName = "PlayerState_BeHit")]
public class PlayerState_BeHit : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        //Debug.Log("BeHit!");
        controller._rigidbody.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!controller.isGrounded && isAnimationFinished)
        {
            stateMachine.SwitchState(typeof(PlayerState_Fall));
        }

        if (controller.isGrounded && input.isMove && isAnimationFinished)
        {
            stateMachine.SwitchState(typeof(PlayerState_Move));
        }

        if (controller.isGrounded && !input.isMove && isAnimationFinished)
        {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        
    }
}
