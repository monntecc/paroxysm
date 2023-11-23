using System.Diagnostics;
using Discord;
using Discord.WebSocket;
using Paroxysm.Discord;
using WindowsInput;
using System.Text.RegularExpressions;
using WindowsInput.Native;

namespace Paroxysm.Hooks.Actions;

public static class KeyboardAction
{
    private static readonly string pattern = @"<(?<TextA>(ctrl|alt|shift))\+(?<TextB>[^>]*)>";

    private static List<Tuple<string, string>> FindAndSplit(string input)
    {
        List<Tuple<string, string>> result = new List<Tuple<string, string>>();

        Regex regex = new(pattern);

        MatchCollection matches = regex.Matches(input);

        foreach (Match match in matches)
        {
            string type = match.Groups["textA"].Value;
            string keys = match.Groups["textB"].Value;
            
            result.Add(new Tuple<string, string>(type, keys));
        }

        return result;
    }

    private static void SimulateKeyCombination(char[] keys, VirtualKeyCode modifierKey)
    {
        InputSimulator simulator = new();

        simulator.Keyboard.KeyDown(modifierKey);
        foreach (var key in keys)
        {
            simulator.Keyboard.TextEntry(key);
        }
        
        Thread.Sleep(50);

        simulator.Keyboard.KeyUp(modifierKey);
    }
    
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