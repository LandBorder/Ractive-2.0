using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutCommand : Command
{
    I_InstructionSet instructionSet;

    public CutCommand(I_InstructionSet newInstructionSet)
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
