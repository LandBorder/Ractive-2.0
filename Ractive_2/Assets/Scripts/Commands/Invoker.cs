using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker
{
    Command command;

    public Invoker(Command newCommand)
    {
        this.command = newCommand;
    }

    public void ExecuteCommand()
    {
        command.Execute();
    }

    public void ExecuteCommandWithParameter(string s)
    {
        command.ExecuteWithParameter(s);
    }

    public void UndoCommand()
    {
        command.Undo();
    }
}
