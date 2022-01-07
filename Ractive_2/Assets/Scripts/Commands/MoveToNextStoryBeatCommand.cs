using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNextStoryBeatCommand : Command
{
    InstructionSet instructionSet;

    public MoveToNextStoryBeatCommand(InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        instructionSet.MoveToNextStoryBeat();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
