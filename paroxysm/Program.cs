using paroxysm.API;
using paroxysm.Hooks.Actions;

namespace paroxysm;

internal abstract class Program
{
    [Obsolete("Obsolete")]
    public static void Main(string[] args)
    {
        // Hide console
        ConsoleAction.Follow();

        // Initialize discord bot
        DiscordAPI.Init().GetAwaiter().GetResult();
    }
}