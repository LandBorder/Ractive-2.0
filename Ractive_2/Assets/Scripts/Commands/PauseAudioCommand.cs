using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAudioCommand : Command
{
    I_InstructionSet instructionSet;

    public PauseAudioCommand(I_InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        instructionSet.PauseAudio();
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
