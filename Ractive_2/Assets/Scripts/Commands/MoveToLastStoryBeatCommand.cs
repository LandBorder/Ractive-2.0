using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLastStoryBeatCommand : Command
{
    I_InstructionSet instructionSet;

    public MoveToLastStoryBeatCommand(I_InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        instructionSet.MoveToLastStoryBeat();
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
