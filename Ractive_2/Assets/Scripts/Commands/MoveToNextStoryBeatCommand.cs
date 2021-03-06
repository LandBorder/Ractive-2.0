using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNextStoryBeatCommand : Command
{
    I_InstructionSet instructionSet;

    public MoveToNextStoryBeatCommand(I_InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        instructionSet.MoveToNextStoryBeat();
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
