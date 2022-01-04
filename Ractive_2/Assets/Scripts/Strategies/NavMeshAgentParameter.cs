using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentParameter : Parameter
{
    private NavMeshAgent _value;
    public NavMeshAgent GetValue() { return _value; }
    public void SetValue(NavMeshAgent value) { _value = value; }
    public NavMeshAgentParameter(string name, NavMeshAgent defaultvalue) : base(name)
    {
        _value = defaultvalue;
    }
}
