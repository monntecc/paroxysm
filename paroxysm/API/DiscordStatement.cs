﻿using Discord;
using Discord.WebSocket;

namespace paroxysm.API;

public static class DiscordStatement
{
    public static DiscordSocketClient DiscordClient = new();
    
    public static ITextChannel CurrentChannel;

    public static Thread TaskMgrThread;
}     