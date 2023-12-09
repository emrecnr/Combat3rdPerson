using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace TP.CombatSystem.StateMachines.Player
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerStateMachine stateMachine;
 
        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        protected void Move(Vector3 motion, float deltaTime)
        {
            stateMachine.Controller.Move((motion + stateMachine.Receiver.Movement)* deltaTime);
        }
        protected void FaceTarget()
        {
            if(stateMachine.Targeter.CurrentTarget == null) return;
            Vector3 lookPosition = stateMachine.Targeter.CurrentTarget.transform.position-
                                    stateMachine.transform.position;
            lookPosition.y = 0f;
            stateMachine.transform.rotation = Quaternion.LookRotation(lookPosition);
        }

    }
}
