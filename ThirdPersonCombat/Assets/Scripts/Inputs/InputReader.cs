using System;
using System.Collections;
using System.Collections.Generic;
using TP.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace TP.CombatSystem.Inputs
{
    public class InputReader : MonoBehaviour, InputController.IPlayerActions
    {
        private InputController _inputs;

        public Vector2 MovementValue { get; private set; }
        public bool IsAttacking {get; private set;}

        public event Action JumpEvent;
        public event Action DodgeEvent;
        public event Action TargetEvent;
        public event Action CancelEvent;
        

        private void OnEnable()
        {
            _inputs = new InputController();
            _inputs.Player.SetCallbacks(this);
            _inputs.Enable();
        }
        private void OnDisable()
        {
            _inputs.Disable();
        }

        public void OnDodge(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            DodgeEvent?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            JumpEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {

        }

        public void OnTarget(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            TargetEvent?.Invoke();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if(!context.performed) return;
            CancelEvent?.Invoke();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed) IsAttacking = true;
            else if (context.canceled) IsAttacking = false;
        }
    }
}
