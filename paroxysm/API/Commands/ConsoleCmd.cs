using paroxysm.API.Commands.Extern;
using paroxysm.Hooks.Actions;

namespace paroxysm.API.Commands;

public class ConsoleCmd : ICommand
{
    public CommandOptions Options()
    {
        return new CommandOptions
        {
            Name = "console",
            Description = "Ukrywa/pokazywa konsole"
        };
    }

    public void Execute(string[]? parameters)
    {
        ConsoleAction.Follow();
    }
}