using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCommand : Command
{
    InstructionSet instructionSet;

    public ActionCommand(InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        instructionSet.StartChoreography();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
