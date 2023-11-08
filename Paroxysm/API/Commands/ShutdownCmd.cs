using Paroxysm.API.Commands.Extern;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.API.Commands;

public class ShutdownCmd : ICommand
{
    public CommandOptions Options()
    {
        return new CommandOptions
        {
            Name = "shutdown",
            Description = "Wyłącza komputer"
        };
    }

    public void Execute(string[]? parameters)
    {
        ShutdownAction.Follow();
    }
}