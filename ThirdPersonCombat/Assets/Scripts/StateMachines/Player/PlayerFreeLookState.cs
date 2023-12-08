using System.Collections;
using System.Collections.Generic;
using TP.Combat.StateMachines;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TP.CombatSystem.StateMachines.Player
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }
        
        private readonly int FreeLookSpeed = Animator.StringToHash("FreeLookSpeed");
        private readonly int FreeLookBlendTree = Animator.StringToHash("FreeLookBlendTree");
        private const float AnimatorDampTime = 0.1f;


        public override void Enter()
        {
            stateMachine.InputReader.TargetEvent += OnTargetHandler;
            stateMachine.Animator.Play(FreeLookBlendTree);
        }

        public override void Exit()
        {
            stateMachine.InputReader.TargetEvent -= OnTargetHandler;
        }

        private void OnTargetHandler()
        {
            if(!stateMachine.Targeter.SelectTarget()) return;
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
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
            FaceMovementDirection(moveDirection, deltaTime);
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
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(moveDirection), deltaTime * stateMachine.RotationDampSpeed);
        }


    }

}