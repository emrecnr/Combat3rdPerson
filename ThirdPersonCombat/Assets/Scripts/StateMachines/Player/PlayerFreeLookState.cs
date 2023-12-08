using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookSpeed = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Tick(float deltaTime)
    {
        Vector3 moveDirection = CalculateMovement();

        stateMachine.Controller.Move(moveDirection * stateMachine.FreeLookMovementSpeed * deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeed, 0, AnimatorDampTime, deltaTime);
            return;
        }
        FaceMovementDirection(moveDirection,deltaTime);
        stateMachine.Animator.SetFloat(FreeLookSpeed, 1, AnimatorDampTime, deltaTime);

    }
    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();
        return forward * stateMachine.InputReader.MovementValue.y +
                                                    right * stateMachine.InputReader.MovementValue.x;
    }


    private void FaceMovementDirection(Vector3 moveDirection, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(moveDirection),deltaTime*stateMachine.RotationDampSpeed);
    }


}
