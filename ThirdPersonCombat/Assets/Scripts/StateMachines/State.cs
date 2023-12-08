using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class State
{
    public abstract void Enter(); // State Enter
    public abstract void Tick(float deltaTime); // State Loop
    public abstract void Exit(); // State Exit
}
