using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpeechToChoreographyCommand : Command
{
    InstructionSet instructionSet;

    public AddSpeechToChoreographyCommand(InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        instructionSet.AddSpeechToChoreography();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
