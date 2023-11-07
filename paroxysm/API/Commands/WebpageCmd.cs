using paroxysm.API.Commands.Extern;
using paroxysm.Debug;
using paroxysm.Hooks.Actions;

namespace paroxysm.API.Commands;

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
        if (parameters is null || parameters[1].Length <= 0)
        {
            Logger.CreateEmbed(ELoggerState.Error, new Exception("Nie podano linku"));
            return;
        }

        WebpageAction.Follow(parameters[0]);
    }
}