using System.Collections;
using System.Collections.Generic;
using TP.CombatSystem.Combat.Target;
using TP.CombatSystem.Inputs;
using UnityEngine;

namespace TP.CombatSystem.StateMachines.Player
{
    public class PlayerStateMachine : StateMachines
    {
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Targeter Targeter { get; private set; }
        public Transform MainCameraTransform { get; private set; }
        [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
        [field: SerializeField] public float RotationDampSpeed { get; private set; }


        private void Start()
        {
            MainCameraTransform = Camera.main.transform;
            SwitchState(new PlayerFreeLookState(this));
        }
        private void Update()
        {
            currentState.Tick(Time.deltaTime);
        }
    }
}

