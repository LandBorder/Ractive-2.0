using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPositionCommand : Command
{
    I_InstructionSet instructionSet;

    public KeepPositionCommand(I_InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        instructionSet.KeepPosition();
    }

    public void ExecuteWithParameter(string s)
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
