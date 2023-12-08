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


        public override void Enter()
        {
            stateMachine.InputReader.CancelEvent += OnCancelHandler;
            stateMachine.Animator.Play(TargetingBlendTree);
        }

        public override void Exit()
        {           
            stateMachine.InputReader.CancelEvent -= OnCancelHandler;            
        }

        public override void Tick(float deltaTime)
        {
            if (stateMachine.Targeter.CurrentTarget == null)
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                return;
            }

        }
        private void OnCancelHandler()
        {
            stateMachine.Targeter.Cancel();
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

}
