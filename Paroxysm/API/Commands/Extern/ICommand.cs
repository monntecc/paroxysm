namespace Paroxysm.API.Commands.Extern;

public interface ICommand
{
    // Get command information
    CommandOptions Options();
    
    // Execute function
    void Execute(string[]? parameters);
}