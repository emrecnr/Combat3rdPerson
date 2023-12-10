using System.Collections;
using System.Collections.Generic;
using TP.Combat.StateMachines;
using TP.CombatSystem.Combat;
using TP.CombatSystem.StateMachines.Player;
using UnityEngine;

namespace TP.CombatSystem.StateMachines.Player
{
    public class PlayerAttackingState : PlayerBaseState
    {
        private Attack _attack;
        private float previousFrameTime;
        private bool alreadyAppliedForce;
        public PlayerAttackingState(PlayerStateMachine stateMachine,int attackId) : base(stateMachine)
        {
            _attack = stateMachine.Attacks[attackId];
        }


        public override void Enter()
        {
            stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName,_attack.AnimationTransitionDuration);
        }

        public override void Exit()
        {

        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
            FaceTarget();
            float normalizedTime = GetNormalizeTime();
            if(normalizedTime >= previousFrameTime && normalizedTime < 1f)
            {
                if(normalizedTime >= _attack.ForceTime) TryApplyForce();
                if(stateMachine.InputReader.IsAttacking)
                                    TryComboAttack(normalizedTime);                
            } 
            else{
                if(stateMachine.Targeter.CurrentTarget != null)
                        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
                else
                    stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
            previousFrameTime = normalizedTime;
        }
        private void TryComboAttack(float normalizedTime)
        {
            if(_attack.ComboStateIndex == -1) return;
            if(normalizedTime < _attack.ComboAttackTime) return;
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,_attack.ComboStateIndex));
        }
        private void TryApplyForce()
        {
            if(alreadyAppliedForce) return;
            stateMachine.Receiver.AddForce(stateMachine.transform.forward* _attack.Force);
            alreadyAppliedForce = true;
        }
        private float GetNormalizeTime()
        {
            AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);
            if(stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
                            return nextInfo.normalizedTime;
            
            else if(!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
                         return currentInfo.normalizedTime;
            
            else
             return 0f;
          
        }
    }

}