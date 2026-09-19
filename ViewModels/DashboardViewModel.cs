using System.Collections.ObjectModel;
using ValheimServerManager.Models;

namespace ValheimServerManager.ViewModels;

public sealed class DashboardViewModel : ViewModelBase
{
    public ServerStatus Server { get; } = new(
        name: "MyValheim Server",
        state: "Running",
        version: "v1.0.14",
        gameMode: "Survival",
        serverType: "Dedicated",
        players: "3 / 10",
        uptime: "2h 14m",
        worldName: "MyWorld",
        port: "2456",
        maxPlayers: "10",
        passwordProtected: "••••••",
        saveDirectory: "D:\Games\ValheimServer\worlds",
        publicServer: "Enabled",
        worldPreset: "Custom",
        modded: "Yes",
        playerCountText: "Players: 3 / 10",
        serverMode: "Dedicated",
        currentWorld: "MyWorld");

    public ObservableCollection<ServerInfoItem> ServerInformation { get; } = new()
    {
        new("Server Name", "MyValheim Server"),
        new("World Name", "MyWorld"),
        new("Password", "••••••"),
        new("Port", "2456"),
        new("Max Players", "10"),
        new("Save Directory", "D:\Games\ValheimServer\worlds"),
        new("Public Server", "Visible in Steam"),
    };

    public ObservableCollection<ModifierItem> WorldModifiers { get; } = new()
    {
        new("Preset", "Custom (No Preset)"),
        new("Combat", "Normal"),
        new("Death Penalty", "Casual"),
        new("Resource Rate", "More (1.5x)"),
        new("Raids", "Less"),
        new("Portals", "Casual (Portal items)"),
    };

    public ObservableCollection<ModItem> Mods { get; } = new()
    {
        new("BetterSmelting", "v1.4.2", "Server+Client", "Adjust smelter, furnace, kiln sizes and speed", true),
        new("ServerDevcommands", "v1.99", "Server", "Admin commands for dedicated server", true),
        new("EpicLoot", "v0.11.0", "Server+Client", "Adds item randomization and more", true),
        new("PlantEverything", "v1.7.15", "Server+Client", "Plant any item", true),
        new("ValheimPlus", "v0.9.12", "Server+Client", "Quality of life improvements", true),
        new("Clock", "v1.0.1", "Client", "In-game clock and time display", true),
    };

    public ObservableCollection<PlayerItem> PlayersOnline { get; } = new()
    {
        new("NeiroCash", "32 ms", "Connected"),
        new("VikingUA", "48 ms", "Connected"),
        new("Ketra", "61 ms", "Connected")
    };

    public ObservableCollection<ConsoleLine> ConsoleLines { get; } = new()
    {
        new("[14:22:01]", "Server started", ConsoleLineKind.System),
        new("[14:22:02]", "BepInEx 5.4.23.2 - chainloader ready", ConsoleLineKind.Info),
        new("[14:22:02]", "Loaded 6 plugins", ConsoleLineKind.Info),
        new("[14:22:03]", "BetterSmelting v1.4.2 loaded", ConsoleLineKind.Info),
        new("[14:22:03]", "Server world: MyWorld", ConsoleLineKind.Info),
        new("[14:22:03]", "Game version: 1.0.14", ConsoleLineKind.Info),
        new("[14:22:11]", "Player connected: NeiroCash", ConsoleLineKind.System),
        new("[14:22:15]", "Player connected: VikingUA", ConsoleLineKind.System),
        new("[14:22:19]", "Player connected: Ketra", ConsoleLineKind.System),
        new("[14:22:20]", "Autosave completed", ConsoleLineKind.Warning)
    };
}
