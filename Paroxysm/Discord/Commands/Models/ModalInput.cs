using Discord;

namespace Paroxysm.Discord.Commands.Models;

public class ModalInput
{
    public string Label { get; set; }
    public string CustomId { get; set; }
    public TextInputStyle? TextInputType { get; set; }
    public string? Placeholder { get; set; }
    public int? MinLength { get; set; }
    public int? MaxLength { get; set; }
    public bool? IsRequired { get; set; }
    public string? Value { get; set; }

    public ModalInput(string label, string id)
    {
        Label = label;
        CustomId = id;
    }
}