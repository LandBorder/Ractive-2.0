using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPositionCommand : Command
{
    I_InstructionSet instructionSet;

    public MoveToPositionCommand(I_InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        instructionSet.MoveToPosition();
    }

    public void ExecuteWithParameter(string s)
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        instructionSet.MoveToPreviousPosition();
    }
}
