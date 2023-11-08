using Paroxysm.API.Commands.Extern;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.API.Commands;

public class ConsoleCmd : ICommand
{
    public CommandOptions Options()
    {
        return new CommandOptions
        {
            Name = "console",
            Description = "Ukrywa/pokazuje konsole"
        };
    }

    public void Execute(string[]? parameters)
    {
        ConsoleAction.Follow();
    }
}