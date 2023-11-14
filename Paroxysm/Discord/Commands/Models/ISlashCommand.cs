using Discord;
using Discord.WebSocket;

namespace Paroxysm.Discord.Commands.Models;

public interface ISlashCommand
{
    // Get command information
    SlashCommandOptions Options();

    // Execute function
    Embed Execute(SocketSlashCommand slashCommand);

    // Slash command generator
    public SlashCommandBuilder CreateSlashCommand()
    {
        SlashCommandBuilder commandBuilder = new();
        commandBuilder.WithName(Options().Name);
        commandBuilder.WithDescription(Options().Description);

        var parameters = Options().Params ?? null;

        if (parameters is null) return commandBuilder;

        var commandOptions = new List<ApplicationCommandOptionChoiceProperties>();

        foreach (var parameter in parameters)
        {
            if (parameter is { IsChoiceEnable: true, ChoiceOptions: not null })
            {
                commandOptions.AddRange(parameter.ChoiceOptions.Select(choiceOption =>
                    new ApplicationCommandOptionChoiceProperties { Value = choiceOption, Name = choiceOption }));
            }

            commandBuilder.AddOption(new SlashCommandOptionBuilder
            {
                Name = parameter.Name,
                Description = parameter.Description,
                Type = parameter.Type,
                IsRequired = parameter.IsRequired,
                Choices = commandOptions
            });
        }
        

        return commandBuilder;
    }
}