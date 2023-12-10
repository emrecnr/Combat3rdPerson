using System.Collections;
using System.Collections.Generic;
using TP.CombatSystem.StateMachines.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace TP.Combat.StateMachines
{
    public class PlayerTargetingState : PlayerBaseState
    {
        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){}

        private readonly int TargetingBlendTree = Animator.StringToHash("TargetingBlendTree");
        private readonly int TargetingForward = Animator.StringToHash("TargetingForwardSpeed");
        private readonly int TargetingRight = Animator.StringToHash("TargetingRightSpeed");


        public override void Enter()
        {
            stateMachine.InputReader.CancelEvent += OnCancelHandler;
            stateMachine.Animator.Play(TargetingBlendTree);
        }

        public override void Tick(float deltaTime)
        {
            if(stateMachine.InputReader.IsAttacking) stateMachine.SwitchState(new PlayerAttackingState(stateMachine,0)) ;
            if (stateMachine.Targeter.CurrentTarget == null)
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                return;
            }
            Vector3 movement = CalculateMovement();
            Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);  
            UpdateAnimator(deltaTime);
            FaceTarget();
        }

        public override void Exit()
        {           
            stateMachine.InputReader.CancelEvent -= OnCancelHandler;            
        }

       
        protected Vector3 CalculateMovement()
        {
            Vector3 movement = new Vector3();
            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
            return movement;
        }
        private void UpdateAnimator(float deltaTime)
        {
            // Forward:
            if(stateMachine.InputReader.MovementValue.y == 0)
            {
                stateMachine.Animator.SetFloat(TargetingForward, 0 , 0.1f,deltaTime);
            }
                                
            else
            {
                float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
                stateMachine.Animator.SetFloat(TargetingForward,value, 0.1f, deltaTime);
            }
            // Right:
            if (stateMachine.InputReader.MovementValue.x == 0)
            {
                stateMachine.Animator.SetFloat(TargetingRight, 0, 0.1f, deltaTime);
            }

            else
            {
                float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
                stateMachine.Animator.SetFloat(TargetingRight, value, 0.1f, deltaTime);
            }
        }
        private void OnCancelHandler()
        {
            stateMachine.Targeter.Cancel();
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

}
