using Paroxysm.API.Commands.Extern;
using Paroxysm.Debug;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.API.Commands;

public class WebpageCmd : ICommand
{
    public CommandOptions Options()
    {
        return new CommandOptions
        {
            Name = "webpage",
            Parameters = "{url}",
            Description = "Uruchamia strone w przeglądarce"
        };
    }

    public void Execute(string[]? parameters)
    {
        if (parameters is null || parameters[0].Length <= 0)
        {
            Logger.CreateEmbed(ELoggerState.Error, new Exception("Nie podano linku"));
            return;
        }

        WebpageAction.Follow(parameters[0]);
    }
}