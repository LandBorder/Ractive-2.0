using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFacialExpressionCommand : Command
{
    I_InstructionSet instructionSet;

    public AddFacialExpressionCommand(I_InstructionSet newInstructionSet)
    {
        this.instructionSet = newInstructionSet;
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void ExecuteWithParameter(string s)
    {
        instructionSet.AddFacialExpression(s);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
