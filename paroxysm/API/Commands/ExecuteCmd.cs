using paroxysm.API.Commands.Extern;
using paroxysm.Hooks.Actions;
using paroxysm.API.Commands.Extern;
using paroxysm.Hooks.Actions;

namespace paroxysm.API.Commands;

public class ExecuteCmd : ICommand
{
    public CommandOptions Options()
    {
        return new CommandOptions
        {
            Name = "execute",
            Parameters = "{command}",
            Description = "używa komendy w konsoli"
        };
    }

    public void Execute(string[]? parameters)
    {
        ExecuteCommandAction.Execute(string.Join(" ", parameters));
    }
}