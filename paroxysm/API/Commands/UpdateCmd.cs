﻿using paroxysm.API.Commands.Extern;
using paroxysm.Hooks.Actions;

namespace paroxysm.API.Commands;

public class UpdateCmd : ICommand
{
    public CommandOptions Options()
    {
        return new CommandOptions
        {
            Name = "update",
            Description = "Aktualizuje program. Wymaga podłączonego USB do komputera"
        };
    }

    public void Execute(string[]? parameters)
    {
        UpdateAction.Execute();
    }
}