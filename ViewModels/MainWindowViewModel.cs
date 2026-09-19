using System.Collections.ObjectModel;
using ValheimServerManager.Models;

namespace ValheimServerManager.ViewModels;

public sealed class MainWindowViewModel : ViewModelBase
{
    public DashboardViewModel Dashboard { get; } = new();
}
