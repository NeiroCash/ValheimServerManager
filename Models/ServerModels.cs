using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ValheimServerManager.Models;

public enum ConsoleLineKind
{
    System,
    Info,
    Warning,
    Error
}

public sealed record ServerStatus(
    string Name,
    string State,
    string Version,
    string GameMode,
    string ServerType,
    string Players,
    string Uptime,
    string WorldName,
    string Port,
    string MaxPlayers,
    string PasswordProtected,
    string SaveDirectory,
    string PublicServer,
    string WorldPreset,
    string Modded,
    string PlayerCountText,
    string ServerMode,
    string CurrentWorld);

public sealed record ServerInfoItem(string Label, string Value);
public sealed record ModifierItem(string Label, string Value);
public sealed record ModItem(string Name, string Version, string Type, string Description, bool Enabled);
public sealed record PlayerItem(string Name, string Ping, string Status);
public sealed record ConsoleLine(string Timestamp, string Message, ConsoleLineKind Kind);

public sealed class ServerStatusViewModel : INotifyPropertyChanged
{
    private string _state = "Running";
    public string State
    {
        get => _state;
        set { if (_state != value) { _state = value; OnPropertyChanged(); } }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
