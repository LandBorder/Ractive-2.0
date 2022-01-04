using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPositionCommand : Command
{
    InstructionSet instructionSet;

    public MoveToPositionCommand(InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        instructionSet.MoveToPosition();
    }

    public void Undo()
    {
        instructionSet.MoveToPreviousPosition();
    }
}
