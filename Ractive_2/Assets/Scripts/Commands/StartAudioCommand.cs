using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudioCommand : Command
{
    I_InstructionSet instructionSet;

    public StartAudioCommand(I_InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        instructionSet.StartAudio();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
