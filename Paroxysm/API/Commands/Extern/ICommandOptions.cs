namespace Paroxysm.API.Commands.Extern;

public class CommandOptions
{
    // Name of command
    public string Name { get; set; }
    
    // List of indicated parameters to command (for info embed only!)
    public string Parameters { get; set; }
    
    // Description of command
    public string Description { get; set; }
}