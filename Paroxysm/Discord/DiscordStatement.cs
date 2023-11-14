using Discord;
using Discord.WebSocket;

namespace Paroxysm.Discord;

public static class DiscordStatement
{
    public static DiscordSocketClient DiscordClient = new();

    public static IGuild CurrentGuild;
    
    public static IWebhook CurrentWebhook;

    public static ITextChannel CurrentChannel;
    
    public static ITextChannel AuditChannel;

    public static Thread TaskMgrThread;
}