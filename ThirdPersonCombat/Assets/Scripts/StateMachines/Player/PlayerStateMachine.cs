using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PlayerStateMachine : StateMachines
{
    [field : SerializeField]public InputReader InputReader {get; private set;}
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed{get; private set;}


    private void Start()
    {
        SwitchState(new PlayerTestState(this));
    }
    private void Update()
    {
        currentState.Tick(Time.deltaTime);
    }
}
