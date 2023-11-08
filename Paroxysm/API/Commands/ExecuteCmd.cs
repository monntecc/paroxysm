using Paroxysm.API.Commands.Extern;
using Paroxysm.Hooks.Actions;
using Paroxysm.API.Commands.Extern;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.API.Commands;

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