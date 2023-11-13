using Discord.WebSocket;
using Discord;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paroxysm.Discord.Commands;

public class ClosewinCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "close",
            Description = "Zamyka okno na którym użytkonik trzyma focus"
        };
    }

    public Embed Execute(SocketSlashCommand slashCommand)
    {
        return CloseWinAction.Follow();
    }
}