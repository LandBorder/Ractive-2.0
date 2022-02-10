using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteChoreographyCommand : Command
{
    I_InstructionSet instructionSet;

    public DeleteChoreographyCommand(I_InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }
    public void Execute()
    {
        instructionSet.DeleteChoreography();
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
