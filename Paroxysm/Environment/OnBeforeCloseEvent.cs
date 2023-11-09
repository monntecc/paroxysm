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
        public static void Init()
        {
            Thread monitoringThread = new Thread(monitorProcess);
            monitoringThread.Start();
            monitoringThread.Join();
        }

        private static void monitorProcess()
        {
            Process currentProcess = Process.GetCurrentProcess();

            while(!currentProcess.HasExited)
            {
                Thread.Sleep(100);
            }

            SendClosingMessage();
        }

        private static async void SendClosingMessage()
        {
            Console.WriteLine(webhookUrl);
            var client = new DiscordWebhookClient(TokenType.Webhook, webhook.Token);
            var embed = EmbedCreator.CreateWithText(Color.DarkBlue, "Wyłączono program", "Program został wyłączony", Environment.UserName, "");
            await client.SendMessageAsync("", false, (IEnumerable<Embed>)embed);


            Environment.Exit(1);
        }
    }
}