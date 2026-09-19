using System.ComponentModel;
using ValheimServerManager.Views;

namespace ValheimServerManager.ViewModels;

public sealed class MainWindowViewModel : ViewModelBase
{
    private object _currentPage;

    public MainWindowViewModel()
    {
        Dashboard = new DashboardViewModel();
        Configuration = new ConfigurationViewModel();
        _currentPage = Dashboard;

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
            OnPropertyChanged();
        }
    }

    public RelayCommand ShowDashboardCommand { get; }
    public RelayCommand ShowConfigurationCommand { get; }
}
