using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementStrategy
{
    public MovementStrategy()
    {
    }

    protected Parameter[] parameters;
    public Parameter[] GetParameters() { return (Parameter[])parameters.Clone(); }
    public abstract void Initialize();
    public abstract void Execute();
}
