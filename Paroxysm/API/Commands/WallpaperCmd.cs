using Paroxysm.API.Commands.Extern;
using Paroxysm.Debug;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.API.Commands;

public class WallpaperCmd : ICommand
{
    public CommandOptions Options()
    {
        return new CommandOptions
        {
            Name = "wallpaper",
            Parameters = "{url}",
            Description = "Zmienia tapete na wyznaczoną"
        };
    }

    [Obsolete("Obsolete")]
    public void Execute(string[]? parameters)
    {
        if (parameters is not null && parameters[0].Length > 0)
        {
            ChangeWallpaperAction.Follow(parameters[0]);
            return;
        }
        
        Logger.CreateEmbed(ELoggerState.Error, new Exception("Brak linku do obrazu"));
    }
}