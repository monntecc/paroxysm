using Paroxysm.API.Commands.Extern;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.API.Commands;

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