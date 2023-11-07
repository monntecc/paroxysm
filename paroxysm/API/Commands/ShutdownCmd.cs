using paroxysm.API.Commands.Extern;
using paroxysm.Hooks.Actions;

namespace paroxysm.API.Commands;

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