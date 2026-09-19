namespace ValheimServerManager.ViewModels;

public sealed class ConfigurationViewModel : ViewModelBase
{
    private string _serverDirectory = @"D:\ValheimServer\";
    private string _serverExecutable = "valheim_server.exe";
    private string _worldsDirectory = @"D:\ValheimServer\worlds\";
    private string _backupsDirectory = @"D:\ValheimServer\backups\";
    private string _startupArguments = "-name \"My Valheim Server\" -port 2456 -world \"MyWorld\"";
    private bool _autoStart;
    private bool _minimizeToTray = true;
    private bool _closeToTray = true;
    private bool _checkForUpdates = true;
    private bool _automaticBackups = true;
    private bool _notifyOnServerState = true;
    private bool _notifyOnPlayers = true;
    private bool _notifyOnErrors = true;

    public string ServerDirectory { get => _serverDirectory; set => Set(ref _serverDirectory, value); }
    public string ServerExecutable { get => _serverExecutable; set => Set(ref _serverExecutable, value); }
    public string WorldsDirectory { get => _worldsDirectory; set => Set(ref _worldsDirectory, value); }
    public string BackupsDirectory { get => _backupsDirectory; set => Set(ref _backupsDirectory, value); }
    public string StartupArguments { get => _startupArguments; set => Set(ref _startupArguments, value); }
    public bool AutoStart { get => _autoStart; set => Set(ref _autoStart, value); }
    public bool MinimizeToTray { get => _minimizeToTray; set => Set(ref _minimizeToTray, value); }
    public bool CloseToTray { get => _closeToTray; set => Set(ref _closeToTray, value); }
    public bool CheckForUpdates { get => _checkForUpdates; set => Set(ref _checkForUpdates, value); }
    public bool AutomaticBackups { get => _automaticBackups; set => Set(ref _automaticBackups, value); }
    public bool NotifyOnServerState { get => _notifyOnServerState; set => Set(ref _notifyOnServerState, value); }
    public bool NotifyOnPlayers { get => _notifyOnPlayers; set => Set(ref _notifyOnPlayers, value); }
    public bool NotifyOnErrors { get => _notifyOnErrors; set => Set(ref _notifyOnErrors, value); }

    public IReadOnlyList<string> Themes { get; } = ["Dark (Default)", "Light"];
    public IReadOnlyList<string> AccentColors { get; } = ["Blue", "Green", "Amber"];
    public IReadOnlyList<string> Languages { get; } = ["English", "Русский"];
    public IReadOnlyList<string> LogLevels { get; } = ["Info", "Warning", "Error"];
    public IReadOnlyList<string> BackupIntervals { get; } = ["Every hour", "Every 6 hours", "Every 12 hours", "Every day"];

    public string SelectedTheme { get; set; } = "Dark (Default)";
    public string SelectedAccentColor { get; set; } = "Blue";
    public string SelectedLanguage { get; set; } = "English";
    public string SelectedLogLevel { get; set; } = "Info";
    public string SelectedBackupInterval { get; set; } = "Every 6 hours";

    private void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }
}
