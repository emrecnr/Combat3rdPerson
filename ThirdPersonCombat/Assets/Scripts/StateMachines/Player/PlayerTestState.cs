using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine){}
    

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        Vector3 moveDirection = new Vector3();
        moveDirection.x = stateMachine.InputReader.MovementValue.x;
        moveDirection.y = 0;
        moveDirection.z = stateMachine.InputReader.MovementValue.y;
        stateMachine.Controller.Move(moveDirection*stateMachine.FreeLookMovementSpeed*deltaTime);

        if(stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat("FreeLookSpeed",0,0.1f,deltaTime);
            return;
        }
        stateMachine.transform.rotation = Quaternion.LookRotation(moveDirection);
        stateMachine.Animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltaTime);

    }
}
