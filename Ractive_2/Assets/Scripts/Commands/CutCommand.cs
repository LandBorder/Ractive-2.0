using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutCommand : Command
{
    InstructionSet instructionSet;

    public CutCommand(InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }
    public void Execute()
    {
        instructionSet.StopChoreography();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
