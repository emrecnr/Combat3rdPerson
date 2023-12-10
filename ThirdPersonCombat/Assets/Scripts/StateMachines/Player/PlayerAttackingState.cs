using System.Collections;
using System.Collections.Generic;
using TP.CombatSystem.Combat;
using TP.CombatSystem.StateMachines.Player;
using UnityEngine;

namespace TP.CombatSystem.StateMachines.Player
{
    public class PlayerAttackingState : PlayerBaseState
    {
        private Attack _attack;
        private float previousFrameTime;
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
            if(normalizedTime > previousFrameTime && normalizedTime < 1f)
            {
                if(stateMachine.InputReader.IsAttacking)
                {
                    TryComboAttack(normalizedTime);
                }
            } 
            else{
                //back to locomotion
            }
            previousFrameTime = normalizedTime;
        }
        private void TryComboAttack(float normalizedTime)
        {
            if(_attack.ComboStateIndex == -1) return;
            if(normalizedTime < _attack.ComboAttackTime) return;
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,_attack.ComboStateIndex));
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