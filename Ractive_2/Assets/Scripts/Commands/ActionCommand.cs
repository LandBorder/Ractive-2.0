using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCommand : Command
{
    I_InstructionSet instructionSet;

    public ActionCommand(I_InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        instructionSet.StartChoreography();
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
