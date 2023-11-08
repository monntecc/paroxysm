using Discord;

namespace Paroxysm.Discord.Commands.Models;

public class SlashCommandOptionParams
{
    // Name of option
    public string Name { get; set; }

    // Description of option
    public string Description { get; set; }

    // Is Required?
    public bool IsRequired { get; set; }

    // Type of option
    public ApplicationCommandOptionType Type { get; set; }

    // Is Choice available ?
    public bool? IsChoiceEnable { get; set; }

    // Choice variants
    public string[]? ChoiceOptions { get; set; }
}