namespace Paroxysm.Discord.Commands.Models;

public class SlashCommandOptions
{
    // Name of command
    public string Name { get; set; }

    // Description of command
    public string Description { get; set; }

    // Command params
    public IEnumerable<SlashCommandOptionParams>? Params { get; set; }
}