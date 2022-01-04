using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToChoreographyCommand : Command
{
    InstructionSet instructionSet;

    public AddToChoreographyCommand(InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        instructionSet.AddToChoreography();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
