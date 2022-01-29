using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
    public void Execute();
    public void ExecuteWithParameter(string s);
    public void Undo();
}
