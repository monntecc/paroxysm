using Discord;
using Discord.Webhook;
using System;
using System.Threading.Tasks;
using Paroxysm.Tools;
using System.Diagnostics;

namespace Paroxysm.Tools
{
    internal class MonitorProcess
    {
        public static IWebhook webhook { get; set; }

        public static void OnBeforeClose(object sender, ConsoleCancelEventArgs e)
        {

            if (Settings.AllowConsoleClose)
            {
                SendPreventedMessage();
                e.Cancel = true;
            }
        }
       
        public static async void SendReadyMessage()
        {
            var client = new DiscordWebhookClient(webhook);
            Embed embed = new EmbedBuilder
            {
                Title = Environment.UserName,
                Color = Color.DarkBlue,
                Description = "Webhook enabled",
                Timestamp = DateTime.UtcNow
            }.Build();
            await client.SendMessageAsync("", false, (IEnumerable<Embed>)embed);
        }

        private static async void SendPreventedMessage()
        {
            var client = new DiscordWebhookClient(webhook);
            var embed = EmbedCreator.CreateWithText(Color.DarkBlue, "Powstrzymano zamknięcie programu", "Wykryto probe wyłączenia programu, oraz powstrzymano", Environment.UserName, "");
            await client.SendMessageAsync("", false, (IEnumerable<Embed>)embed);
        }

        public static async void SendClosingMessage(object sender, object e)
        {
            var client = new DiscordWebhookClient(webhook);
            var embed = EmbedCreator.CreateWithText(Color.DarkBlue, "Wyłączono program", "Program został wyłączony", Environment.UserName, "");
            await client.SendMessageAsync("", false, (IEnumerable<Embed>)embed);

            Environment.Exit(1);
        }
    }
}