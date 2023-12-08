using System;
using System.Collections;
using System.Collections.Generic;
using TP.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputReader : MonoBehaviour, InputController.IPlayerActions
{
    private InputController _inputs;

    public Vector2 MovementValue { get; private set; }

    public event Action JumpEvent;
    public event Action DodgeEvent;

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
}
