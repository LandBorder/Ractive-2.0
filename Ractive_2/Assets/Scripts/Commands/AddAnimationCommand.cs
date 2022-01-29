using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAnimationCommand : Command
{
    I_InstructionSet instructionSet;

    public AddAnimationCommand(I_InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void ExecuteWithParameter(string s)
    {
        instructionSet.AddAnimation(s);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
