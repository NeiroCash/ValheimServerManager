using ValheimServerManager.Views;

namespace ValheimServerManager.ViewModels;

public sealed class MainWindowViewModel : ViewModelBase
{
    private object _currentPage;
    private bool _isDashboardSelected;
    private bool _isConfigurationSelected;

    public MainWindowViewModel()
    {
        Dashboard = new DashboardViewModel();
        Configuration = new ConfigurationViewModel();
        _currentPage = Dashboard;
        _isDashboardSelected = true;

        ShowDashboardCommand = new RelayCommand(() => CurrentPage = Dashboard);
        ShowConfigurationCommand = new RelayCommand(() => CurrentPage = Configuration);
    }

    public DashboardViewModel Dashboard { get; }
    public ConfigurationViewModel Configuration { get; }

    public object CurrentPage
    {
        get => _currentPage;
        private set
        {
            if (ReferenceEquals(_currentPage, value)) return;

            _currentPage = value;
            IsDashboardSelected = ReferenceEquals(value, Dashboard);
            IsConfigurationSelected = ReferenceEquals(value, Configuration);
            OnPropertyChanged();
        }
    }

    public bool IsDashboardSelected
    {
        get => _isDashboardSelected;
        private set => Set(ref _isDashboardSelected, value);
    }

    public bool IsConfigurationSelected
    {
        get => _isConfigurationSelected;
        private set => Set(ref _isConfigurationSelected, value);
    }

    public RelayCommand ShowDashboardCommand { get; }
    public RelayCommand ShowConfigurationCommand { get; }

    private void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }
}
