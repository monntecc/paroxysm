using Paroxysm.API.Commands.Extern;
using Paroxysm.Debug;

namespace Paroxysm.API.Commands;

public class PingCmd : ICommand
{
    public CommandOptions Options()
    {
        return new CommandOptions
        {
            Name = "ping",
            Description = "Pong"
        };
    }

    public void Execute(string[]? parameters)
    {
        Logger.CreateEmbed(ELoggerState.Error, new Exception("Pong!"));
    }
}