using System.IO;
using System.Xml.Serialization;
using Forms = System.Windows.Forms;
using WpfOpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace ValheimServerManager.ViewModels;

public sealed class ConfigurationViewModel : ViewModelBase
{
    private static readonly string ConfigurationDirectory = Path.Combine(AppContext.BaseDirectory, "Config");
    private static readonly string ConfigurationFilePath = Path.Combine(ConfigurationDirectory, "Configuration.xml");

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

    public ConfigurationViewModel()
    {
        BrowseServerDirectoryCommand = new RelayCommand(() => ServerDirectory = PickFolder(ServerDirectory));
        BrowseExecutableCommand = new RelayCommand(() => ServerExecutable = PickExecutable(ServerExecutable));
        BrowseWorldsDirectoryCommand = new RelayCommand(() => WorldsDirectory = PickFolder(WorldsDirectory));
        BrowseBackupsDirectoryCommand = new RelayCommand(() => BackupsDirectory = PickFolder(BackupsDirectory));
        SaveConfigurationCommand = new RelayCommand(SaveConfiguration);

        LoadConfiguration();
    }

    public RelayCommand BrowseServerDirectoryCommand { get; }
    public RelayCommand BrowseExecutableCommand { get; }
    public RelayCommand BrowseWorldsDirectoryCommand { get; }
    public RelayCommand BrowseBackupsDirectoryCommand { get; }
    public RelayCommand SaveConfigurationCommand { get; }

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

    private void SaveConfiguration()
    {
        Directory.CreateDirectory(ConfigurationDirectory);

        var data = new ConfigurationData
        {
            ServerDirectory = ServerDirectory,
            ServerExecutable = ServerExecutable,
            WorldsDirectory = WorldsDirectory,
            BackupsDirectory = BackupsDirectory,
            StartupArguments = StartupArguments,
            AutoStart = AutoStart,
            MinimizeToTray = MinimizeToTray,
            CloseToTray = CloseToTray,
            CheckForUpdates = CheckForUpdates,
            AutomaticBackups = AutomaticBackups,
            NotifyOnServerState = NotifyOnServerState,
            NotifyOnPlayers = NotifyOnPlayers,
            NotifyOnErrors = NotifyOnErrors,
            SelectedTheme = SelectedTheme,
            SelectedAccentColor = SelectedAccentColor,
            SelectedLanguage = SelectedLanguage,
            SelectedLogLevel = SelectedLogLevel,
            SelectedBackupInterval = SelectedBackupInterval
        };

        var serializer = new XmlSerializer(typeof(ConfigurationData));
        using var writer = new StreamWriter(ConfigurationFilePath, false);
        serializer.Serialize(writer, data);
    }

    private void LoadConfiguration()
    {
        if (!File.Exists(ConfigurationFilePath)) return;

        try
        {
            var serializer = new XmlSerializer(typeof(ConfigurationData));
            using var reader = new StreamReader(ConfigurationFilePath);
            if (serializer.Deserialize(reader) is not ConfigurationData data) return;

            ServerDirectory = data.ServerDirectory ?? ServerDirectory;
            ServerExecutable = data.ServerExecutable ?? ServerExecutable;
            WorldsDirectory = data.WorldsDirectory ?? WorldsDirectory;
            BackupsDirectory = data.BackupsDirectory ?? BackupsDirectory;
            StartupArguments = data.StartupArguments ?? StartupArguments;
            AutoStart = data.AutoStart;
            MinimizeToTray = data.MinimizeToTray;
            CloseToTray = data.CloseToTray;
            CheckForUpdates = data.CheckForUpdates;
            AutomaticBackups = data.AutomaticBackups;
            NotifyOnServerState = data.NotifyOnServerState;
            NotifyOnPlayers = data.NotifyOnPlayers;
            NotifyOnErrors = data.NotifyOnErrors;
            SelectedTheme = data.SelectedTheme ?? SelectedTheme;
            SelectedAccentColor = data.SelectedAccentColor ?? SelectedAccentColor;
            SelectedLanguage = data.SelectedLanguage ?? SelectedLanguage;
            SelectedLogLevel = data.SelectedLogLevel ?? SelectedLogLevel;
            SelectedBackupInterval = data.SelectedBackupInterval ?? SelectedBackupInterval;
        }
        catch (InvalidOperationException)
        {
            // Keep default values if an old or damaged configuration is found.
        }
    }

    private static string PickFolder(string currentValue)
    {
        var initial = Directory.Exists(currentValue) ? currentValue : Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
        using var dialog = new Forms.FolderBrowserDialog
        {
            Description = "Select a folder",
            SelectedPath = initial,
            ShowNewFolderButton = true
        };
        return dialog.ShowDialog() == Forms.DialogResult.OK ? dialog.SelectedPath : currentValue;
    }

    private static string PickExecutable(string currentValue)
    {
        var currentDirectory = Path.GetDirectoryName(currentValue);
        var initialDirectory = !string.IsNullOrWhiteSpace(currentDirectory) && Directory.Exists(currentDirectory)
            ? currentDirectory
            : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        var dialog = new WpfOpenFileDialog
        {
            Filter = "Valheim server executable (*.exe)|*.exe|All files (*.*)|*.*",
            InitialDirectory = initialDirectory,
            FileName = Path.GetFileName(currentValue),
            CheckFileExists = true
        };
        return dialog.ShowDialog() == true ? dialog.FileName : currentValue;
    }

    private void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }

    [Serializable]
    public sealed class ConfigurationData
    {
        public string? ServerDirectory { get; set; }
        public string? ServerExecutable { get; set; }
        public string? WorldsDirectory { get; set; }
        public string? BackupsDirectory { get; set; }
        public string? StartupArguments { get; set; }
        public bool AutoStart { get; set; }
        public bool MinimizeToTray { get; set; }
        public bool CloseToTray { get; set; }
        public bool CheckForUpdates { get; set; }
        public bool AutomaticBackups { get; set; }
        public bool NotifyOnServerState { get; set; }
        public bool NotifyOnPlayers { get; set; }
        public bool NotifyOnErrors { get; set; }
        public string? SelectedTheme { get; set; }
        public string? SelectedAccentColor { get; set; }
        public string? SelectedLanguage { get; set; }
        public string? SelectedLogLevel { get; set; }
        public string? SelectedBackupInterval { get; set; }
    }
}
