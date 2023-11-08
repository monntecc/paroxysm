using Paroxysm.API.Commands.Extern;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.API.Commands;

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
        UpdateAction.Follow();
    }
}