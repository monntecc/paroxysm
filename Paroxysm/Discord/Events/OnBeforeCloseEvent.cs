using System.Runtime.InteropServices;
using Discord;
using Discord.Webhook;
using Paroxysm.Discord.Events.Models;
using Paroxysm.Tools;

namespace Paroxysm.Discord.Events;

internal abstract class OnBeforeCloseEvent
{
    public static IWebhook Webhook { get; set; }

    public static bool IsProgramExited { get; set; }

    [DllImport("Kernel32")]
    public static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

    public static EventHandler AppCloseHandler = null!;

    internal delegate bool EventHandler(CtrlType sig);

    public static void OnBeforeClose(object? sender, ConsoleCancelEventArgs e)
    {
        if (!Settings.AllowConsoleClose) return;

        SendPreventedMessage();
        e.Cancel = true;
    }

    public static async void SendReadyMessage()
    {
        var client = new DiscordWebhookClient(Webhook);
        var embed = DiscordEmbed.CreateWithText(Color.DarkBlue, Environment.UserName,
            "Webhook was successfully enabled.", Environment.UserName, "");
        await client.SendMessageAsync("", false, new[] { embed });
    }

    private static async void SendPreventedMessage()
    {
        var client = new DiscordWebhookClient(Webhook);
        var embed = DiscordEmbed.CreateWithText(Color.DarkBlue, "Powstrzymano zamknięcie programu",
            "Wykryto probe wyłączenia programu, oraz powstrzymano", Environment.UserName, "");
        await client.SendMessageAsync("", false, new[] { embed });
    }

    public static bool SendClosingMessage(CtrlType sig)
    {
        var client = new DiscordWebhookClient(Webhook);
        var embed = DiscordEmbed.CreateWithText(Color.DarkBlue, "Wyłączono program", "Program został wyłączony",
            Environment.UserName, "");
        client.SendMessageAsync("", false, new[] { embed });

        IsProgramExited = true;

        Environment.Exit(-1);
        return true;
    }
}