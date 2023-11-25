using Discord;
using Discord.WebSocket;
using Paroxysm.Discord;
using WindowsInput;
using System.Text.RegularExpressions;
using WindowsInput.Native;

namespace Paroxysm.Hooks.Actions;

public static class KeyboardAction
{
    private static readonly string pattern = @"{([^{}]+)}";
    private static InputSimulator simulator = new();

    private static (string modifiedText, List<string> placeholders) GetData(string input)
    {
        var placeholders = new List<string>();

        Regex regex = new Regex(pattern);

        string result = regex.Replace(input, match =>
        {
            placeholders.Add(match.Groups[1].Value);

            return "{" + (placeholders.Count - 1) + "}";
        });
        
         return (result, placeholders);
    }
    
    private static void SimulateKeyPress(string inputText)
    {
        foreach (char c in inputText)
        {
            simulator.Keyboard.TextEntry(c);
        }
    }

    private static void SimulateSpecialKeyPress(VirtualKeyCode keyCode)
    {
        simulator.Keyboard.KeyPress(keyCode);
    }

    private static VirtualKeyCode GetFunctionKey(string key)
    {
        if(Enum.TryParse<VirtualKeyCode>(key, true, out var keyCode))
        {
            return keyCode;
        }
        else
        {
            throw new ArgumentException($@"Unknown Function Key: F{key}");
        }
    }
    
    private static VirtualKeyCode GetCodeFromString(string key)
    {
        switch (key.ToLower())
        {
            case ("enter") : return VirtualKeyCode.RETURN;
            case ("backspace") : return VirtualKeyCode.BACK;
            case ("tab") : return VirtualKeyCode.TAB;
            case ("shift") : return VirtualKeyCode.SHIFT;
            case ("capslock") : return VirtualKeyCode.CAPITAL;
            case ("ctrl") : return VirtualKeyCode.CONTROL;
            case ("windows") : return VirtualKeyCode.LWIN;
            default: throw new ArgumentException($@"Unknown special key: {key.ToLower()}");
        }
    }
    
    private static void SimulateSequence(IEnumerable<VirtualKeyCode> keyCodes)
    {
        foreach (var keyCode in keyCodes)
        {
            simulator.Keyboard.KeyDown(keyCode);
        }
        
        Thread.Sleep(50);
        
        foreach (var keyCode in keyCodes)
        {
            simulator.Keyboard.KeyUp(keyCode);
        }
    }

    private static void SimulateSequenceWithSpecial(string text, IEnumerable<string> list)
    {
        int index = 0;
        string[] fragments = text.Split('{', '}');

        foreach (var fragment in fragments)
        {
            if (fragment.Length > 0)
            {
                if (int.TryParse(fragment, out int sequenceNumber))
                {
                    // sequence Number - znak wewnątrz {int}
                    var data = (list as List<string>)[sequenceNumber];
                }
            }
        }
    }
    
    public static Embed Follow(SocketSlashCommand slashCommand)
    {
        string inputText = slashCommand.Data.Options.ElementAt(0).Value as string;
        var result = GetData(inputText);

        if (result.placeholders.Count != 0)
        {
            // there are special keys
            SimulateSequenceWithSpecial(result.modifiedText, result.placeholders);
        }
        else
        {
            SimulateKeyPress(inputText);
        }

        return DiscordEmbed.CreateWithText(Color.Blue, "Zakończono", $"Wypisano znaki {inputText}",
            Environment.UserName, null);
    }
}