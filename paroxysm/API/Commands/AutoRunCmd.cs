using paroxysm.API.Commands.Extern;
using paroxysm.Hooks.Actions;

namespace paroxysm.API.Commands;

public class AutoRunCmd : ICommand
{
    public CommandOptions Options()
    {
        return new CommandOptions
        {
            Name = "autorun",
            Description = "Włącza opcje autorun"
        };
    }

    public void Execute(string[]? parameters)
    {
        AutoRunAction.Follow();
    }
}